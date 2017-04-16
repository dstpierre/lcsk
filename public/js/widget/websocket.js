(function (parle) {
    var conn = new WebSocket("ws://" + document.location.host + "/rt");
    conn.onopen = function (evt) {
        console.log("connection opened");
    };
    conn.onclose = function (evt) {
        console.error("websocket connection closed");
    };
    conn.onmessage = function (evt) {
        console.log(evt.data);
    };
})(window["parle"]);
//# sourceMappingURL=websocket.js.map