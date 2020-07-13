/*
|--------------------------------------------------------------------------
| Acessos
|--------------------------------------------------------------------------
*/

APP.component.Acessos = {
  init(result, decoded) {
    APP.component.SidebarMenu.preencheInfosLaterais(result);
    APP.component.SidebarMenu.preenchePontuacaoLateral(result);
    APP.component.Acessos.retornaLogoRedeUsuario(decoded);
    APP.component.Acessos.setPerfilUsuario(result);
  },

  verificaAcesso() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const decoded = jwt_decode(token);
    let url = window.location.pathname;
    url == '/' ? (url = 'Home') : (url = url.replace(/\//g, ''));
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: `${pathApi.getUser}/${decoded.id}`,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        APP.controller[url].acessoPermitido(result, decoded);
        APP.component.Acessos.catalogoDePremios();
        APP.component.Acessos.treinamentoAcademia();
        //
        APP.component.Acessos.verificaSeAceitouRegulamento(result);
        APP.component.Acessos.aceiteRegulamento();
      },
      error: function(result) {
        window.location = '/Login';
      }
    });
  },

  linkExternoCampanhaTreinamento(result) {
    if (result.responseText != '') {
      const url = result.responseText;
      window.open(url, '_blank');
    } else if (result.status == 401) {
      APP.component.Modal.closeModalSuperior();
      return APP.component.Modal.mensagemModalSuperior('Erro', 'Sessão expirada. Faça o login novamente.');
    } else {
      APP.component.Modal.closeModalSuperior();
      return APP.component.Modal.mensagemModalSuperior('Erro', 'Caso o erro persista, ligue para 0800 780 0606 e fale com um de nossos atendentes.');
    }
  },

  catalogoDePremios() {
    $('body').on('click', '[data-menu-header="CatalogoDePremios"] a, #resgate-sidebar-right', function(e) {
      e.preventDefault();
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      $.ajax({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        url: pathApi.catalogoDePremios,
        cache: false,
        headers: { Authorization: `Bearer ${token}` },
        success: function(result) {
          if (result.status == 404) {
            window.location.href = '/PaginaForaDoAr';
          } else {
            APP.component.Acessos.linkExternoCampanhaTreinamento(result);
          }
        },
        error: function(result) {
          if (result.status == 404) {
            window.location.href = '/PaginaForaDoAr';
          } else {
            APP.component.Acessos.linkExternoCampanhaTreinamento(result);
          }
        }
      });
    });
  },

  treinamentoAcademia() {
    $('body').on('click', '[data-menu-header="Treinamentos"] a', function(e) {
      e.preventDefault();
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      $.ajax({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        url: pathApi.treinamentoAcademia,
        cache: false,
        headers: { Authorization: `Bearer ${token}` },
        success: function(result) {
          APP.component.Acessos.linkExternoCampanhaTreinamento(result);
        },
        error: function(result) {
          APP.component.Acessos.linkExternoCampanhaTreinamento(result);
        }
      });
    });
  },

  retornaLogoRedeUsuario(rede) {
    const path = APP.controller.Api.pathRedes();
    $('.logo-empresa img').attr('src', `${path}rede${rede.network}.jpg`);
  },

  setPerfilUsuario(result) {
    switch (result.office.description) {
      case 'VENDEDOR':
        $('body').addClass('perfil-vendedor');
        break;
      case 'GERENTE':
        $('body').addClass('perfil-gerente');
        break;
      case 'GERENTE REGIONAL':
        $('body').addClass('perfil-gr');
        break;
      case 'GESTOR DA INFORMACAO':
        $('body').addClass('perfil-gi');
        break;
    }
  },

  verificaSeAceitouRegulamento(user) {
    if (!user.privacyPolicy) {
      APP.component.Acessos.pdfRegulamento();
    } else if (user.privacyPolicy) {
      APP.component.Modal.criarCamposPesquisa();
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
        APP.component.Modal.closeModalRegulamento();
        APP.component.Alert.customMessage('aceiteRegulamento');
        const rede = result[0].network.siteShortName;
        setTimeout(() => {
          $('#download-regulamento-modal').attr('href', `${APP.controller.Api.pathRegulamentos()}Regulamento-Programa-maistop_${rede.replace(/\s/g, '')}.pdf`);
        }, 300);
      },
      error: function(result) {}
    });
  },

  aceiteRegulamento() {
    $('body').on('click', '#btn-aceitar-regulamento', function() {
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      setTimeout(() => {
        APP.component.Alert.customMessage('novoMaisTop');
      }, 300);
      setTimeout(() => {
        APP.component.Modal.closeModal();
        APP.component.Slider.sliderNovoMaisTop();
      }, 500);
      $.ajax({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        url: `${pathApi.aceiteRegulamento}${true}`,
        headers: { Authorization: `Bearer ${token}` }
      });
    });
  }
};
