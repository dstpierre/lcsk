package controllers

import (
	"net/http"
	"github.com/gorilla/mux"
)

// Start registers our routes
func Start() {
  uc := &userController{}

	rt := newRealtime("/rt")
	go rt.listen()

  router := mux.NewRouter()

  api := router.PathPrefix("/api").Subrouter()
  api.HandleFunc("/users", uc.Login)

  router.PathPrefix("/").Handler(http.FileServer(http.Dir("./public/")))

  http.Handle("/", router)


  http.ListenAndServe(":8181", nil)
}