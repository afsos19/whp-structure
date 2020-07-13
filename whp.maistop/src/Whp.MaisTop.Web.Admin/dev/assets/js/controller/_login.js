/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Login = {
  init() {
    this.setup();
  },

  setup() {
    this.validateLogin();
    this.deslogaUsuario();
  },

  validateLogin: function () {

    const token = sessionStorage.getItem('tokenAdmin');

    if (window.location.href.toUpperCase().indexOf('/sac') !== -1) {
      if (token) {
        window.location.href = '/sac/listarOcorrencia';
        return true;
      }
    }

    $("#btnEntrar").click(function (e) {
      e.preventDefault();
      var campoLogin = $("#txtLogin").val();
      var campoSenha = $("#txtSenha").val();

      if (campoLogin == "" || campoSenha == "") {
        APP.component.Alerta.alerta(
          "Erro",
          "O login ou a senha inseridos estão incorretos."
        );
        return false;
      } else {
        APP.controller.Login.getUser(campoLogin, campoSenha, false);
      }
    });
  },

  saveToken: function (token, cpf) {
    sessionStorage.setItem("tokenAdmin", token);
  },

  deleteToken: function () {
    sessionStorage.removeItem("tokenAdmin");
    sessionStorage.removeItem("a");
    sessionStorage.removeItem("b");
  },

  getUser: function (login, senha, reload) {
    const pathsApi = APP.controller.Api.pathApi();

    var data = {
      cpf: login,
      password: senha
    };

    $.ajax({
      type: "POST",
      url: pathsApi.loginAdmin,
      dataType: "json",
      contentType: "application/json",
      data: JSON.stringify(data),

      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        sessionStorage.setItem("a", btoa(login));
        sessionStorage.setItem("b", btoa(senha));
        APP.controller.Login.logaUsuario(response, reload);
      },

      error: function (response) {
        APP.controller.Login.deleteToken();
        APP.component.Alerta.alerta("Erro", response.responseText);
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });
  },

  logaUsuario: function (response, reload) {
    
    if (!response.token) {
      APP.controller.Login.deleteToken();
      if (!reload)
        APP.component.Alerta.alerta("Erro", "Usuário ou senha incorretos");
      else {
        location.href = "/login";
      }

    }

    APP.controller.Login.saveToken(response.token);

    if (!reload)
      location.href = "/Sac/ListarOcorrencia";

  },

  deslogaUsuario: function () {
    $("#btnDeslogar").on("click", e => {
      e.preventDefault();

      APP.controller.Login.deleteToken();
      location.href = "/login";
    });
  }
};
