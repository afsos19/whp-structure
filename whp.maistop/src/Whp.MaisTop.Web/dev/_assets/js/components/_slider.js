/*
|--------------------------------------------------------------------------
| Slider
|--------------------------------------------------------------------------
*/

APP.component.Slider = {
  init() {
    this.setup();
  },

  setup() {},

  ultimasNovidades() {
    $('.ultimas-novidades-carrossel.owl-carousel').owlCarousel({
      loop: false,
      rewind: true,
      nav: true,
      slideBy: 1,
      stagePadding: 10,
      margin: 50,
      responsive: {
        0: {
          items: 1,
          nav: false,
          dots: false,
          stagePadding: 0,
          margin: 28
        },
        768: {
          items: 2
        },
        1100: {
          items: 3
        },
        1287: {
          items: 1
        },
        1570: {
          items: 2
        }
      }
    });
  },

  sliderHomeHeader() {
    $('.slider-ativo-header').owlCarousel({
      slideBy: 1,
      nav: false,
      dots: false,
      items: 2,
      autoWidth: true,
    });
  },

  sliderNovoMaisTop() {
    $('.container-modal-novoMaisTop').owlCarousel({
      loop: false,
      rewind: true,
      slideBy: 1,
      slideBy: 1,
      nav: true,
      dots: false,
      items: 1
    });
  },

  sliderNovidades(){
    $('.novidades-slider').owlCarousel({
      loop: false,
      rewind: true,
      nav: true,
      slideBy: 1,
      stagePadding: 10,
      margin: 15,
      responsive: {
        0: {
          items: 1,
          nav: false,
          dots: false,
          stagePadding: 0,
          margin: 28,
        },
        600: {
          items: 2,
          margin: 25,
        },
        831: {
          items: 1,
        },
        1070: {
          items: 2,
        },
        1550: {
          items: 3,
        },
      }
    });
  },

  sliderRegrasConvideAmigos(){
    $('.regras-convide-amigos-box').owlCarousel({
      loop: false,
      rewind: true,
      nav: true,
      slideBy: 1,
      stagePadding: 10,
      margin: 15,
      responsive: {
        0: {
          items: 1,
          nav: false,
          dots: false,
          stagePadding: 0,
          margin: 28
        },
        600: {
          items: 2,
          margin: 25
        },
        1250: {
          items: 4
        },
      }
    });
  }
};
