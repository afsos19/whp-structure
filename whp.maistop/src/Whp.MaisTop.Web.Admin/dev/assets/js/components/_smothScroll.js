/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.component.SmoothScroll = {
  init: function() {
    this.setup();
  },
  setup: function() {
    this.scrollAnchor();
  }, 

  scrollAnchor: function(elem, time) {
    $(document).on('click', `${elem} a[href="/#"]`, function (event) {
      event.preventDefault();
      
      $('html, body').animate({
        scrollTop: $($.attr(this, 'href')).offset().top
      }, time);
    });
  }

};