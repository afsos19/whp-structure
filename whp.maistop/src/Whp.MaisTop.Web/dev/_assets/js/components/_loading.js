/*
|--------------------------------------------------------------------------
| Date
|--------------------------------------------------------------------------
*/

APP.component.Loading = {
  init() {},

  show() {
    $('#modal').load('/_modais/loading.html', function() {
      $('.box-loading')
        .stop(true, true)
        .css('display', 'flex')
        .hide()
        .stop(true, true)
        .fadeIn(200);
    });
  },

  hide() {
    $('.box-loading')
      .stop(true, true)
      .fadeOut(200);
    $('.box-loading').remove();
  }
};
