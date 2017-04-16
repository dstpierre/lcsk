if (!window["parle"]) { window["parle"] = {}; }

((parle: IParle) => {
	parle.templates = {};
	parle.events = {};

	parle.init = (attributes: IConfiguration) => {
		parle.attributes = attributes;

		parle.state = {
			isOpen: false
		}

		parle.load();
	};
})(window["parle"]);