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
    parle.events["newConvClicked"] = function (e) {
        parle.send("newconv", "");
    };
    var getHeight = function () {
        return Math.max(document.documentElement.clientHeight, document.body.scrollHeight, document.documentElement.scrollHeight, document.body.offsetHeight, document.documentElement.offsetHeight);
    };
})(window["parle"]);
//# sourceMappingURL=events.js.map