/*
|--------------------------------------------------------------------------
| Modal
|--------------------------------------------------------------------------
*/

APP.component.Modal = {
  init() {
    this.setup();
  },

  setup() {},

  mensagemModalSuperior(titulo, texto, close) {
    $('#modal').load('/_modais/modalSuperior.html');
    const classModal = Math.round(Math.random() * 1000);
    setTimeout(() => {
      $('#msg-titulo').text(titulo);
      $('#msg-texto').text(texto);
      $('#mensagem-modal-superior').addClass('active');
      $('#mensagem-modal-superior').addClass(`modal${classModal}`);
    }, 350);
    if (close) {
      setTimeout(() => {
        $('#mensagem-modal-superior').hasClass(`modal${classModal}`) ? $('.btn-close-msg').click() : false;
      }, 8000);
    }
  },

  closeModalSuperior() {
    $('body').on('click', '.btn-close-msg', function() {
      $('#mensagem-modal-superior').removeClass('active');
      setTimeout(() => {
        $('#mensagem-modal-superior').remove();
      }, 200);
    });
  },

  closeModal() {
    $('body').on('click', '#bg-modal, [data-close-modal]', function() {
      $('.modal').fadeOut(400);
      setTimeout(() => {
        $('#modal').removeClass('box-modal-ativo');
        $('body').removeClass('blockScroll');
        $('#bg-modal').remove();
        $('.modal').remove();
      }, 400);
    });
  },

  closeModalRegulamento() {
    $('body').on('click', '[data-close-modal]', function() {
      $('.modal').fadeOut(400);
      setTimeout(() => {
        $('#modal').removeClass('box-modal-ativo');
        $('body').removeClass('blockScroll');
        $('#bg-modal').remove();
        $('.modal').remove();
      }, 400);
    });
  },

  criarCamposPesquisa() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    // fetch('./_assets/js/json/pesquisa.json', {
    fetch(pathApi.pesquisa, {
      headers: {
        authorization: `Bearer ${token}`
      }
    })
      .then(response => response.json())
      .then(result => {
        APP.component.Modal.closeModalRegulamento();
        $('#modal').load('/_modais/pesquisaOkno.html', function() {
          $('body').addClass('blockScroll');
          $('#modal').addClass('box-modal-ativo');
          $('.modal').show();
          $('#bg-modal').addClass('active');
          APP.component.Modal.prencheCamposPesquisa(result);
        });
      });
  },

  prencheCamposPesquisa(result) {
    const arrayPesquisa = [];
    result.question.forEach((item, i) => {
      const numPergunta = item.id;
      const arrayObjRespostas = result.answer.filter(objResposta => {
        return objResposta.questionQuiz.id == numPergunta;
      });
      const arrayRespostas = [];
      arrayObjRespostas.forEach(resposta => {
        arrayRespostas.push({
          resposta: resposta.description,
          val: resposta.id,
          right: resposta.right
        });
      });
      arrayPesquisa.push({
        type: item.questionQuizType.description,
        name: `pergunta-${i}-resposta`,
        pergunta: item.description,
        respostas: arrayRespostas
      });
    });
    arrayPesquisa.forEach((item, indice) => {
      const i = indice + 1;
      $('#pesquisaOkno .container-modal-perguntas').append(`
        <div class="container-modal-perguntas-item" data-pergunta-pesquisa="${i}">
        <p>${item.pergunta}</p>
        </div>
      `);
      APP.component.Modal.criarTiposDosCampos(item, i);
    });
    APP.component.Modal.iniciaModalPesquisa(arrayPesquisa);
    APP.component.Modal.proximoItemPesquisa(arrayPesquisa);
    APP.component.Modal.voltarItemPesquisa(arrayPesquisa);
    APP.component.Modal.montarCamposPesquisa(arrayPesquisa);
    APP.component.Modal.ativarBtnFinalizarUltimoCampo(arrayPesquisa);
    //
    const path = APP.controller.Api.pathPesquisa();
    //
    $('#intro-modal-pesquisa').html(result.answer[0].questionQuiz.quiz.description || 'Convidamos você a responder uma rápida pesquisa, que vai nos ajudar bastante. Vamos lá?');
    $('.container-imagem-pesquisa').css({
      background: result.answer[0].questionQuiz.quiz.image ? `url('${path}${result.answer[0].questionQuiz.quiz.image}')` : `url('${path}pesquisa-padrao.png')`,
      'background-position': 'center',
      'background-size': 'cover'
    });
  },

  criarTiposDosCampos(item, i) {
    switch (item.type) {
      case 'DISCURSIVA':
        APP.component.Modal.criaCamposTextarea(item, i);
        break;
      case 'MULTIPLA ESCOLHA':
        APP.component.Modal.criaCamposSelect(item, i);
        break;
      case 'UNICA ESCOLHA':
        APP.component.Modal.criaCamposRadio(item, i);
        break;
    }
  },

  criaCamposTextarea(objeto, i) {
    $(`[data-pergunta-pesquisa="${i}"]`).append(`
      <div>
        <textarea data-textarea-pesquisa="${objeto.respostas[0].val}" id="${objeto.name.toLowerCase().replace(/\s/g, '-')}-${i}"></textarea>
      </div>
    `);
  },

  criaCamposSelect(objeto, i) {
    $(`[data-pergunta-pesquisa="${i}"]`).append(`
      <div>
        <select id="${objeto.name.toLowerCase().replace(/\s/g, '-')}-${i}"></select>
      </div>
    `);
    $(`#${objeto.name.toLowerCase().replace(/\s/g, '-')}-${i}`).append(`
      <option value="" hidden>Selecione sua resposta</option>
      `);
    objeto.respostas.forEach(item => {
      $(`#${objeto.name.toLowerCase().replace(/\s/g, '-')}-${i}`).append(`
      <option value="${item.val}">${item.resposta}</option>
      `);
    });
  },

  criaCamposRadio(objeto, i) {
    $(`[data-pergunta-pesquisa="${i}"]`).append(`
      <div class="container-radio-pesquisa"></div>
    `);
    objeto.respostas.forEach(item => {
      $(`[data-pergunta-pesquisa="${i}"] > div`).append(`

          <input type="radio" id="${item.val}" name="${objeto.name}">
          <label for="${item.val}">${item.resposta}</label>

    `);
    });
  },

  iniciaModalPesquisa(itensPesquisa) {
    $('#modal-pergunta-total').text(itensPesquisa.length);
    $('body').on('click', '#comecar-pesquisa', function() {
      $('#pesquisa-voltar').hide();
      $('#pesquisa-apresentacao').hide();
      $('#enviar-pesquisa').hide();
      $('#pesquisa-perguntas').show();
      $('[data-pergunta-pesquisa="1"]').addClass('active');
    });
    $('#proximo-item-pesquisa').hide();
  },

  proximoItemPesquisa(itensPesquisa) {
    $('body').on('click', '#proximo-item-pesquisa', function() {
      const item = $('.container-modal-perguntas-item.active').data('pergunta-pesquisa') + 1;
      $('[data-pergunta-pesquisa]').removeClass('active');
      $(`[data-pergunta-pesquisa="${item}"]`).addClass('active');
      $('#modal-pergunta-atual').text(item);

      if (item === itensPesquisa.length && !!APP.component.Modal.ativarBtnFinalizar()) {
        $('#enviar-pesquisa').show();
      } else {
        $('#enviar-pesquisa').hide();
      }
      const textAreaCampo = $('.container-modal-perguntas-item.active > div > textarea');
      const selectCampo = $('.container-modal-perguntas-item.active > div > select');
      const radioCampo = $('.container-modal-perguntas-item.active > div.container-radio-pesquisa');
      if ((textAreaCampo.length > 0 && textAreaCampo.val() == '') || (selectCampo.length > 0 && selectCampo.val() == '') || (radioCampo.length > 0 && !radioCampo.hasClass('radio-ok'))) {
        $('#proximo-item-pesquisa').hide();
      } else {
        $('#proximo-item-pesquisa').show();
      }
      if (item == itensPesquisa.length) {
        $('#proximo-item-pesquisa').hide();
      }
      if (item > 1) {
        $('#pesquisa-voltar').show();
      }
    });
  },

  voltarItemPesquisa(itensPesquisa) {
    $('body').on('click', '#pesquisa-voltar', function() {
      const item = $('.container-modal-perguntas-item.active').data('pergunta-pesquisa') - 1;
      $('[data-pergunta-pesquisa]').removeClass('active');
      $(`[data-pergunta-pesquisa="${item}"]`).addClass('active');
      $('#modal-pergunta-atual').text(item);
      if (item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').show();
        $('#enviar-pesquisa').hide();
      }
      if (item == 1) {
        $('#pesquisa-voltar').hide();
      }
    });
  },

  ativarBtnFinalizarUltimoCampo(itensPesquisa) {
    $('body').on('change', '.container-modal-perguntas-item select', function() {
      const item = $('.container-modal-perguntas-item.active').data('pergunta-pesquisa');
      if (!!$('.container-modal-perguntas-item.active select').val() && item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').show();
      } else if ($('.container-modal-perguntas-item.active select').val() == '' && item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').hide();
      }
      if (!!APP.component.Modal.ativarBtnFinalizar() && item == itensPesquisa.length) {
        $('#enviar-pesquisa').fadeIn();
      } else {
        $('#enviar-pesquisa').fadeOut();
      }
    });
    $('body').on('change', '.container-modal-perguntas-item input[type="radio"]', function() {
      $(this)
        .closest('.container-radio-pesquisa')
        .addClass('radio-ok');
      const item = $('.container-modal-perguntas-item.active').data('pergunta-pesquisa');
      if ($(`div[data-pergunta-pesquisa="${item}"].active .container-radio-pesquisa`).hasClass('radio-ok') && item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').show();
      } else {
        $('#proximo-item-pesquisa').hide();
      }
      if (!!APP.component.Modal.ativarBtnFinalizar() && item == itensPesquisa.length) {
        $('#enviar-pesquisa').fadeIn();
      } else {
        $('#enviar-pesquisa').fadeOut();
      }
    });
    $('body').on('keyup', '.container-modal-perguntas-item textarea', function() {
      const item = $('.container-modal-perguntas-item.active').data('pergunta-pesquisa');
      if (!!$('.container-modal-perguntas-item.active textarea').val() && item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').show();
      } else if ($('.container-modal-perguntas-item.active textarea').val() == '' && item < itensPesquisa.length) {
        $('#proximo-item-pesquisa').hide();
      }
      if (!!APP.component.Modal.ativarBtnFinalizar() && item == itensPesquisa.length) {
        $('#enviar-pesquisa').fadeIn();
      } else {
        $('#enviar-pesquisa').fadeOut();
      }
    });
  },

  ativarBtnFinalizar() {
    let selectValidos = true;
    $('.container-modal-perguntas select').each(function() {
      if ($(this).val() != '') {
        selectValidos = true;
      } else {
        selectValidos = false;
        return false;
      }
    });
    let textareaValidos = true;
    $('.container-modal-perguntas textarea').each(function() {
      if ($(this).val() != '') {
        textareaValidos = true;
      } else {
        textareaValidos = false;
        return false;
      }
    });
    let radioValidos = true;
    $('.container-modal-perguntas .container-radio-pesquisa').each(function() {
      if ($(this).hasClass('radio-ok')) {
        radioValidos = true;
      } else {
        radioValidos = false;
        return false;
      }
    });
    if (selectValidos && textareaValidos && radioValidos) {
      return true;
    } else {
      return false;
    }
  },

  montarCamposPesquisa(arrayPesquisa) {
    $('body').on('click', '#enviar-pesquisa', function() {
      const data = [];
      $('.container-modal-perguntas-item').each(function() {
        if ($(this).find('textarea').length > 0) {
          data.push({
            answerQuizId: $(this)
              .find('textarea')
              .data('textarea-pesquisa')
              .toString(),
            answerDescription: String(
              $(this)
                .find('textarea')
                .val()
            )
          });
        } else if ($(this).find('select').length > 0) {
          data.push({
            answerQuizId: $(this)
              .find('select')
              .val()
          });
        } else if ($(this).find('input[type="radio"]').length > 0) {
          data.push({
            answerQuizId: $(this)
              .find('input:checked')
              .attr('id')
          });
        }
      });

      const respostasCertas = arrayPesquisa
        .map(item => {
          for (let i = 0; i < item.respostas.length; i++) {
            if (item.respostas[i].right == true) {
              return item.respostas[i];
            }
          }
        })
        .map(valor => valor.val)
        .join('');
      const respostasDadas = data.map(item => Number(item.answerQuizId)).join('');
      const acertouTudo = respostasCertas == respostasDadas;

      APP.component.Modal.enviarPesquisa(data, acertouTudo);
    });
  },

  enviarPesquisa(answerUserQuizDto, acertouTudo) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');

    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.salvarPesquisa,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify({ answerUserQuizDto, rightAnswers: acertouTudo }),
      beforeSend: function() {
        // APP.component.Loading.show();
      },
      success: function(result) {
        $('#pesquisa-perguntas').hide();
        $('#pesquisa-resultado').show();
        if (acertouTudo) {
          $('#modal-pesquisa-resultado-texto').html('<span style="color: #707070">Parabéns!<br>Você acertou todas as respostas!</span>');
        } else {
          $('#modal-pesquisa-resultado-texto').html('<span style="color: #B1B1B1">Não foi dessa vez.</span>');
        }
      },
      error: function(result) {
        APP.component.Modal.closeModalSuperior();
        APP.component.Modal.mensagemModalSuperior('Erro!', 'Falha ao enviar. Por favor, tente mais tarde.');
      },
      complete: function() {
        // APP.component.Loading.hide();
      }
    });
  },

  modalAtualizacaoDados(dados) {
    $('#modalAtualizarCadastro-endereco').append(`
      <p><span>Endereço</span>${dados.address}</p>
      ${dados.number.length == 0 ? `<p class="modalAtualizarCadastro-upper"><span>Número*:</span></p><input isnum id="moda-alteracao-endereco-numero" type="text">` : ''}
      ${dados.complement == '' || !dados.complement ? `<p class="modalAtualizarCadastro-upper"><span>Complemento*:</span></p><input id="moda-alteracao-endereco-complemento" type="text">` : ''}
      <div class="modalAtualizarCadastro-endereco-container">
        <p><span>Bairro:</span>${dados.neighborhood}</p>
        <p><span>CEP:</span>${dados.cep.slice(0, 5)}-${dados.cep.slice(5, 8)}</p>
      </div>
      <a id="salvar-alteracao-cadastro-modal" data-usuario-id="${
        dados.id
      }" class="btn btn-desabilitado btn-login" rel="noopener noreferrer" onclick="APP.component.Modal.modalAtualizarEndereco()">Atualizar</a>
    `);
    APP.controller.Main.maskInit();
    $('body').on('keyup', '#modalAtualizarCadastro-endereco input', function() {
      if (APP.component.Modal.modalAtualizacaoVerificaCamposPreenchidos() && APP.component.Modal.modalAtualizacaoVerificaCamposRegras()) {
        $('#salvar-alteracao-cadastro-modal').removeClass('btn-desabilitado');
        $('#salvar-alteracao-cadastro-modal').addClass('btn-verde');
      } else {
        $('#salvar-alteracao-cadastro-modal').removeClass('btn-verde');
        $('#salvar-alteracao-cadastro-modal').addClass('btn-desabilitado');
      }
    });
  },

  modalAtualizacaoVerificaCamposPreenchidos() {
    return [...$('#modalAtualizarCadastro-endereco input')].every(function(elem) {
      return elem.value != '';
    });
  },

  modalAtualizacaoVerificaCamposRegras() {
    let numOk = true;
    let complemOk = true;
    if ($('#modalAtualizarCadastro-endereco #moda-alteracao-endereco-numero').length > 0) {
      $('#modalAtualizarCadastro-endereco #moda-alteracao-endereco-numero')
        .val()
        .trim().length > 0
        ? (numOk = true)
        : (numOk = false);
    }
    if ($('#modalAtualizarCadastro-endereco #moda-alteracao-endereco-complemento').length > 0) {
      $('#modalAtualizarCadastro-endereco #moda-alteracao-endereco-complemento')
        .val()
        .trim().length > 0
        ? (complemOk = true)
        : (complemOk = false);
    }
    return numOk && complemOk;
  },

  modalAtualizarEndereco() {
    if (!APP.component.Modal.modalAtualizacaoVerificaCamposPreenchidos() || !APP.component.Modal.modalAtualizacaoVerificaCamposRegras()) {
      return false;
    } else {
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      const decoded = jwt_decode(token);
      fetch(`${pathApi.getUser}/${decoded.id}`, {
        headers: {
          authorization: `Bearer ${token}`
        }
      })
        .then(r => r.json())
        .then(dados => {
          const formData = new FormData();
          formData.append('Address', dados.address);
          formData.append('BithDate', dados.bithDate);
          formData.append('CellPhone', dados.cellPhone);
          formData.append('City', dados.city);
          formData.append('CivilStatus', dados.civilStatus);
          formData.append('CEP', dados.cep);
          formData.append('Complement', dados.complement == '' || !dados.complement ? $('#moda-alteracao-endereco-complemento').val() : dados.complement);
          formData.append('CPF', dados.cpf);
          formData.append('Email', dados.email);
          formData.append('Photo', dados.photo);
          formData.append('Gender', dados.gender);
          formData.append('HeartTeam', dados.heartTeam);
          formData.append('Id', dados.id);
          formData.append('Name', dados.name);
          formData.append('Neighborhood', dados.neighborhood);
          formData.append('Number', dados.number == '' || dados.number == 'null' || !dados.number ? $('#moda-alteracao-endereco-numero').val() : dados.number);
          formData.append('Phone', dados.phone);
          formData.append('PrivacyPolicy', true);
          formData.append('ReferencePoint', dados.referencePoint);
          formData.append('SonAmout', dados.sonAmout);
          formData.append('Uf', dados.uf);
          $.ajax({
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            cache: false,
            url: pathApi.atualizarCadastro,
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function() {
              APP.component.Modal.closeModalSuperior();
              APP.component.Loading.show();
            },
            success: function(result) {
              $('#modal').removeClass('box-modal-ativo');
              $('body').removeClass('blockScroll');
              $('#bg-modal').remove();
              $('.modal').remove();
              setTimeout(() => {
                APP.component.Modal.mensagemModalSuperior('Sucesso!', 'Seu cadastro foi atualizado com sucesso.');
              }, 100);
              if (window.location.pathname == '/MeuPerfil/') {
                $('#numero').val(result.user.number);
                $('#complemento').val(result.user.complement);
              }
            },
            error: function(result) {
              APP.component.Modal.mensagemModalSuperior('Erro!', 'Não conseguimos salvar seus dados. Por favor, tente novamente em alguns instantes.');
            },
            complete: function(result) {
              APP.component.Loading.hide();
            }
          });
        });
    }
  }
};
