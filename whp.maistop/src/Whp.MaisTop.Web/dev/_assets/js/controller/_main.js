/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Main = {
  init() {
    this.main();
    APP.component.SidebarMenu.init();
    APP.controller.MenuMobile.init();
    this.funcoesDoScroll();
    this.maskInit();
  },

  main() {
    APP.component.CheckConnection.init();
    this.setClickToTop();
    this.setClickBack();
  },

  maskInit: function() {
    $('[isphone]').mask('(00) 00000-0000');
    $('[istel]').mask('(00) 0000-00000');
    $('[isdate]').mask('00/00/0000');
    $('[isnumber]').mask('000.000.000.000.000,00', { reverse: false });
    $('[istime]').mask('00:00');
    $('[iscpf]').mask('000.000.000-00');
    $('[iscep]').mask('00000-000');
    $('[isnum]').mask('000000');
  },

  setClickToTop() {
    $('body').on('click', '.btn-backtotop', function(e) {
      e.preventDefault();
      $('html, body').animate(
        {
          scrollTop: 0
        },
        500
      );
    });
  },

  setClickBack() {
    $('body').on('click', '.back', function(event) {
      event.preventDefault();
      window.history.back();
    });
  },

  // Ativa link do menu superior da pagina atual
  ativaMenuPage() {
    const page = APP.component.Utils.getController();
    $(`[data-menu-header="${page == '' ? 'Home' : page}"] a`).addClass('active');
  },

  // Funções do scroll
  funcoesDoScroll() {
    $(window).on('scroll', function() {
      APP.controller.MenuMobile.fotoProfileMobile();
    });
  },
};
