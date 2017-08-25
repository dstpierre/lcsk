if (!window["parle"]) {
    window["parle"] = {};
}
(function (parle) {
    parle.templates = {};
    parle.events = {};
    parle.init = function (attributes) {
        parle.attributes = attributes;
        parle.state = {
            isOpen: false
        };
        parle.load();
    };
})(window["parle"]);
//# sourceMappingURL=_init.js.map
(function (parle) {
    var css = "\n\t\t#parle-widget {\n\t\t\tposition: fixed;\n\t\t\tright: 25px;\n\t\t\tbottom: 25px;\n\t\t\twidth: 75px;\n\t\t\theight: 75px;\n\t\t\tbackground-color: red;\n\t\t\tcolor: #fff;\n\t\t\tborder-radius: 50%;\n\t\t}\n\t\t#parle-discussions {\n\t\t\tposition: fixed;\n\t\t\tright: 25px;\n\t\t\tbottom: 115px;\n\t\t\twidth: 350px;\n\t\t\theight: 650px;\n\t\t\tbackground-color: black;\n\t\t\tcolor: #fff;\n\t\t}\n\t";
    var head = document.head || document.getElementsByTagName('head')[0];
    var style = document.createElement('style');
    style.type = 'text/css';
    if (style.styleSheet) {
        style.styleSheet.cssText = css;
    }
    else {
        style.appendChild(document.createTextNode(css));
    }
    head.appendChild(style);
})(window["parle"]);
//# sourceMappingURL=css.js.map
(function (parle) {
    parle.events["widgetClicked"] = function (e) {
        e.preventDefault();
        parle.state.isOpen = !parle.state.isOpen;
        var wc = document.getElementById("parle-widget");
        wc.innerHTML = parle.templates["widget"].apply(parle.state);
        var dc = document.getElementById("parle-discussions");
        dc.innerHTML = parle.templates["discussions"].apply(parle.state);
        dc.style.height = (getHeight() - 125) + "px";
        dc.style.display = parle.state.isOpen ? "block" : "none";
    };
    var getHeight = function () {
        return Math.max(document.documentElement.clientHeight, document.body.scrollHeight, document.documentElement.scrollHeight, document.body.offsetHeight, document.documentElement.offsetHeight);
    };
})(window["parle"]);
//# sourceMappingURL=events.js.map
if (!window["parle"]) {
    window["parle"] = {};
}
(function (parle) {
    parle.init = function (attributes) {
        parle.attributes = attributes;
        parle.state = {
            isOpen: false
        };
        parle.load();
    };
})(window["parle"]);
//# sourceMappingURL=init.js.map
(function (parle) {
    parle.load = function () {
        var body = document.body;
        var container = document.createElement("div");
        container.id = "parle";
        var wc = document.createElement("div");
        wc.id = "parle-widget";
        wc.innerHTML = parle.templates["widget"].apply(parle.state);
        wc.addEventListener("click", parle.events["widgetClicked"], false);
        var dc = document.createElement("div");
        dc.id = "parle-discussions";
        dc.innerHTML = parle.templates["discussions"].apply(parle.state);
        dc.style.display = "none";
        container.appendChild(wc);
        container.appendChild(dc);
        body.appendChild(container);
    };
    parle.open = function () {
        if (parle.state.isOpen) {
            return;
        }
    };
    parle.listConvos = function (conversations) {
        if (conversations == parle.state.conversations) {
            return;
        }
        if (parle.eventHandlers && parle.eventHandlers.length > 0) {
            parle.eventHandlers.forEach(function (h) {
                var el = document.getElementById(h.id);
                if (el) {
                    el.removeEventListener(h.event, h.handler);
                }
            });
        }
        var content = document.getElementById("parle-content");
        content.innerHTML = parle.templates["discussions"].apply(parle.state);
    };
})(window["parle"]);
//# sourceMappingURL=load.js.map
(function (parle) {
    var parseTemplate = function (html) {
        var re = /<%(.+)?%>/g;
        var reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g;
        var subExp = /#([^#+])?#/;
        var code = 'var r=[];\n';
        var cursor = 0;
        var match;
        var add = function (line, js) {
            js ? (code += line.match(reExp) ? line + '\n' : 'r.push(' + line + ');\n') :
                (code += line != '' ? 'r.push("' + line.replace(/"/g, '\\"') + '");\n' : '');
            return add;
        };
        while (match = re.exec(html)) {
            add(html.slice(cursor, match.index))(match[1], true);
            cursor = match.index + match[0].length;
        }
        add(html.substr(cursor, html.length - cursor));
        code += 'return r.join("");';
        console.log(code);
        return new Function(code.replace(/[\r\t\n]/g, ''));
    };
    var widget = "\n\t\t<div class=\"parle-widget\">\n\t\t<%if (this.isOpen) {%>\n\t\t\t<p>OPEN</p>\n\t\t<%} else {%>\n\t\t\t<p>CLOSE</p>\n\t\t<%}%>\n\t\t</div>\n\t";
    var discussions = "\n\t\t<div class=\"parle-container\">\n\t\t\t<div class=\"parle-header\">\n\t\t\t\tHeader <%this.isOpen%>\n\t\t\t</div>\n\t\t\t<div id=\"parle-content\" class=\"parle-content\">\n\t\t\t<p><button id=\"parle-newconv\">Start a new conversation</button></p>\n\t\t\t<%if (this.conversations && this.conversations.length > 0) {%>\n\t\t\t\t<div class=\"parle-conversations\">\n\t\t\t\t\t<%for (var i = 0; i < this.conversations.length; i++) {%>\n\t\t\t\t\t\t<div id=\"parle-convo-$this.conversations[i].id$\">\n\t\t\t\t\t\t\tconv $this.conversations[i].id$\n\t\t\t\t\t\t</div>\n\t\t\t\t\t<%}%>\n\t\t\t\t</div>\n\t\t\t<%} else {%>\n\t\t\t\t<p>You do not have any conversation with us so far.</p>\n\t\t\t<%}%>\n\t\t\t</div>\n\t\t</div>\n\t";
    parle.templates["widget"] = parseTemplate(widget);
    parle.templates["discussions"] = parseTemplate(discussions);
})(window["parle"]);
//# sourceMappingURL=template.js.map
(function (parle) {
    if (!window["WebSocket"]) {
        console.error("your browser does not supported, websocket need to be available");
        return;
    }
    parle.send = function (d) {
        if (parle.token && conn.readyState == conn.OPEN) {
            d.token = parle.token;
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
                break;
        }
    };
})(window["parle"]);
//# sourceMappingURL=websocket.js.map