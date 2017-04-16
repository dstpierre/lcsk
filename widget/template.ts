((parle: IParle) => {
	const parseTemplate = (html: string) => {
		const re = /<%([^%>]+)?%>/g;
		const reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g;
		let code = 'var r=[];\n';
		let cursor = 0;
		let match;

		const add = (line: string, js?: boolean) => {
			js ? (code += line.match(reExp) ? line + '\n' : 'r.push(' + line + ');\n') :
				(code += line != '' ? 'r.push("' + line.replace(/"/g, '\\"') + '");\n' : '');
			return add;
		}
		while (match = re.exec(html)) {
			add(html.slice(cursor, match.index))(match[1], true);
			cursor = match.index + match[0].length;
		}
		add(html.substr(cursor, html.length - cursor));
		code += 'return r.join("");';
		return new Function(code.replace(/[\r\t\n]/g, ''));
	}

	const widget = `
		<div class="parle-widget">
		<%if (this.isOpen) {%>
			<p>OPEN</p>
		<%} else {%>
			<p>CLOSE</p>
		<%}%>
		</div>
	`;

	const discussions = `
		<div class="parle-container">
			<p>Something here</p>
		</div>
	`;

	parle.templates["widget"] = parseTemplate(widget);
	parle.templates["discussions"] = parseTemplate(discussions);
})(window["parle"]);