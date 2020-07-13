/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.CadastroConvidado = {
  // token
  t: "",

  init() {
    this.guardaToken();
    APP.component.Modal.closeModalSuperior();
    APP.component.Modal.closeModal();
    this.enviarInformacoes();
  },

  guardaToken() {
    APP.controller.CadastroConvidado.t = sessionStorage.getItem(
      "CadastroConvidado"
    );
    sessionStorage.removeItem("CadastroConvidado");
  },

  enviarInformacoes() {
    $("body").on("click", ".btn.btn-verde-escuro", function (e) {
      const pathApi = APP.controller.Api.pathsApi();
      const token = APP.controller.CadastroConvidado.t;
      const user = {
        Cellphone: $("#celular").val(),
        Cpf: $("#cpf").val().replace(/[^\d]+/g, ''),
        Code: $("#codigo").val().toUpperCase()
      };
      $.ajax({
        async: true,
        crossDomain: true,
        url: pathApi.EnviarUsuarioConvidado,
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "cache-control": "no-cache",
          Authorization: `Bearer ${token}`
        },
        processData: false,
        data: JSON.stringify(user),
        beforeSend: function () {
          APP.component.Loading.show();
        },
        success: function (result) {
          APP.component.Alert.customMessage("cadastroConvidadoSucesso");
          sessionStorage.setItem("token", APP.controller.SenhaExpirada.t);
        },
        error: function (jqXHR) {
          APP.component.Loading.hide();
          if (jqXHR.status === 404 || jqXHR.status === 400) {
            APP.component.Modal.mensagemModalSuperior('Atenção!', jqXHR.responseText, false);
          } else {
            APP.component.Modal.mensagemModalSuperior(
              "Erro!",
              "Não conseguimos enviar suas informações. Por favor, tente novamente em alguns instantes.",
              false
            );
          }
        },
        complete: function (result) {
          APP.component.Loading.hide();
        }
      });
    });
  }
};
