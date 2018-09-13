module Main exposing (..)

import Browser
import Browser.Navigation as Nav
import Html exposing (..)
import Html.Attributes exposing (..)
import Html.Events exposing (onInput, onClick)
import Url


-- import WebSocket


main : Program () Model Msg
main =
    Browser.application
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        , onUrlRequest = LinkClicked
        , onUrlChange = UrlChanged
        }


type alias Model =
    { key : Nav.Key
    , url : Url.Url
    , input : String
    , messages : List String
    }


init : () -> Url.Url -> Nav.Key -> ( Model, Cmd Msg )
init flags url key =
    ( Model key url "" [], Cmd.none )


type Msg
    = LinkClicked Browser.UrlRequest
    | UrlChanged Url.Url
    | Input String
    | NewMessage String


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        LinkClicked urlRequest ->
            case urlRequest of
                Browser.Internal url ->
                    ( model, Nav.pushUrl model.key (Url.toString url) )

                Browser.External href ->
                    ( model, Nav.load href )

        UrlChanged url ->
            ( { model | url = url }, Cmd.none )

        Input ni ->
            ( { model | input = ni }, Cmd.none )

        NewMessage s ->
            ( { model | messages = (s :: model.messages) }, Cmd.none )



-- WebSocket.listen "ws://echo.websocket.org" NewMessage


subscriptions : Model -> Sub Msg
subscriptions model =
    Sub.none


view : Model -> Browser.Document Msg
view model =
    { title = "testing 0.19"
    , body =
        [ viewLayout model ]
    }


viewLayout model =
    div [ class "columns" ]
        [ div [ class "column is-one-fifth" ] [ viewMenu model ]
        , div [ class "column is-four-fifths" ] [ viewMain model ]
        ]


viewMenu model =
    div [ class "columns" ]
        [ div [ class "column is-one-fifth" ]
            [ img [ src "/img/logo.png" ] []
            , p [] [ a [ href "/dashboard" ] [ text "dash" ] ]
            , p [] [ a [ href "/people" ] [ text "people" ] ]
            , p [] [ a [ href "/tickets" ] [ text "tickets" ] ]
            , p [] [ a [ href "/campaigns" ] [ text "campaigns" ] ]
            , p [] [ a [ href "/conversations" ] [ text "convo" ] ]
            , p [] [ a [ href "/kb" ] [ text "kb" ] ]
            ]
        , div [ class "column is-four-fifths" ] [ text "sub-menu" ]
        ]


viewMain : Model -> Html Msg
viewMain model =
    h1 [ class "title is-1" ] [ text ("Page: " ++ (Url.toString model.url)) ]


viewMessage : String -> Html msg
viewMessage msg =
    div [] [ text msg ]
