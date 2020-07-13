/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.SenhaExpirada = {
  // token
  t: "",

  init() {
    this.guardaToken();
    APP.component.Modal.closeModalSuperior();
    APP.component.Modal.closeModal();
    this.cadastrarNovaSenha();
    this.verificarSMS();
    this.verificaTexto();
    this.fazerLogin();
    this.countdown();
  },

  guardaToken() {
    APP.controller.SenhaExpirada.t = sessionStorage.getItem("senhaExpirada");
    sessionStorage.removeItem("senhaExpirada");
  },

  cadastrarNovaSenha() {
    $("body").on(
      "click",
      ".btn.btn-laranja.btn-login-internas-enviar",
      function (e) {
        e.preventDefault();
        if (
          APP.component.Form.validateSenha($("#senha").val()) &&
          $("#senha").val() === $("#confirmacao-senha").val()


        ) {
          APP.controller.SenhaExpirada.enviarInformacoes();
        } else {
          APP.component.Modal.mensagemModalSuperior(
            "Erro!",
            "Verifique as senhas digitadas."
          );
        }
      }
    );
  },

  countdown(segundos) {
    segundos = parseInt(localStorage.getItem("expirouSenha")) || segundos;

    function tick() {
      segundos--;
      localStorage.setItem("expirouSenha", segundos);
      let contador = document.getElementById("timer");
      let minutosAtuais = parseInt(segundos / 60);
      let segundosAtuais = segundos % 60;
      contador.innerHTML = minutosAtuais + ":" + (segundosAtuais < 10 ? "0" : "") + segundosAtuais;
      if (segundos > 0) {
        setTimeout(tick, 1000);
        $(".enviar-solicitacao").hide();
        $(".mensagem-sucesso").show();
        $("#timer").show();
        $(".timer-content").addClass("disabled");
      } else {
         $(".timer-content").removeClass("disabled");
        $("#timer").hide();
        $("body").on("click", ".link", function (e) {
          $(".enviar-solicitacao").show();
          $(".mensagem-sucesso").hide();
        });
      }
    }
    tick();
  },

  enviarInformacoes() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = APP.controller.SenhaExpirada.t;
    const novaSenha = {
      password: $("#confirmacao-senha").val()
    };

    localStorage.setItem("novaSenhaExpirada", novaSenha.password);

    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.senhaExpirada,
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "cache-control": "no-cache",
        Authorization: `Bearer ${token}`
      },
      processData: false,
      data: JSON.stringify(novaSenha),
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (result) {
        APP.controller.SenhaExpirada.countdown(300);
        let tel = result.replace(/[^0-9]/g, "");
        localStorage.setItem("mensagemSenhaExpirada", tel);
        $(".phone").html(`${tel}`);
      },
      error: function (jqXHR) {
        APP.component.Loading.hide();
        if (jqXHR.status === 404) {
          APP.component.Modal.mensagemModalSuperior('Atenção!', jqXHR.responseText);
        } else {
          APP.component.Modal.mensagemModalSuperior('Erro!', 'Não conseguimos enviar seu código. Por favor, tente novamente em alguns instantes.');
        }
      },
      complete: function (result) {
        APP.component.Loading.hide();
      }
    });
  },

  verificarSMS() {
    $("body").on("click", "#enviar-sms", function (e) {
      e.preventDefault();
      APP.controller.SenhaExpirada.confirmarSMS();
    });
  },

  verificaTexto() {
    $(".phone").html(`${localStorage.getItem("mensagemSenhaExpirada")}`);
  },

  confirmarSMS() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = APP.controller.SenhaExpirada.t;
    let smsCodigo = [];
    $(".sms input").each(function () {
      smsCodigo.push(
        $(this)
        .val()
        .toUpperCase()
      );
    });
    const novaSenha = {
      password: localStorage.getItem("novaSenhaExpirada")
    };

    const dados = {
      password: novaSenha.password, // SENHA
      token: smsCodigo.join("") // ESSE É O CÓDIGO DE SMS QUE O CARA DIGITOU
    };
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.verificaCodigoSMS,
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "cache-control": "no-cache",
        Authorization: `Bearer ${token}`
      },
      processData: false,
      data: JSON.stringify(dados),
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (result) {
        APP.component.Alert.customMessage("trocaSenhaSucesso");
        sessionStorage.setItem("token", APP.controller.SenhaExpirada.t);
      },
      error: function (result) {
        APP.component.Alert.customMessage("trocaSenhaFalha");
        APP.controller.SenhaExpirada.tentarNovamente();
      },
      complete: function (result) {
        APP.component.Loading.hide();
      }
    });
  },

  tentarNovamente() {
    $("body").on("click", "#modal-tentar-novamente-sms", function () {
      $("#bg-modal").click();
      APP.controller.SenhaExpirada.enviarInformacoes();
    });
  },

  fazerLogin() {
    $("body").on("click", ".btn.btn-verde.btn-login", function (e) {
      window.location = "/Login";
    });
  }
};
