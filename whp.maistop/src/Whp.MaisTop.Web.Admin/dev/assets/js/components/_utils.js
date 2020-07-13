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
    const path = window.location.pathname.slice(1).split(".")[0];
    const pathArray = path.split("-");
    for (let i = 0; i < pathArray.length; i++) {
      pathArray[i] =
        pathArray[i].charAt(0).toUpperCase() + pathArray[i].slice(1);
    }
    const controller = pathArray.join("");
    return controller;
  },

  tratarUrl(url) {
    var ambiente = urlBase.substring(0, urlBase.length - 1);
    var ret = ambiente + url;
    return ret;
  },

  getUrlParameter(_parameter) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
      sURLVariables = sPageURL.split("&"),
      sParameterName,
      i;

    for (i = 0; i < sURLVariables.length; i++) {
      sParameterName = sURLVariables[i].split("=");

      if (sParameterName[0] === _parameter) {
        return sParameterName[1] === undefined ? true : sParameterName[1];
      }
    }
  },

 formatReal( int )
{
        var tmp = int+'';
        tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
        if( tmp.length > 6 )
                tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");

        return tmp;
},

  removerCaracteresCpf: cpf => cpf.replace(/\D/g, "")
};
