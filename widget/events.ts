((parle: IParle) => {
	parle.events["widgetClicked"] = (e: MouseEvent) => {
		e.preventDefault();

		parle.state.isOpen = !parle.state.isOpen;

		let wc = document.getElementById("parle-widget");
		wc.innerHTML = parle.templates["widget"].apply(parle.state);

		let dc = document.getElementById("parle-discussions");
		dc.innerHTML = parle.templates["discussions"].apply(parle.state);
		dc.style.height = (getHeight() - 125) + "px";
		dc.style.display = parle.state.isOpen ? "block" : "none";
	}

	parle.events["newConvClicked"] = (e: MouseEvent) => {
		parle.send("newconv", "");
	}

	const getHeight = (): number => {
		return Math.max(
			document.documentElement.clientHeight,
			document.body.scrollHeight,
			document.documentElement.scrollHeight,
			document.body.offsetHeight,
			document.documentElement.offsetHeight
		);
	};
})(window["parle"]);