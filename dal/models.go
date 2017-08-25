package dal

import (
	"time"

	"gopkg.in/mgo.v2/bson"
)

// Base represents a base Document
type Base struct {
	ID      bson.ObjectId `bson:"_id" json:"id"`
	Created time.Time     `bson:"created" json:"created"`
}

// User represents a in the app
type User struct {
	Base
}

// Conversation represents the data structure used during a conversation
type Conversation struct {
	Base
	UserID   bson.ObjectId `bson:"userId" json:"userId"`
	Messages []Message     `bson:"messages" json:"messages"`
	IsClosed bool          `bson:"isClosed" json:"isClosed"`
}

// Message represents a message sent by the user or the agent in a conversation
type Message struct {
	ID        string    `bson:"id" json:"id"`
	FirstName string    `bson:"first" json:"first"`
	LastName  string    `bson:"last" json:"last"`
	Email     string    `bson:"email" json:"email"`
	Body      string    `bson:"body" json:"body"`
	SentOn    time.Time `bson:"sentOn" json:"sentOn"`
}
