/*
|--------------------------------------------------------------------------
| Check Connection
|--------------------------------------------------------------------------
*/

APP.component.CheckConnection = {
  init() {
    this.setup();
    //this.checkConnection();
  },

  setup() {
    window.addEventListener('online', APP.component.CheckConnection.checkConnection);
    window.addEventListener('offline', APP.component.CheckConnection.checkConnection);
  },

  checkConnection() {
    if (!navigator.onLine) {
      $('body').addClass('blockScroll');
      $('body #check-connection')
        .css('display', 'flex')
        .hide()
        .fadeIn(400);
    } else {
      $('body').removeClass('blockScroll');
      $('body #check-connection').fadeOut(400);
    }
  }
};
