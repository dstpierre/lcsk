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