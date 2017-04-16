(function (parle) {
    var parseTemplate = function (html) {
        var re = /<%([^%>]+)?%>/g;
        var reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g;
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
    var discussions = "\n\t\t<div class=\"parle-container\">\n\t\t\t<p>Something here</p>\n\t\t</div>\n\t";
    parle.templates["widget"] = parseTemplate(widget);
    parle.templates["discussions"] = parseTemplate(discussions);
})(window["parle"]);
//# sourceMappingURL=template.js.map