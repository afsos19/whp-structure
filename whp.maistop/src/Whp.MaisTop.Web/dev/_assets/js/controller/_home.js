/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.Home = {
  init() {
    APP.controller.Home.verificaAcessoEspelho();
    setTimeout(() => {
      const token = sessionStorage.getItem('token');
      if (!token) {
        window.location = '/Login';
      } else {
        APP.component.Acessos.verificaAcesso();
      }
    }, 100);
  },

  verificaAcessoEspelho() {
    if (window.location.href.includes('?')) {
      const token = window.location.search.slice(3);
      const tokenDecode = APP.controller.Home.b64DecodeUnicode(token);
      sessionStorage.setItem('token', tokenDecode);
    } else {
      return false;
    }
  },

  b64DecodeUnicode(str) {
    return decodeURIComponent(
      atob(str)
        .split('')
        .map(function(c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/home.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.Home.ancoraHeaderHome();
      APP.controller.Home.preencheProdutosParticipantes();
      APP.controller.Home.carregarNovidades();
      APP.component.Slider.sliderHomeHeader();
      APP.controller.Main.ativaMenuPage();
    });
  },

  ancoraHeaderHome() {
    $('body').on('click', '[data-click]', function() {
      const valorClick = $(this).data('click');
      const elemTarget = $(`[data-target="${valorClick}"]`);
      const scrollParaObj = elemTarget.offset().top;

      $('html, body')
        .stop()
        .animate({ scrollTop: scrollParaObj - 140 }, 600);
    });
  },

  preencheProdutosParticipantes() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: pathApi.produtosParticipantes,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        let gruposAdicionado = [];
        result.forEach(item => {
          if (gruposAdicionado.indexOf(item.product.categoryProduct.name) > -1 || item.product.categoryProduct.name == 'BUILT IN') {
            return false;
          }
          $('.produtos-participantes-container').append(`
            <div class="produtos-participantes-lista">
            <h3 class="produtos-participantes-lista-item">${item.product.categoryProduct.name}</h3>
            <h4 class="produtos-participantes-lista-pontos">${item.punctuation}</h4>
            </div>
          `);
          gruposAdicionado.push(item.product.categoryProduct.name);
        });
      },
      error: function(result) {}
    });
  },

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
          $('.ultimas-novidades-carrossel.owl-carousel').append(`
            <article class="ultimas-novidades-carrossel-item">
              <picture> <img class="novidades-carrossel-img" src="${pathImg}${result[i].thumb}" /> </picture>
              <div class="ultimas-novidades-carrossel-container">
                <h3 class="ultimas-novidades-carrossel-container-title">${result[i].title}</h3>
                <p class="ultimas-novidades-carrossel-container-text">${result[i].subTitle}</p>
                <footer class="ultimas-novidades-carrossel-footer">
                  <a href="/Novidades/?id=${result[i].id}" rel="noopener noreferrer">Saiba +</a>
                </footer>
              </div>
            </article>
          `);
        }
      },
      error: function(result) {},
      complete: function() {
        APP.component.Slider.ultimasNovidades();
      }
    });
  }
};
