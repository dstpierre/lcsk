var parle = parle || {};

(() => {
  parle.init = (attributes) => {
    parle.attributes = attributes;
  };

  var d = document.createElement("script");
  d.src = "/js/load.js?x=" + (new Date().getTime());
  document.body.appendChild(d);
})();