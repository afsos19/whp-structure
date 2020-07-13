/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.EsqueceuSenha = {
  init() {
    this.setup();
    this.verificaTexto();
    this.erroAoEntrar();
    this.countdown();
    APP.component.Modal.closeModalSuperior();
    $(".mensagem-sucesso").hide();
  },

  setup() {
    //
  },

  countdown(segundos) {
    segundos = parseInt(localStorage.getItem("esqueceuSenha")) || segundos;

    function tick() {
      segundos--;
      localStorage.setItem("esqueceuSenha", segundos);
      let contador = document.getElementById("timer");
      let minutosAtuais = parseInt(segundos / 60);
      let segundosAtuais = segundos % 60;
      contador.innerHTML = minutosAtuais + ":" + (segundosAtuais < 10 ? "0" : "") + segundosAtuais;
      if (segundos > 0) {
        setTimeout(tick, 1000);
        $(".enviar-solicitacao").hide();
        $(".mensagem-sucesso").show();
        $(".mensagem-sucesso").addClass("disabled");
        $("#timer").show();
      } else {
        $(".mensagem-sucesso").removeClass("disabled");
        $("#timer").hide();
        $("#cpf").focus();
        $("body").on("click", ".link", function (e) {
          $(".enviar-solicitacao").show();
          $(".mensagem-sucesso").hide();
          $("#cpf").val("");
          $("#cpf").focus();
        });
      }
    }
    tick();
  },

  verificaTexto() {
    $(".sub-title").html(
      `${localStorage.getItem("mensagemEsqueciSenha")}`
    );
  },

  erroAoEntrar() {
    $("body").on("click", "#enviar", function (e) {
      e.preventDefault();

      const cpf = $("#cpf")
        .val()
        .replace(/[^\d]+/g, "");

      if (!cpf) {
        return APP.component.Modal.mensagemModalSuperior(
          "Erro!",
          "Digite o CPF para continuar."
        );
      } else if (!APP.component.Form.validateCpf(cpf)) {
        return APP.component.Modal.mensagemModalSuperior(
          "Erro!",
          "CPF inválido, por favor, verifique."
        );
      }
      const pathApi = APP.controller.Api.pathsApi();
      $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        cache: false,
        url: `${pathApi.esqueceuSenha}/${cpf}`,
        beforeSend: function () {
          APP.component.Loading.show();
        },
        success: function (result) {
          APP.component.Modal.mensagemModalSuperior("Sucesso!", result.message);
          APP.controller.EsqueceuSenha.countdown(300);
          localStorage.setItem("mensagemEsqueciSenha", result.message);
          $(".sub-title").html(`${result.message}`);
        },
        error: function (jqXHR) {
          APP.component.Loading.hide();
          if (jqXHR.status === 404) {
            return APP.component.Modal.mensagemModalSuperior(
              "Atenção!",
              jqXHR.responseText
            );
          } else {
            return APP.component.Modal.mensagemModalSuperior(
              "Erro!",
              "Cadastro não encontrado. Por favor, verifique."
            );
          }
        },
        complete: function (result) {
          APP.component.Loading.hide();
        }
      });
    });
  },
};
