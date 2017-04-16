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