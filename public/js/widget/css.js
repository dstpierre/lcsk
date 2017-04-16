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