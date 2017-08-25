package controllers

import (
	"errors"
	"fmt"
	"io"
	"log"
	"net/http"
	"strings"
	"time"

	"gopkg.in/mgo.v2/bson"

	"encoding/json"

	"github.com/parle-io/parle/dal"
	"github.com/satori/go.uuid"
	"golang.org/x/net/websocket"
)

// temporary fake conversations repository
var tmpConversations map[bson.ObjectId]dal.Conversation

// RealtimeEvent is used to pass data from browser to server via websocket
type RealtimeEvent struct {
	Token string `json:"token"`
	Name  string `json:"name"`
	Data  string `json:"data"`
}

type client struct {
	id     string
	userID bson.ObjectId
	ws     *websocket.Conn
	s      *server
	ch     chan *RealtimeEvent
	doneCh chan bool
}

func newClient(ws *websocket.Conn, s *server) (*client, error) {
	if ws == nil {
		return nil, errors.New("websoket is nil")
	}

	if s == nil {
		return nil, errors.New("server is nil")
	}

	ch := make(chan *RealtimeEvent, 100)
	doneCh := make(chan bool)

	// we use an empty userID until we are able to identify this user
	var emptyUserID bson.ObjectId
	return &client{uuid.NewV4().String(), emptyUserID, ws, s, ch, doneCh}, nil
}

func (c *client) write(event *RealtimeEvent) {
	select {
	case c.ch <- event:
	default:
		c.s.del(c)
		err := fmt.Errorf("Client %s is disconnected", c.id)
		c.s.err(err)
	}
}

func (c *client) done() {
	c.doneCh <- true
}

func (c *client) listen() {
	go c.listenWrite()
	c.listenRead()
}

func (c *client) listenWrite() {
	log.Println("Listening write to client")
	for {
		select {
		case evt := <-c.ch:
			websocket.JSON.Send(c.ws, evt)
		case <-c.doneCh:
			c.s.del(c)
			c.doneCh <- true
			return
		}
	}
}

func (c *client) listenRead() {
	log.Println("Listen read for client: " + c.id)

	for {
		select {
		case <-c.doneCh:
			c.s.del(c)
			c.doneCh <- true
			return
		default:
			var evt RealtimeEvent
			err := websocket.JSON.Receive(c.ws, &evt)
			if err == io.EOF {
				c.doneCh <- true
			} else if err != nil {
				c.s.err(err)
			} else {
				c.s.do(&evt)
			}
		}
	}
}

type server struct {
	pattern string
	clients map[string]*client
	convos  map[string][]*client
	addCh   chan *client
	evtCh   chan *RealtimeEvent
	delCh   chan *client
	doneCh  chan bool
	errCh   chan error
}

func newRealtime(pattern string) *server {
	clients := make(map[string]*client)
	convos := make(map[string][]*client)
	addCh := make(chan *client)
	evtCh := make(chan *RealtimeEvent)
	delCh := make(chan *client)
	doneCh := make(chan bool)
	errCh := make(chan error)

	return &server{
		pattern,
		clients,
		convos,
		addCh,
		evtCh,
		delCh,
		doneCh,
		errCh,
	}
}

func (s *server) add(c *client) {
	s.addCh <- c
}

func (s *server) emit(e *RealtimeEvent) {
	s.evtCh <- e
}

func (s *server) del(c *client) {
	s.delCh <- c
}

func (s *server) done() {
	s.doneCh <- true
}

func (s *server) err(e error) {
	s.errCh <- e
}

func (s *server) do(e *RealtimeEvent) {
	log.Printf("Received this: %v", e)

	switch strings.ToUpper(e.Name) {
	case "IDENTIFY":
		s.identify(e)
	case "LISTCONV":
		s.listConversations(e)
	case "NEWCONV":
		s.newConversation(e)
	case "AUTH":
		s.auth(e)
	}
}

func (s *server) identify(e *RealtimeEvent) {
	// at this stage we assume everyone is public visitor
	s.clients[e.Token].userID = bson.NewObjectId()
}

func (s *server) listConversations(e *RealtimeEvent) {
	userID := s.clients[e.Token].userID

	// this should be re-written using mongo where passing the userID
	var convos []dal.Conversation
	for _, v := range tmpConversations {
		if v.UserID == userID {
			convos = append(convos, v)
		}
	}

	msg := &RealtimeEvent{Token: e.Token, Name: "listconv"}
	b, err := json.Marshal(convos)
	if err != nil {
		log.Println("error todo:", err)
		return
	}

	msg.Data = string(b)
	s.emit(msg)
}

func (s *server) newConversation(e *RealtimeEvent) {
	convo := dal.Conversation{}
	convo.ID = bson.NewObjectId()
	convo.Created = time.Now()
	convo.UserID = s.clients[e.Token].userID

	tmpConversations[convo.ID] = convo

	msg := &RealtimeEvent{Token: e.Token, Name: "newconv", Data: convo.ID.Hex()}
	s.emit(msg)

	/*
		var toLink *client
		for _, c := range s.clients {
			if c.id == e.Data {
				toLink = c
				break
			}
		}

		if lists, ok := s.convos[e.Token]; ok {
			s.convos[e.Token] = append(lists, toLink)
		} else {
			var lists []*client
			s.convos[e.Token] = append(lists, toLink)
		}

		toLink.write(&RealtimeEvent{e.Token, "joined channel", "ready to read/write to channel."})
	*/
}

func (s *server) auth(e *RealtimeEvent) {
	log.Println("AUTH", e.Data)

	if e.Data != "something-here" {
		log.Println("wrong auth")
		for _, c := range s.clients {
			if c.id == e.Token {
				c.done()
				break
			}
		}
		return
	}

	msg := &RealtimeEvent{Token: e.Token, Name: "discussions"}
	d := ""
	for k := range s.convos {
		d += k + ","
	}
	d = strings.TrimSuffix(d, ",")

	msg.Data = d

	log.Println("sending msg", msg)
	s.emit(msg)
}

func (s *server) listen() {
	log.Println("Realtime server listening...")

	onConnected := func(ws *websocket.Conn) {
		defer func() {
			err := ws.Close()
			if err != nil {
				s.errCh <- err
			}
		}()

		c, err := newClient(ws, s)
		if err != nil {
			log.Println("error: " + err.Error())
		} else {
			s.add(c)
			c.listen()
		}
	}

	http.Handle(s.pattern, websocket.Handler(onConnected))
	log.Println("Created websocket handler")

	for {
		select {
		case c := <-s.addCh:
			log.Println("New connection")

			s.clients[c.id] = c

			// sending the socket id to the client so they can join (link) a channel
			c.write(&RealtimeEvent{"server", "hello", c.id})
		case e := <-s.evtCh:
			if target, ok := s.clients[e.Token]; ok {
				target.write(e)
			} else if convo, ok := s.convos[e.Token]; ok {
				for _, c := range convo {
					c.write(e)
				}
			}
		case c := <-s.delCh:
			log.Println("Removing client")

			for _, v := range s.convos {
				for _, linked := range v {
					if linked.id == c.id {
						//TODO: Remove from groups
					}
				}
			}
			delete(s.clients, c.id)
		case err := <-s.errCh:
			log.Println("Error: " + err.Error())
		case <-s.doneCh:
			return

		}
	}
}
