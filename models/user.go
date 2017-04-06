package models

import (
	"gopkg.in/mgo.v2/bson"
)

// User represents a user on the app
type User struct {
  ID bson.ObjectId `bson:"_id" json:"id"`
}

