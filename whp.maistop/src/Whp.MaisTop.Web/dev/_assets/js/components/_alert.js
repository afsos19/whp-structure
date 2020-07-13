/*
|--------------------------------------------------------------------------
| Alert
|--------------------------------------------------------------------------
*/

APP.component.Alert = {
  init() {
    this.setup();
  },

  setup() {},

  // MODAL MENSAGEM CUSTOMIZADA
  customMessage(element) {
    $('#modal').load(`/_modais/${element}.html`, function() {
      $('body').addClass('blockScroll');
      $('#modal').addClass('box-modal-ativo');
      $('.modal').show();
      $('#bg-modal').addClass('active');
    });
  },

  // MODAL CONVIDE AMIGOS
  modalConvideAmigos(element) {
    $('#modal').load(`/_modais/${element}.html`, function() {
      $('body').addClass('blockScroll');
      $('#modal').addClass('box-modal-ativo');
      $('.modal').show();
      $('#bg-modal').addClass('active');
      $('.regras-convide-amigos-topo, .regras-convide-amigos-box.owl-carousel, .regras-convide-amigos-rodape').fadeIn('slow');
      APP.component.Slider.sliderRegrasConvideAmigos();
    });
  }
};
