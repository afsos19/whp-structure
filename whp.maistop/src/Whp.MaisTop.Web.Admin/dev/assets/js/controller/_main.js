/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Main = {
  init() {
    APP.controller.Login.init();
    this.main();
    this.maskInit();
    this.role();
  },

  setup() {

    this.MenuRules();
    APP.controller.Sac.init();
    APP.controller.SacEai.init();
    APP.controller.Sku.init();
    APP.controller.Product.init();
    APP.controller.FocusProduct.init();
    APP.controller.ParticipantProduct.init();
    APP.controller.PreProcessing.init();
    APP.controller.PreProcessingSale.init();
    APP.controller.User.init();
    APP.controller.MirrorAccess.init();
    APP.controller.AcompanhamentoBaseVenda.init();
    APP.controller.AcompanhamentoBaseHierarquia.init();
    APP.controller.PhraseologyCategory.init();
    APP.controller.PhraseologySubject.init();
    APP.controller.PhraseologyTypeSubject.init();
    APP.controller.Phraseology.init();
    APP.controller.TrainingManagerReport.init();
  },
  MenuRules() {

    switch (this.role()) {
      case 8:
        $("#menu-pre-processamento").hide();
        $("#menu-pre-processamento-venda").hide();
        $("#menu-sac-eai").hide();
        if (window.location.href.toUpperCase().indexOf('/RELATORIOS/PREPROCESSAMENTO') !== -1) {
          window.location.href = '/Sac/ListarOcorrencia';
        }
        break;
      case 6:
        $("#menu-usuario").hide();
        $("#menu-produto").hide();
        $("#menu-sac-eai").hide();
        $("#menu-pre-processamento").hide();
        $("#menu-pre-processamento-venda").hide();
        if (window.location.href.toUpperCase().indexOf('/RELATORIOS/PREPROCESSAMENTO') !== -1
          || window.location.href.toUpperCase().indexOf('/USUARIO/') !== -1 ||
          window.location.href.toUpperCase().indexOf('/PRODUTO/') !== -1) {
          window.location.href = '/Sac/ListarOcorrencia';
        }
        break;
      case 7:
        $("#menu-usuario").hide();
        $("#menu-produto").hide();
        $("#menu-fraseologia").hide();
        $("#menu-processamento").hide();
        $("#menu-acesso-espelho").hide();
        $("#menu-pre-processamento").hide();
        $("#menu-pre-processamento-venda").hide();
        $("#menu-sac").hide();
        if (window.location.href.toUpperCase().indexOf('/SACEAI/') === -1 ) {
      
             window.location.href = '/SACEAI/ListarOcorrencia';
        }
        break;
      default:
        $("#menu-sac-eai").hide();
    }

  },
  //Main
  role() {
    const token = sessionStorage.getItem('tokenAdmin');
    var decodedToken = this.parseJwt(token);
    return parseInt(decodedToken.office);
  },
  main() {
    const token = sessionStorage.getItem('tokenAdmin');
    if (!token && window.location.href.toUpperCase().indexOf('LOGIN') === -1) {
      window.location.href = '/Login';
    } else if (token) {
      var decodedToken = this.parseJwt(token);
      if (Date.now() / 1000 < decodedToken.exp) {
        APP.controller.Main.setup();
      } else {
        APP.controller.Main.getUser();
      }
    }
    APP.component.Modal.init();
  },
  getUser: () => {
    var user = sessionStorage.getItem("a");
    var password = sessionStorage.getItem("b");
    if (user && password) {
      APP.controller.Login.getUser(atob(user), atob(password), true);
    };
  },
  parseJwt: function (token) {
    var base64Url = token.split('.')[1];
    var base64 = decodeURIComponent(atob(base64Url).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(base64);
  },
  maskInit: function () {
    $("[isphone]").mask("(00) 00000-0000");
    $("[istel]").mask("(00) 0000-0000");
    $("[isdate]").mask("00/00/0000");
    $("[isnumber]").mask("000.000.000.000.000,00", { reverse: false });
    $("[istime]").mask("00:00");
    $("[iscpf]").mask("000.000.000-00");
    $("[iscep]").mask("00000-000");
  }

  // Funções do scroll
  // funcoesDoScroll() {
  //   $(window).on('scroll', function() {
  //     APP.controller.MenuMobile.fotoProfileMobile();
  //   });
  // },
};
