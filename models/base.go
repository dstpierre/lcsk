package models

import (
	"time"

	"gopkg.in/mgo.v2/bson"
)

// Base represents a base Document
type Base struct {
	ID      bson.ObjectId `bson:"_id" json:"id"`
	Created time.Time     `bson:"created" json:"created"`
}
