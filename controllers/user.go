package controllers

import (
	"net/http"
)

type userController struct{}

func (ctrl *userController) Login(w http.ResponseWriter, r *http.Request) {
  w.Write([]byte("hello world"))
}