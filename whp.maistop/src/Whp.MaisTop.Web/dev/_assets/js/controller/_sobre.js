/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.Sobre = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/sobre.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.Sobre.conteudoPerfil(decoded);
      APP.controller.Sobre.pdfRegulamento();
      APP.controller.Main.ativaMenuPage();
    });
  },

  conteudoPerfil(decoded) {
    const perfil = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    switch (perfil) {
      case 'VENDEDOR':
        $('.container-video').append(`
          <div class="wrap-video">
            <iframe src="https://www.youtube.com/embed/EZuGI0NUOFU"></iframe>
          </div>
        `);
        break;
      case 'GERENTE':
        $('.container-video').append(`
          <div class="wrap-video">
            <iframe src="https://www.youtube.com/embed/G-9xlT40cU0"></iframe>
          </div>
        `);
        break;
      case 'GERENTE REGIONAL':
        $('.container-video').append(`
          <div class="wrap-video">
            <iframe src="https://www.youtube.com/embed/G-9xlT40cU0"></iframe>
          </div>
        `);
        break;
      case 'GESTOR DA INFORMACAO':
        $('.container-video').append(`
          <picture>
            <source srcset="/_assets/img/sobre/sobre-gestor-informacao-mobile.png" media="(max-width: 1280px)">
            <img src="/_assets/img/sobre/sobre-gestor-informacao-desk.png" />
          </picture>
        `);
        // $('.download-sobre').remove();
        break;
    }
  },

  pdfRegulamento() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      cache: false,
      url: pathApi.pegarLoja,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        const rede = result[0].network.siteShortName;
        $('#pdf-regulamento').attr('href', `${APP.controller.Api.pathRegulamentos()}Regulamento-Programa-maistop_${rede.replace(/\s/g, '')}.pdf`);
      },
      error: function(result) {}
    });
  }
};
