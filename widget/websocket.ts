((parle: IParle) => {
	if (!window["WebSocket"]) return;

	let conn = new WebSocket("ws://" + document.location.host + "/rt");
	conn.onopen = (evt: Event) => {
		console.log("connection opened");
	}
	conn.onclose = (evt: CloseEvent) => {
		console.error("websocket connection closed");
	}
	conn.onmessage = (evt: MessageEvent) => {
		console.log(evt.data);
	}
})(window["parle"]);