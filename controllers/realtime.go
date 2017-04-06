package controllers

import (
	"errors"
	"fmt"
	"io"
	"log"
	"net/http"
	"strings"

	"github.com/satori/go.uuid"
	"golang.org/x/net/websocket"
)

// RealtimeEvent is used to pass data from browser to server via websocket
type RealtimeEvent struct {
	Token string `json:"token"`
	Name  string `json:"name"`
	Data  string `json:"data"`
}

type client struct {
	id     string
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
	return &client{uuid.NewV4().String(), ws, s, ch, doneCh}, nil
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
	groups  map[string][]*client
	addCh   chan *client
	evtCh   chan *RealtimeEvent
	delCh   chan *client
	doneCh  chan bool
	errCh   chan error
}

func newRealtime(pattern string) *server {
	clients := make(map[string]*client)
	groups := make(map[string][]*client)
	addCh := make(chan *client)
	evtCh := make(chan *RealtimeEvent)
	delCh := make(chan *client)
	doneCh := make(chan bool)
	errCh := make(chan error)

	return &server{
		pattern,
		clients,
		groups,
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
	case "LINK":
		s.link(e)
	case "AUTH":
		s.auth(e)
	}
}

func (s *server) link(e *RealtimeEvent) {
	var toLink *client
	for _, c := range s.clients {
		if c.id == e.Data {
			toLink = c
			break
		}
	}

	if lists, ok := s.groups[e.Token]; ok {
		s.groups[e.Token] = append(lists, toLink)
	} else {
		var lists []*client
		s.groups[e.Token] = append(lists, toLink)
	}

	toLink.write(&RealtimeEvent{e.Token, "joined channel", "ready to read/write to channel."})
}

func (s *server) auth(e *RealtimeEvent) {
	log.Println("AUTH", e.Data)

	if e.Data != "something-here" {
		log.Println("wrong auth")
		for _, c := range s.clients {
			if c.id == e.Token {
				c.done();
				break
			}
		}
		return
	}

	msg := &RealtimeEvent{Token: e.Token, Name: "discussions"}
	d := ""
	for k := range s.groups {
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
			for k, v := range s.groups {
				if k == e.Token {
					for _, c := range v {
						c.write(e)
					}
				}
			}
		case c := <-s.delCh:
			log.Println("Removing client")

			for _, v := range s.groups {
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
