((parle: IParle) => {
	if (!window["WebSocket"]) {
		console.error("your browser does not supported, websocket need to be available");
		return;
	}

	// send is used to communicate with the websocket server
	parle.send = (evt: string, data: any) => {
		let d: IParleEvent = { token: parle.token, name: evt, data: "" };

		if (parle.token && conn.readyState == conn.OPEN) {
			conn.send(d);
		}
	}

	let conn = new WebSocket("ws://" + document.location.host + "/rt");
	conn.onopen = (evt: Event) => {
		console.log("connection opened");
	}
	conn.onclose = (evt: CloseEvent) => {
		console.error("websocket connection closed");
	}
	conn.onmessage = (evt: MessageEvent) => {
		let d: IParleEvent = evt.data || { token: "", name: "", data: "" };
		switch (d.name) {
			case "hello":
				parle.token = d.data;

				parle.send("identify", "");
				break;
			case "newconv":
				console.log(e.data);
				break;	
		}
	}


})(window["parle"]);