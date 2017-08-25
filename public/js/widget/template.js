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
        return new Function(code.replace(/[\r\t\n]/g, ''));
    };
    var widget = "\n\t\t<div class=\"parle-widget\">\n\t\t<%if (this.isOpen) {%>\n\t\t\t<p>OPEN</p>\n\t\t<%} else {%>\n\t\t\t<p>CLOSE</p>\n\t\t<%}%>\n\t\t</div>\n\t";
    var discussions = "\n\t\t<div class=\"parle-container\">\n\t\t\t<div class=\"parle-header\">\n\t\t\t\tHeader <%this.isOpen%>\n\t\t\t</div>\n\t\t\t<div id=\"parle-content\" class=\"parle-content\">\n\t\t\t<p><button id=\"parle-newconv\">Start a new conversation</button></p>\n\t\t\t<%if (this.conversations && this.conversations.length > 0) {%>\n\t\t\t\t<div class=\"parle-conversations\">\n\t\t\t\t\t<%for (var i = 0; i < this.conversations.length; i++) {%>\n\t\t\t\t\t\t<div id=\"parle-convo-$this.conversations[i].id$\">\n\t\t\t\t\t\t\tconv $this.conversations[i].id$\n\t\t\t\t\t\t</div>\n\t\t\t\t\t<%}%>\n\t\t\t\t</div>\n\t\t\t<%} else {%>\n\t\t\t\t<p>You do not have any conversation with us so far.</p>\n\t\t\t<%}%>\n\t\t\t</div>\n\t\t</div>\n\t";
    parle.templates["widget"] = parseTemplate(widget);
    parle.templates["discussions"] = parseTemplate(discussions);
})(window["parle"]);
//# sourceMappingURL=template.js.map