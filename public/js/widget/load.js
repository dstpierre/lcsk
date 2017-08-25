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
    parle.renderDiscussions = function () {
        var conversations = parle.state.conversations;
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
        var btn = document.getElementById("parle-newconv");
    };
    parle.listConvos = function (conversations) {
    };
})(window["parle"]);
//# sourceMappingURL=load.js.map