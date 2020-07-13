/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.Campanhas = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/campanhas.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.Campanhas.verificaUrl();
      APP.controller.Campanhas.irParaTopo();
      APP.controller.Main.ativaMenuPage();
    });
  },

  verificaUrl() {
    if (window.location.href.includes('?')) {
      $('[data-campanhas="leitura"]').addClass('active');
      APP.controller.Campanhas.carregarNoticia();
      $('.wrap-campanhas').append('<p id="voltar-ao-topo">Voltar ao topo</p>');
    } else {
      $('[data-campanhas="geral"]').addClass('active');
      APP.controller.Campanhas.carregarCampanha();
    }
  },

  irParaTopo() {
    $('body').on('click', '#voltar-ao-topo', function(e) {
      e.preventDefault();
      $('html, body').animate({ scrollTop: 0 }, 'slow');
    });
  },

  // Todas as novidades
  carregarCampanha() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/novidades/json',
      url: pathApi.campanhas,
      // url: '/json/campanhas/campanhas.json',
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        const pathImg = APP.controller.Api.pathCampanhas();
        for (let i = 0; i < result.length; i++) {
          $('.campanhas-slider').append(`
            <div class="campanhas-slider-item">
              <div class="campanhas-slider-item-img" style="background-image: url('${pathImg}${result[i].thumb}'); background-position: center; background-size: cover;"></div>
              <div class="campanhas-slider-item-textos">
                <h3 class="campanhas-slider-item-textos-title">${result[i].title}</h3>
                <p class="campanhas-slider-item-textos-descricao">${result[i].subTitle}</p>
                <div class="campanhas-slider-item-textos-btns">
                  <a class="btn btn-verde" href="/Campanhas/?id=${result[i].id}">Ver informações</a>
                </div>
              </div>
            </div>
          `);
        }
      },
      error: function(result) {
        $('.campanhas-slider').append(`
          <p style="padding: 0 20px;color: #8d8980;font-size: 1.25rem;">Você não tem nenhuma campanha disponível no momento.</p>
        `);
      },
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
      url: pathApi.campanhas,
      // url: '/json/campanhas/campanhas.json',
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        const pathImg = APP.controller.Api.pathCampanhas();
        const id = window.location.search.slice(4);
        const campanhas = result.filter(function(value) {
          return value.id == id;
        });
        $('[data-campanhas="leitura"]').append(`
        <div class="campanhas-leitura-bg-campanhas"><img src="${pathImg}${campanhas[0].photo}" /></div>
        <div class="container-campanhas-texto">
          <h2 class="campanhas-leitura-titulo">${campanhas[0].title}</h2>
          <div class="campanhas-leitura-texto">
            <div class="campanhas-leitura-texto-explicativo">
              <div class="box-campanhas-leitura-texto">
                <p>${campanhas[0].description}</p>
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
