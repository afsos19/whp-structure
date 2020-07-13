/*
|--------------------------------------------------------------------------
| Menu Mobile
|--------------------------------------------------------------------------
*/

APP.controller.MenuMobile = {
  init() {
    this.setup();
    this.toggleMenuMobile();
  },

  setup() {},

  fotoProfileMobile() {
    if ($(window).scrollTop() > 210) {
      $('#foto-mobile-header').addClass('activeInHeader');
    } else {
      $('#foto-mobile-header').removeClass('activeInHeader');
    }
  },

  toggleMenuMobile() {
    $('body').on('click', '.menu-hamburger', function() {
      $(this).toggleClass('active');
      $('body').toggleClass('blockScroll');
      $('.menu-principal-superior').toggleClass('menuMobileActive');
    });
  }
};
