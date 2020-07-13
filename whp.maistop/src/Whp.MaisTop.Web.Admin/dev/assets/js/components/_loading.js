/*
|--------------------------------------------------------------------------
| Date
|--------------------------------------------------------------------------
*/

APP.component.Loading = {
  init() {},

  show() {
    $(".box-loading").fadeIn('fast');
  },

  hide() {
    $('.box-loading')
      .stop(true, true)
      .fadeOut(200);
    // $('.box-loading').remove();
  }
};
