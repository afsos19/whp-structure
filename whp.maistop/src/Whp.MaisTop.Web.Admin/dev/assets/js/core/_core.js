/*
|--------------------------------------------------------------------------
| Core
|--------------------------------------------------------------------------
*/

APP.core.Main = {
  init() {
    APP.controller.General.init();
    this.loadPageController();
  },

  loadPageController() {
    var ctrl = APP.component.Utils.getController();
    // ctrl == '' ? APP.controller.Home.init() : APP.controller[ctrl].init();
  }
};

// Chamada
$(document).ready(function () {
  APP.core.Main.init();
});
