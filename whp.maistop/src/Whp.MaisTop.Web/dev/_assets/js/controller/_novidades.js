/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.Novidades = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/novidades.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.Novidades.verificaUrl();
      APP.controller.Novidades.irParaTopo();
      APP.controller.Main.ativaMenuPage();
    });
  },

  verificaUrl() {
    if (window.location.href.includes('?')) {
      $('[data-novidade="leitura"]').addClass('active');
      APP.controller.Novidades.carregarNoticia();
      $('.wrap-novidades').append('<p id="voltar-ao-topo">Voltar ao topo</p>');
    } else {
      $('[data-novidade="geral"]').addClass('active');
      APP.controller.Novidades.carregarNovidades();
    }
  },

  irParaTopo(){
    $('body').on('click', '#voltar-ao-topo', function(e){
      e.preventDefault();
      $('html, body').animate({ scrollTop: 0 }, 'slow');
    });
  },

  // Todas as novidades
  carregarNovidades() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/novidades/json',
      url: pathApi.noticias,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        const pathImg = APP.controller.Api.pathNews();
        for (let i = 0; i < result.length; i++) {
          $('.novidades-slider.owl-carousel').append(`
            <div class="novidades-slider-item">
              <div class="novidades-slider-item-img" style="background-image: url('${pathImg}${result[i].thumb}'); background-position: center; background-size: cover;"></div>
              <div class="novidades-slider-item-textos">
                <h3 class="novidades-slider-item-textos-title">${result[i].title}</h3>
                <p class="novidades-slider-item-textos-descricao">${result[i].subTitle}</p>
                <div class="novidades-slider-item-textos-btns">
                  <a class="btn btn-verde" href="/Novidades/?id=${result[i].id}">Ver informações</a>
                </div>
              </div>
            </div>
            `);
          // <p><span class="icon-heart icon-heart-novidades ${result[i].curtidas ? 'active' : ''}"></span></p>
        }
      },
      error: function(result) {},
      complete: function() {
        APP.component.Slider.sliderNovidades();
      }
    });
  },

  carregarNoticia() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/novidades/json',
      url: pathApi.noticias,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        const pathImg = APP.controller.Api.pathNews();
        const id = window.location.search.slice(4);
        const noticia = result.filter(function(value){
          return value.id == id;
        })
        $('[data-novidade="leitura"]').append(`
        <div class="novidades-leitura-bg-noticia"><img src="${pathImg}${noticia[0].photo}" /></div>
        <div class="container-novidades-texto">
          <h2 class="novidades-leitura-titulo">${noticia[0].title}</h2>
          <div class="novidades-leitura-texto">
            <div class="novidades-leitura-texto-explicativo">
              <div class="box-novidades-leitura-texto">
                <p>${noticia[0].description}</p>
              </div>
            </div>
          </div>
        </div>
        `);
      },
      error: function(result) {},
      complete: function() {
        APP.component.Slider.sliderNovidades();
      }
    });
  }
};
