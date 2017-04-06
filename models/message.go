/*

External website / web app embed widget

| visitor|user => discussion => multiple messages
| agent => => discussion => send messages & send internal notes


chatbot: Hey, what's your email?
visitor: email@

*/

package models

type Message struct {
	Base
	FirstName string `bson:"first" json:"first"`
	LastName  string `bson:"last" json:"last"`
	Email     string `bson:"email" json:"email"`
	Body      string `bson:"body" json:"body"`
}
