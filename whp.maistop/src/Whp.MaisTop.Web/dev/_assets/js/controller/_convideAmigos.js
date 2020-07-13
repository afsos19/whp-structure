/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.ConvideAmigos = {
  init() {
    const token = sessionStorage.getItem("token");
    if (!token) {
      window.location = "/Login";
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $("#app").load("/_logadas/convide-amigos.html", function () {
      $(".arrow-back").removeClass("back");
      APP.component.Modal.closeModalSuperior();
      APP.component.Acessos.init(result, decoded);
      APP.controller.ConvideAmigos.preencheProdutosParticipantes();
      APP.controller.ConvideAmigos.copiarTextoConvite();
      APP.controller.ConvideAmigos.countdown();
      APP.controller.ConvideAmigos.verificaTexto();
      APP.controller.ConvideAmigos.mudarTelasConvideAmigos();
      APP.controller.ConvideAmigos.voltarTelaConvideAmigos();
      APP.controller.ConvideAmigos.abrirModalRegras();
      APP.controller.ConvideAmigos.trazerCodigDeConvite();
      APP.controller.ConvideAmigos.enviarCodigoSms();
      APP.controller.ConvideAmigos.trazerHistoricoConvites();
      APP.controller.Main.ativaMenuPage();
    });
  },

  trazerCodigDeConvite() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem("token");
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.pegarCodigoDeConvite,
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "cache-control": "no-cache"
      },
      success: function (result) {
        $(".meu-codigo").append(`
            <span>${result}</span>
          `);
      },
      error: function (result) {}
    });
  },

  countdown(segundos) {
    segundos =
      parseInt(localStorage.getItem("mensagemConviteAmigo")) || segundos;

    function tick() {
      segundos--;
      localStorage.setItem("mensagemConviteAmigo", segundos);
      let contador = document.getElementById("timer");
      let minutosAtuais = parseInt(segundos / 60);
      let segundosAtuais = segundos % 60;
      contador.innerHTML =
        minutosAtuais + ":" + (segundosAtuais < 10 ? "0" : "") + segundosAtuais;
      if (segundos > 0) {
        setTimeout(tick, 1000);
        $(".enviar-solicitacao").hide();
        $(".mensagem-sucesso").show();
        $("#timer").show();
        $(".mensagem-sucesso").addClass("disabled");
      } else {
        $(".mensagem-sucesso").removeClass("disabled");
        $("#timer").hide();
        $("body").on("click", ".link", function (e) {
          $(".enviar-solicitacao").show();
          $(".mensagem-sucesso").hide();
        });
      }
    }
    tick();
  },

  enviarCodigoSms() {
    $("body").on("click", ".convide-amigos-enviar-sms", function (e) {
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem("token");
      const data = {
        Cellphone: APP.component.Form.limpaPhone(
          $(".celular-convite-sms").val()
        )
      };
      if ($(".celular-convite-sms").val().length > 13) {
        $.ajax({
          type: "POST",
          url: pathApi.enviarCodigoDeConviteSMS,
          data: JSON.stringify(data),
          cache: false,
          contentType: "application/json",
          headers: {
            Authorization: `Bearer ${token}`
          },
          beforeSend: function () {
            APP.component.Modal.closeModalSuperior();
            APP.component.Loading.show();
          },
          success: function (result) {
            APP.controller.ConvideAmigos.countdown(300);
            let tel = result.replace(/[^0-9]/g, "");
            localStorage.setItem("telefoneConviteAmigo", tel);
            $(".phone").html(`${tel}`);
          },
          error: function (jqXHR) {
            APP.component.Loading.hide();
            if (jqXHR.status == 404) {
              APP.component.Modal.mensagemModalSuperior(
                "Atenção!",
                jqXHR.responseText,
                false
              );
            } else {
              return APP.component.Modal.mensagemModalSuperior(
                "Erro!",
                "Não conseguimos enviar seu convite. Por favor, tente novamente em alguns instantes.",
                false
              );
            }
          },
          complete: function (result) {
            APP.component.Loading.hide();
          }
        });
      }
    });
  },

  verificaTexto() {
    $(".phone").html(`${localStorage.getItem("telefoneConviteAmigo")}`);
  },

  preencheProdutosParticipantes() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem("token");
    $.ajax({
      type: "GET",
      dataType: "json",
      contentType: "application/json",
      url: pathApi.produtosParticipantes,
      cache: false,
      headers: {
        Authorization: `Bearer ${token}`
      },
      success: function (result) {
        let gruposAdicionado = [];
        result.forEach(item => {
          if (
            gruposAdicionado.indexOf(item.product.categoryProduct.name) > -1 ||
            item.product.categoryProduct.name == "BUILT IN"
          ) {
            return false;
          }
          $(".produtos-participantes-container").append(`
            <div class="produtos-participantes-lista">
            <h3 class="produtos-participantes-lista-item">${
              item.product.categoryProduct.name
            }</h3>
            <h4 class="produtos-participantes-lista-pontos">${
              item.punctuation
            }</h4>
            </div>
          `);
          gruposAdicionado.push(item.product.categoryProduct.name);
        });
      },
      error: function (result) {}
    });
  },

  trazerHistoricoConvites() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem("token");
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.historicoDeConvites,
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "cache-control": "no-cache"
      },
      success: function (result) {
        result.forEach(item => {
          let iconError = `<img src="/_assets/img/convide-amigos/icon-convide-amigos-erro.svg" alt="MaisTop" />`;
          let iconSucesso = `<img src="/_assets/img/convide-amigos/icon-convide-amigos-cadastrado.svg" alt="MaisTop" />`;
          let icon = item.result.gotPunctuation ? iconSucesso : iconError;
          $(".convide-amigos-progresso-tabela-conteudo").append(`
            <div class="convide-amigos-progresso-tabela-item">
               <p class="convide-amigos-progresso-itens-nome">${
                 item.result.name
               }</p>
               <p class="convide-amigos-progresso-itens-indicacao">${
                 item.result.invitedAt
               }</p>
               <p class="convide-amigos-progresso-itens-meio">Código Externo</p>
               <p class="convide-amigos-progresso-itens-cadastro">${
                 item.result.createdAt
               }</p> 
               <p class="convide-amigos-progresso-itens-credito">${icon}</p>
            </div>`);
        });
      },
      error: function (result) {}
    });
  },

  copiarTextoConvite() {
    $("body").on("click", ".convide-amigos-copiar-codigo-btn", function (e) {
      e.preventDefault();
      const $temp = $("<input>");
      $("body").append($temp);
      $temp.val($("#toClipboard").text()).select();
      document.execCommand("copy");
      $temp.remove();
      APP.component.Modal.closeModalSuperior();
      return APP.component.Modal.mensagemModalSuperior(
        "Sucesso.",
        "O texto foi copiado."
      );
    });
  },

  mudarTelasConvideAmigos() {
    $("body").on("click", "[data-convide-amigos-btn]", function () {
      const target = $(this).data("convide-amigos-btn");
      $("[data-convide-amigos]").removeClass("active");
      $(`[data-convide-amigos="${target}"]`).addClass("active");
    });
  },

  voltarTelaConvideAmigos() {
    $("body").on("click", ".arrow-back", function (e) {
      e.preventDefault();
      $("[data-convide-amigos]").removeClass("active");
      $('[data-convide-amigos="home"]').addClass("active");
    });
  },

  abrirModalRegras() {
    $("body").on("click", "#btn-ver-regras", function () {
      APP.component.Modal.closeModal();
      APP.component.Alert.modalConvideAmigos("regrasConvideAmigos");
    });
  }
};
