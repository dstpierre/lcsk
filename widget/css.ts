((parle: IParle) => {
	const css = `
		#parle-widget {
			position: fixed;
			right: 25px;
			bottom: 25px;
			width: 75px;
			height: 75px;
			background-color: red;
			color: #fff;
			border-radius: 50%;
		}
		#parle-discussions {
			position: fixed;
			right: 25px;
			bottom: 115px;
			width: 350px;
			height: 650px;
			background-color: black;
			color: #fff;
		}
	`;

	const head = document.head || document.getElementsByTagName('head')[0];
	const style: any = document.createElement('style');

	style.type = 'text/css';
	if (style.styleSheet) {
		style.styleSheet.cssText = css;
	} else {
		style.appendChild(document.createTextNode(css));
	}

	head.appendChild(style);
})(window["parle"]);