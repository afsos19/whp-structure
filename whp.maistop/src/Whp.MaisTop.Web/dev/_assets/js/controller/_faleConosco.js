/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.FaleConosco = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/fale-conosco.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.FaleConosco.ativaPrimeiroCampo();
      APP.controller.FaleConosco.ativarFaqDuvidas();
      APP.controller.FaleConosco.ativarFaqHome();
      APP.controller.FaleConosco.abrePergunta();
      APP.controller.FaleConosco.ativarFaqFormulario();
      APP.controller.FaleConosco.abreOcorrencias();
      APP.controller.FaleConosco.ativarChamadaMensagens();
      APP.controller.FaleConosco.mudarTabsChamadas();
      APP.controller.FaleConosco.infoFileAbertura();
      APP.controller.FaleConosco.infoFileAtualizacao();
      //Chamdados
      APP.controller.FaleConosco.voltarParaListaChamadas();
      APP.controller.FaleConosco.ativaCampoNovaMensagem();
      APP.controller.FaleConosco.mostraBarraMensagem();
      APP.controller.FaleConosco.abreModalAnexo();
      APP.controller.FaleConosco.fechaModalAnexo();
      //Interações
      APP.controller.FaleConosco.liberaCampoOcorrencia();
      APP.controller.FaleConosco.enviarNovaOcorrencia();
      APP.controller.FaleConosco.mensagemOcorrenciaExistente();
      APP.controller.FaleConosco.abrirOcorrencia();
      APP.controller.FaleConosco.atualizarOcorrencia();
      APP.controller.Main.ativaMenuPage();
    });
  },

  ativaPrimeiroCampo() {
    $('[data-faq="home"]').addClass('active');
  },

  ativarFaqDuvidas() {
    $('body').on('click', '.faq-click-duvidas', function() {
      $('[data-faq="home"]').removeClass('active');
      $('[data-faq="duvidas"]').addClass('active');
      $('[data-pergunta]').addClass('visible');
    });
  },

  ativarFaqHome() {
    $('.faq-duvidas-topo .arrow-back').removeClass('back');
    $('body').on('click', '.arrow-back', function() {
      $('[data-faq="duvidas"]').removeClass('active');
      $('[data-faq="home"]').addClass('active');
      $('[data-pergunta]').removeClass('visible');
      $('[data-pergunta]').removeClass('active');
      $('[data-resposta-item]').slideUp();
    });
  },

  abrePergunta() {
    const perguntas = $('[data-pergunta]');
    perguntas.each(function() {
      $(this).on('click', function() {
        if (!$('.faq-duvidas').hasClass('active')) {
          return false;
        }
        const pergunta = $(this).data('pergunta');
        $(this).toggleClass('active');
        $(`[data-resposta-item="${pergunta}"]`)
          .stop()
          .slideToggle('fast');
      });
    });
  },

  ativarFaqFormulario() {
    $('.faq-contato-ocorrencias .arrow-back').removeClass('back');
    $('body').on('click', '.faq-contato-ocorrencias.active .arrow-back', function() {
      $('[data-form-contato]').removeClass('active');
      $('[data-form-contato="formulario"]').addClass('active');
    });
  },

  voltarParaListaChamadas() {
    $('.faq-contato-mensagens .arrow-back').removeClass('back');
    $('body').on('click', '.faq-contato-mensagens.active .arrow-back', function() {
      $('[data-form-contato]').removeClass('active');
      $('[data-form-contato="ocorrencias"]').addClass('active');
    });
  },

  ativarChamadaMensagens() {
    $('body').on('click', '.list-item-chamado', function() {
      $('[data-form-contato]').removeClass('active');
      $('[data-form-contato="mensagens"]').addClass('active');
      $('.container-historico-mensagem').html('');
      const id = $(this).data('id-ocorrencia');
      const status = $(this).data('status');
      const chamado = $(this)
        .find('.chamado')
        .text();
      $('#enviar-mensagem').data('id-ocorrencia', id);

      $('#numero-chamado-mensagem').text(chamado);
      $('.status-chamado').text(`- ${status}`);
      if (status == 'Fechado') {
        $('.faq-contato-mensagens').removeClass('container-aberto');
        $('.faq-contato-mensagens').addClass('container-fechado');
      } else if (status == 'Em andamento') {
        $('.faq-contato-mensagens').removeClass('container-fechado');
        $('.faq-contato-mensagens').addClass('container-aberto');
      }
      APP.controller.FaleConosco.historicoMensagens(id);
    });
  },

  abreOcorrencias() {
    $('body').on('click', '.faq-contato-form-abrir-chamado-botao', function() {
      APP.controller.FaleConosco.trazerOcorrenciasUsuario();
      $('[data-form-contato]').removeClass('active');
      $('[data-form-contato="ocorrencias"]').addClass('active');
    });
  },

  mudarTabsChamadas() {
    const abasChamadas = $('[data-chamadas]');
    abasChamadas.each(function() {
      $(this).on('click', function() {
        const abaSelecionada = $(this).data('chamadas');
        $('[data-chamadas]').removeClass('active');
        $('[data-chamadas-lista]').removeClass('active');
        $(this).addClass('active');
        $(`[data-chamadas-lista="${abaSelecionada}"]`).addClass('active');
      });
    });
  },

  infoFileAbertura() {
    $('body').on('change', '#arquivo', function() {
      if ($('#arquivo')[0].files[0]) {
        $('#label-input-file').addClass('upload');
        $('#input-file-name').addClass('upload');
        $('#input-file-name').text($('#arquivo')[0].files[0].name);
      } else {
        $('#label-input-file').removeClass('upload');
        $('#input-file-name').removeClass('upload');
        $('#input-file-name').text('Arquivos aceitos: .png, .jpg e .jpeg');
      }
    });
  },

  infoFileAtualizacao() {
    $('body').on('change', '#arquivo-anexo', function() {
      if ($('#arquivo-anexo')[0].files[0]) {
        $('.nova-mensagem-barra').addClass('upload');
        $('#input-file-name-atualizacao').text($('#arquivo-anexo')[0].files[0].name);
      } else {
        $('.nova-mensagem-barra').removeClass('upload');
        $('#input-file-name-atualizacao').text('Arquivos aceitos: .png, .jpg e .jpeg');
      }
    });
  },

  ativaCampoNovaMensagem() {
    $('body').on('click', '#digite-aqui-mensagem', function() {
      $('.historico-mensagem').addClass('active');
      $('.nova-mensagem-barra').addClass('active');
      $('.container-enviar-nova-mensagem').addClass('active');
      $('.mensagem-chamado-item').addClass('active');
      $('#nova-mensagem-faq').focus();
    });
    $('#nova-mensagem-faq').blur(function() {
      $('.historico-mensagem').removeClass('active');
      $('.nova-mensagem-barra').removeClass('active');
      $('.container-enviar-nova-mensagem').removeClass('active');
      $('.mensagem-chamado-item').removeClass('active');
    });
  },

  mostraBarraMensagem() {
    $(document).scroll(function() {
      if (window.innerWidth < 768) {
        const scrollTop = $(window).scrollTop() + $(window).height();
        const elem = $('.historico-mensagem');
        const offsetTopElemento = elem.offset().top + 150;
        if (scrollTop < offsetTopElemento) {
          $('.container-enviar-nova-mensagem').removeClass('barraFixa');
        } else {
          $('.container-enviar-nova-mensagem').addClass('barraFixa');
        }
      } else {
        return false;
      }
    });
  },

  abreModalAnexo() {
    $('body').on('click', '.btn-mensagem-visualizar', function(e) {
      const img = $(this)
        .closest('.mensagem-chamado-anexo')
        .find('img')
        .attr('src');
      $('#modal-chamado-anexo img').attr('src', img);
      $('#modal-chamado-anexo').addClass('active');
      $('body').addClass('blockScroll');
    });
  },

  fechaModalAnexo() {
    $('body').on('click', '#btn-fecha-modal-anexo, #bg-fecha-modal', function(e) {
      $('#modal-chamado-anexo').removeClass('active');
      $('body').removeClass('blockScroll');
      setTimeout(() => {
        $('#modal-chamado-anexo img').attr('src', '');
      }, 300);
    });
  },

  // ENVIAR PRIMEIRA OCORRENCIA #########################################################################################################

  liberaCampoOcorrencia() {
    $('body').on('change', '#assunto', function() {
      if ($('#assunto').val() != '' && $('#mensagem').val() != '') {
        $('.faq-contato-form-enviar .btn.btn-desabilitado').removeClass('btn-desabilitado');
        $('.faq-contato-form-enviar .btn').addClass('btn-laranja');
      } else {
        $('.faq-contato-form-enviar .btn.btn-laranja').removeClass('btn-laranja');
        $('.faq-contato-form-enviar .btn').addClass('btn-desabilitado');
      }
    });
    $('body').on('keyup', '#mensagem', function() {
      if ($('#assunto').val() != '' && $('#mensagem').val() != '') {
        $('.faq-contato-form-enviar .btn.btn-desabilitado').removeClass('btn-desabilitado');
        $('.faq-contato-form-enviar .btn').addClass('btn-laranja');
      } else {
        $('.faq-contato-form-enviar .btn.btn-laranja').removeClass('btn-laranja');
        $('.faq-contato-form-enviar .btn').addClass('btn-desabilitado');
      }
    });
  },

  enviarNovaOcorrencia() {
    $('body').on('click', '.faq-contato-form-enviar .btn.btn-laranja', function() {
      const dados = APP.controller.FaleConosco.dadosNovaOcorrencia();
    });
  },

  dadosNovaOcorrencia() {
    const formData = new FormData();
    //
    formData.append('OccurrenceContactTypeId', 2);
    formData.append('OccurrenceSubjectId', Number($('#assunto').val()));
    formData.append('OccurrenceMessage.Message', $('#mensagem').val());
    formData.append('OccurrenceMessage.OccurrenceMessageTypeId', 2);
    const arquivo = $('#arquivo')[0].files[0];
    if (!!arquivo) {
      // formData.append('OccurrenceMessage.File', arquivo);
      formData.append('formFile', arquivo);
    }
    //
    return formData;
  },

  dadosAtualizarOcorrencia() {
    const formData = new FormData();
    formData.append('Occurrence.Id', $('#enviar-mensagem').data('id-ocorrencia'));
    formData.append('OccurrenceMessageTypeId', 2);
    //
    if ($('#nova-mensagem-faq').val() != '') {
      formData.append('Message', $('#nova-mensagem-faq').val());
    }
    if (!!$('#arquivo-anexo')[0].files[0]) {
      formData.append('formFile', $('#arquivo-anexo')[0].files[0]);
    }
    //
    return formData;
  },

  mensagemOcorrenciaExistente() {
    $('body').on('keyup', '#nova-mensagem-faq', function() {
      if ($('#nova-mensagem-faq').val() != '' || !!$('#arquivo-anexo')[0].files[0]) {
        $('.nova-mensagem-barra .btn.btn-desabilitado').removeClass('btn-desabilitado');
        $('.nova-mensagem-barra .btn').addClass('btn-laranja');
      } else {
        $('.nova-mensagem-barra .btn.btn-laranja').removeClass('btn-laranja');
        $('.nova-mensagem-barra .btn').addClass('btn-desabilitado');
      }
    });
    $('body').on('change', '#arquivo-anexo', function() {
      if ($('#nova-mensagem-faq').val() != '' || !!$('#arquivo-anexo')[0].files[0]) {
        $('.nova-mensagem-barra .btn.btn-desabilitado').removeClass('btn-desabilitado');
        $('.nova-mensagem-barra .btn').addClass('btn-laranja');
      } else {
        $('.nova-mensagem-barra .btn.btn-laranja').removeClass('btn-laranja');
        $('.nova-mensagem-barra .btn').addClass('btn-desabilitado');
      }
    });
  },

  abrirOcorrencia() {
    $('body').on('click', '.faq-contato-form-enviar .btn.btn-laranja', function() {
      const formData = APP.controller.FaleConosco.dadosNovaOcorrencia();
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      $.ajax({
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        cache: false,
        url: pathApi.abrirOcorrencia,
        headers: { Authorization: `Bearer ${token}` },
        beforeSend: function() {
          APP.component.Loading.show();
          APP.component.Modal.closeModalSuperior();
        },
        success: function(result) {
          $('#assunto').val('');
          $('#mensagem').val('');
          $('#arquivo').val('');
          return APP.component.Modal.mensagemModalSuperior('Sucesso', 'Sua mensagem foi enviada. O prazo para retornarmos seu contato é de até 7 dias úteis.');
        },
        error: function(result) {
          return APP.component.Modal.mensagemModalSuperior('Falha', 'Por favor, tente novamente em alguns instantes.');
        },
        complete: function(result) {
          APP.component.Loading.hide();
        }
      });
    });
  },

  trazerOcorrenciasUsuario() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.todasOcorrencias,
      method: 'GET',
      headers: {
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      success: function(result) {
        APP.controller.FaleConosco.preencheChamados(result);
      },
      error: function(result) {}
    });
  },

  preencheChamados(result) {
    $('[data-chamadas-lista="finalizadas"] .container-faq-lista-chamadas-itens').html('');
    $('[data-chamadas-lista="andamento"] .container-faq-lista-chamadas-itens').html('');
    result.forEach(item => {
      if (item.occurrenceStatus.description == 'FECHADO') {
        const data = item.createdAt.split('T');
        $('[data-chamadas-lista="finalizadas"] .container-faq-lista-chamadas-itens').append(`
          <div class="container-faq-lista-chamadas-list list-item-chamado" data-id-ocorrencia="${item.id}" data-status="Fechado">
            <p class="abertura">${data[0]
              .split('-')
              .reverse()
              .join('/')}</p>
            <p class="chamado">${item.code}</p>
            <p class="assunto">${item.occurrenceSubject ? item.occurrenceSubject.description : ''}</p>
          </div>
        `);
      } else if (item.occurrenceStatus.description == 'EM ABERTO' || item.occurrenceStatus.description == 'EM ANDAMENTO' || item.occurrenceStatus.description == 'AGUARDANDO PARTICIPANTE') {
               const data = item.createdAt.split('T');
               $('[data-chamadas-lista="andamento"] .container-faq-lista-chamadas-itens').append(`
          <div class="container-faq-lista-chamadas-list list-item-chamado" data-id-ocorrencia="${item.id}"  data-status="Em andamento">
            <p class="abertura">${data[0]
              .split('-')
              .reverse()
              .join('/')}</p>
            <p class="chamado">${item.code}</p>
            <p class="assunto">${item.occurrenceSubject ? item.occurrenceSubject.description : ''}</p>
          </div>
        `);
             }
    });
  },

  historicoMensagens(id) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      async: true,
      crossDomain: true,
      url: `${pathApi.mensagensOcorrencia}/${id}`,
      method: 'GET',
      headers: {
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      beforeSend: function() {
        APP.component.Loading.show();
      },
      success: function(result) {
        const historico = result.reverse();
        historico.forEach(item => {
          if (!item.internal) {
            const pathImg = APP.controller.Api.pathOcorrencias();
            const data = item.createdAt.split('T');
            const fotoUsuario = $('.info-pessoais-foto img').attr('src');
            const nomeUsuario = $('#info-nome')
              .text()
              .split(' ');
            $('.container-historico-mensagem').append(`
            <div class="mensagem-chamado-item ${item.occurrenceMessageType.description == 'PARTICIPANTE' ? 'enviada' : 'recebida'}">
              <div class="mensagem-chamado-foto" style="background-image: url(${
                item.occurrenceMessageType.description == 'PARTICIPANTE'
                  ? fotoUsuario + '); background-position: center; background-size: cover;'
                  : '/_assets/img/faq/icon-atendente.svg); background-position: center -5px; background-size: 85px;'
              }">
              </div>
              <p class="mensagem-chamado-nome">${item.occurrenceMessageType.description == 'PARTICIPANTE' ? nomeUsuario[0].charAt(0).toUpperCase() + nomeUsuario[0].slice(1) : 'Atendente'}</p>
              <p class="mensagem-chamado-data">${data[0]
                .split('-')
                .reverse()
                .join('/')} às ${data[1].slice(0, 5)}</p>
              <p class="mensagem-chamado-texto">${item.message}</p>
              ${APP.controller.FaleConosco.verificaExisteImagem(item.file, pathImg)}
            </div>
            `);
          }
        });
      },
      error: function(result) {},
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  verificaExisteImagem(img, pathImg) {
    if (!!img) {
      const pathDownload = pathImg
        .split('/')
        .splice(3)
        .join('/');
      return `
      <div class="mensagem-chamado-anexo">
        <figure data-figure>
          <img src="${pathImg}${img}">
          <figcaption>${img}</figcaption>
        </figure>
        <div class="botoes-mensagem-anexo">
          <button class="btn-mensagem-visualizar"></button>
          <a download href="/${pathDownload}${img}" rel="noopener noreferrer" class="btn-mensagem-baixar"></a>
        </div>
      </div>
      `;
    } else {
      return ``;
    }
  },

  atualizarOcorrencia() {
    $('body').on('click', '.nova-mensagem-barra .btn.btn-laranja', function() {
      const formData = APP.controller.FaleConosco.dadosAtualizarOcorrencia();
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      $.ajax({
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        cache: false,
        url: pathApi.atualizarOcorrencia,
        headers: { Authorization: `Bearer ${token}` },
        beforeSend: function() {
          APP.component.Modal.closeModalSuperior();
          APP.component.Loading.show();
        },
        success: function(result) {
          $('#nova-mensagem-faq').val('');
          $('#arquivo-anexo').val('');
          $('#enviar-mensagem').removeClass('btn-laranja');
          $('#enviar-mensagem').addClass('btn-desabilitado');
          APP.controller.FaleConosco.inserirMensagemOcorrencia(result);
          return APP.component.Modal.mensagemModalSuperior('Sucesso', 'Sua mensagem foi enviada. O prazo para retornarmos seu contato é de até 7 dias úteis.');
        },
        error: function(result) {},
        complete: function(result) {
          APP.component.Loading.hide();
        }
      });
    });
  },

  inserirMensagemOcorrencia(result) {
    const pathImg = APP.controller.Api.pathOcorrencias();
    const data = result.createdAt.split('T');
    const fotoUsuario = $('.info-pessoais-foto img').attr('src');
    const nomeUsuario = $('#info-nome')
      .text()
      .split(' ');
    $('.container-historico-mensagem').prepend(`
    <div class="mensagem-chamado-item enviada">
      <div class="mensagem-chamado-foto" style="background-image: url(${fotoUsuario + '); background-position: center; background-size: cover;'}">
      </div>
      <p class="mensagem-chamado-nome">${nomeUsuario[0].charAt(0).toUpperCase() + nomeUsuario[0].slice(1)}</p>
      <p class="mensagem-chamado-data">${data[0]
        .split('-')
        .reverse()
        .join('/')} às ${data[1].slice(0, 5)}</p>
      <p class="mensagem-chamado-texto">${result.message}</p>
      ${APP.controller.FaleConosco.verificaExisteImagem(result.file, pathImg)}
    </div>
    `);
  }
};
