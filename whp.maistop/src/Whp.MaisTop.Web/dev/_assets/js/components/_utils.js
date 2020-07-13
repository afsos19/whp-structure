/*
|--------------------------------------------------------------------------
| Utils
|--------------------------------------------------------------------------
*/

APP.component.Utils = {
  init() {
    this.setup();
  },

  setup() {},

  getController() {
    return window.location.pathname
      .slice(1)
      .split('.')[0]
      .replace('/', '');
  },

  tratarUrl(url) {
    var ambiente = urlBase.substring(0, urlBase.length - 1);
    var ret = ambiente + url;
    return ret;
  },

  getUrlParameter(_parameter) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
      sURLVariables = sPageURL.split('&'),
      sParameterName,
      i;

    for (i = 0; i < sURLVariables.length; i++) {
      sParameterName = sURLVariables[i].split('=');

      if (sParameterName[0] === _parameter) {
        return sParameterName[1] === undefined ? true : sParameterName[1];
      }
    }
  },
};
