(function (parle) {
    if (!window["WebSocket"]) {
        console.error("your browser does not supported, websocket need to be available");
        return;
    }
    parle.send = function (evt, data) {
        var d = { token: parle.token, name: evt, data: "" };
        if (parle.token && conn.readyState == conn.OPEN) {
            conn.send(d);
        }
    };
    var conn = new WebSocket("ws://" + document.location.host + "/rt");
    conn.onopen = function (evt) {
        console.log("connection opened");
    };
    conn.onclose = function (evt) {
        console.error("websocket connection closed");
    };
    conn.onmessage = function (evt) {
        var d = evt.data || { token: "", name: "", data: "" };
        switch (d.name) {
            case "hello":
                parle.token = d.data;
                parle.send("identify", "");
                break;
            case "newconv":
                console.log(e.data);
                break;
        }
    };
})(window["parle"]);
//# sourceMappingURL=websocket.js.map