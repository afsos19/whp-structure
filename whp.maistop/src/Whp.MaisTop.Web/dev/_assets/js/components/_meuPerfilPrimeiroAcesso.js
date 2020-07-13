/*
|--------------------------------------------------------------------------
| Date
|--------------------------------------------------------------------------
*/

APP.component.MeuPerfilPrimeiroAcesso = {
  init() {
    $('#app').load('/_logadas/meu-perfil.html', function() {
      APP.controller.MeuPerfil.acessoPermitido();
      APP.component.MeuPerfilPrimeiroAcesso.setup();
      APP.component.MeuPerfilPrimeiroAcesso.salvarDados();
      APP.component.MeuPerfilPrimeiroAcesso.digitarEmUppercase();
    });
  },

  // Faz a configuração da página Meu Perfil para o primeiro acesso
  setup() {
    $('#rede').val(JSON.parse(sessionStorage.getItem('firstAcess')).rede);
    $('#loja').val(JSON.parse(sessionStorage.getItem('firstAcess')).loja);
    $('#cargo').val(JSON.parse(sessionStorage.getItem('firstAcess')).cargo);
    $('#nome').val(JSON.parse(sessionStorage.getItem('firstAcess')).nome);
    $('#link-download-pdf').attr('href', `${APP.controller.Api.pathRegulamentos()}Regulamento-Programa-maistop_${JSON.parse(sessionStorage.getItem('firstAcess')).nomePdf.replace(/\s/g, '')}.pdf`);
    if (cargo == 'GERENTE REGIONAL') {
      $('#rede')
        .parent()
        .hide();
      $('#loja')
        .parent()
        .hide();
    }
    $('#alterar-telefone').attr('id', '');
    $('#info-nome').text('Nome');
    $('#info-telefone').attr('value', 'Telefone');
    $('#title-cadastro-senha').text('Cadastre uma senha');
    $('#salvar-etapa-01').remove();
    $('#salvar-etapa-02').remove();
    $('#senha-atual')
      .parent()
      .remove();
    $('[for="senha"]').text('Senha*:');
    $('[for="confirmacao-senha"]').text('Confirmar Senha*:');
    $('.arrow-back.back').removeClass('back');
    $('.links-perfil').attr('href', '#!');
    $('.btn-planilha').attr('href', '#!');
    $('[data-menu-header] a').attr('href', '#!');
    // $('.links-perfil').attr('id', '');
  },

  // Clique no "Salvar", se os dados estiverem certos, envia o SMS
  salvarDados() {
    $('body').on('click', '#salvar-etapa-03', function(e) {
      e.preventDefault();
      if (!!APP.component.MeuPerfilPrimeiroAcesso.verificarCamposObrigatorios()) {
        APP.component.MeuPerfilPrimeiroAcesso.enviarSms();
      } else {
        APP.component.Alert.customMessage('camposIncorretos');
        const arr = APP.component.MeuPerfilPrimeiroAcesso.informarCamposIncorretos();
        setTimeout(() => {
          for (let i = 0; i < arr.length; i++) {
            $('#campos-incorretos-modal').append(`<li>${arr[i]}</li>`);
            if (i == arr.length - 1) {
              APP.controller.MeuPerfil.agrupamentoDosCamposNoModalDeErro();
            }
          }
        }, 150);
      }
    });
  },

  // Código que é digitado no modal de SMS é transformado em letras maiúsculas
  digitarEmUppercase() {
    $('body').on('keyup', '#codigo-sms', function() {
      const msg = $(this).val();
      const novaMsg = msg.toUpperCase();
      $(this).val(novaMsg);
    });
  },

  // Envia o SMS
  enviarSms() {
    const objFirstAccess = JSON.parse(sessionStorage.getItem('firstAcess'));
    const pathApi = APP.controller.Api.pathsApi();
    const usuario = {
      id: objFirstAccess.id,
      Cellphone: APP.component.Form.limpaPhone($('#celular').val())
    };

    $.ajax({
      type: 'POST',
      dataType: 'json',
      url: pathApi.smsPrimeiroAcesso,
      data: JSON.stringify(usuario),
      cache: false,
      contentType: 'application/json',
      beforeSend: function() {
        APP.component.Loading.show();
      },
      success: function(result) {
        APP.component.Alert.customMessage('enviarSms');
        setTimeout(() => {
          $('#codigo-sms').focus();
        }, 150);
        APP.component.MeuPerfilPrimeiroAcesso.confirmaCodigoSms(result);
        APP.component.MeuPerfilPrimeiroAcesso.reenviarCodigoSms();
      },
      error: function(result) {
        if (result.status == 500) {
          return APP.component.Modal.mensagemModalSuperior('Erro!', 'Celular já está em uso. Tente utilizar outro número.');
        }
        APP.component.Modal.mensagemModalSuperior('Erro!', 'Houve um erro ao enviar o SMS. Tente novamente em alguns instantes.');
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  // Reenvia código SMS
  reenviarCodigoSms() {
    $('body').off('click', '#reenviar-codigo');
    $('body').on('click', '#reenviar-codigo', function(e) {
      const pathApi = APP.controller.Api.pathsApi();
      const usuario = {
        id: $('[data-usuario]').data('id'),
        Cellphone: APP.component.Form.limpaPhone($('#celular').val())
      };
      $.ajax({
        type: 'POST',
        dataType: 'json',
        url: pathApi.smsPrimeiroAcesso,
        data: JSON.stringify(usuario),
        cache: false,
        contentType: 'application/json',
        beforeSend: function() {
          APP.component.Loading.show();
        },
        success: function(result) {
          APP.component.Alert.customMessage('enviarSms');
          setTimeout(() => {
            $('#codigo-sms').focus();
          }, 150);
          APP.component.MeuPerfilPrimeiroAcesso.confirmaCodigoSms(result);
          APP.component.MeuPerfilPrimeiroAcesso.reenviarCodigoSms();
        },
        error: function(result) {
          APP.component.Modal.mensagemModalSuperior('Erro!', 'Houve um erro ao enviar o SMS. Tente novamente em alguns instantes.');
        },
        complete: function(result) {
          APP.component.Loading.hide();
        }
      });
    });
  },

  // Reenvia código SMS na tela de erro
  codigoSmsEnviarNovamenteErro() {
    $('body').off('click', '#reenviar-codigo-erro');
    $('body').on('click', '#reenviar-codigo-erro', function(e) {
      const pathApi = APP.controller.Api.pathsApi();
      const usuario = {
        id: $('[data-usuario]').data('id'),
        Cellphone: APP.component.Form.limpaPhone($('#celular').val())
      };
      $.ajax({
        type: 'POST',
        dataType: 'json',
        url: pathApi.smsPrimeiroAcesso,
        data: JSON.stringify(usuario),
        cache: false,
        contentType: 'application/json',
        beforeSend: function() {
          APP.component.Loading.show();
        },
        success: function(result) {
          APP.component.Alert.customMessage('enviarSms');
          APP.component.MeuPerfilPrimeiroAcesso.confirmaCodigoSms(result);
          APP.component.MeuPerfilPrimeiroAcesso.reenviarCodigoSms();
        },
        error: function(result) {
          APP.component.Modal.mensagemModalSuperior('Erro!', 'Houve um erro ao enviar o SMS. Tente novamente em alguns instantes.');
        },
        complete: function(result) {
          APP.component.Loading.hide();
        }
      });
    });
  },

  // Tentar digitar novamente o código do SMS
  codigoSmsTentarNovamente() {
    $('body').off('click', '#modal-tentar-novamente-sms');
    $('body').on('click', '#modal-tentar-novamente-sms', function(e) {
      APP.component.Alert.customMessage('enviarSms');
    });
  },

  // Finaliza o cadastro
  finalizarCadastro() {
    const pathApi = APP.controller.Api.pathsApi();
    const formData = APP.component.MeuPerfilPrimeiroAcesso.criaFormData();
    //
    $.ajax({
      type: 'POST',
      data: formData,
      contentType: false,
      processData: false,
      cache: false,
      url: pathApi.cadastroPrimeiroAcesso,
      beforeSend: function() {
        APP.component.Loading.show();
      },
      success: function(result) {
        sessionStorage.removeItem('firstAcess');
        sessionStorage.setItem('token', result.token);
        window.location.href = '/';
      },
      error: function(result) {
        APP.component.Modal.mensagemModalSuperior('Erro!', 'Não conseguimos salvar seus dados. Por favor, tente novamente em alguns instantes.');
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  // Cria o formData com os dados do formulário
  criaFormData() {
    const usuario = APP.controller.MeuPerfil.pegarDadosFormulario();
    const infoStorage = JSON.parse(sessionStorage.getItem('firstAcess'));
    const codigo = $('#codigo-sms').val();
    const formData = new FormData();
    const imagem = $('#foto-perfil')[0].files[0];
    const imagemNome = !imagem ? '' : imagem.name;
    //
    formData.append('AccessCode', codigo);
    formData.append('Address', usuario.endereco);
    formData.append('BithDate', APP.component.Form.formatDataNascParaBanco(usuario.dataNascimento));
    formData.append('CellPhone', APP.component.Form.limpaPhone(usuario.celular));
    formData.append('City', usuario.cidade);
    formData.append('CivilStatus', usuario.estadoCivil);
    formData.append('CEP', APP.component.Form.limpaPhone(usuario.cep));
    formData.append('Complement', usuario.complemento);
    formData.append('CPF', APP.component.Form.limpaPhone(infoStorage.cpf));
    formData.append('Email', usuario.email);
    formData.append('file', imagem);
    formData.append('Photo', imagemNome);
    formData.append('Gender', usuario.sexo);
    formData.append('HeartTeam', usuario.timeCoracao);
    formData.append('Id', infoStorage.id);
    formData.append('Name', JSON.parse(sessionStorage.getItem('firstAcess')).nome);
    formData.append('Neighborhood', usuario.bairro);
    formData.append('Number', usuario.numero);
    formData.append('Password', usuario.senha);
    formData.append('Phone', APP.component.Form.limpaPhone(usuario.telefone));
    formData.append('PrivacyPolicy', true);
    formData.append('ReferencePoint', usuario.referencia);
    formData.append('SonAmout', usuario.filhos);
    formData.append('Uf', usuario.uf);
    //
    return formData;
  },

  //Confirmar código do SMS
  confirmaCodigoSms(objCodigo) {
    $('body').off('click', '.btn-enviar-codigo');
    $('body').on('click', '.btn-enviar-codigo', function(e) {
      e.preventDefault();
      const campoSms = $('#codigo-sms').val();
      if (campoSms === objCodigo.code) {
        APP.component.MeuPerfilPrimeiroAcesso.finalizarCadastro();
      } else {
        APP.component.Alert.customMessage('reenviarSms');
        APP.component.MeuPerfilPrimeiroAcesso.codigoSmsTentarNovamente();
        APP.component.MeuPerfilPrimeiroAcesso.codigoSmsEnviarNovamenteErro();
      }
    });
  },

  verificarCamposObrigatorios() {
    // Limpa os campos incorretos para setar novamente
    $('[data-formulario]').removeClass('incorreto');
    // Verificação de todos os campos
    const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
    return (
      // dados.foto != '/_assets/img/usuario/img-default.jpg' &&
      !!dados.nome &&
      !!APP.component.Form.validateEmail(dados.email) &&
      dados.confirmarEmail === dados.email &&
      !!APP.component.Form.validateCelular(dados.celular) &&
      !!APP.component.Form.validateTelefone(dados.telefone) &&
      !!APP.component.Form.validateDataNascimento(dados.dataNascimento) &&
      !!dados.sexo &&
      // !!dados.timeCoracao &&
      !!dados.filhos &&
      !!dados.estadoCivil &&
      !!APP.component.Form.validateCep(dados.cep) &&
      !!dados.endereco &&
      !!dados.numero &&
      !!dados.complemento &&
      !!dados.bairro &&
      !!dados.cidade &&
      dados.uf.length == 2 &&
      !!dados.referencia &&
      !!APP.component.Form.validateSenha(dados.senha) &&
      dados.confirmarAtual === dados.senha &&
      !!dados.regulamento.is(':checked')
    );
  },

  // Função de verificação
  functionVerificaCampos(condicao, id, array) {
    if (condicao) {
      var nomeEtapa = $(`#${id}`)
        .closest('[data-etapa-nome]')
        .data('etapa-nome');
      array.push(nomeEtapa);
      array.push($(`#${id}`).data('formulario'));
      $(`#${id}`).addClass('incorreto');
    }
  },

  informarCamposIncorretos() {
    const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
    // Array para salvar os campos incorretos
    const arrayCamposIncorretor = [];
    // Verificação dos campos incorretos
    // APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.foto === '/_assets/img/usuario/img-default.jpg', 'foto-usuario-cadastro', arrayCamposIncorretor);
    // APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.nome, 'nome', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateEmail(dados.email), 'email', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.confirmarEmail !== dados.email || dados.confirmarEmail == '', 'conf-email', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateCelular(dados.celular), 'celular', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateTelefone(dados.telefone), 'telefone', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateDataNascimento(dados.dataNascimento), 'data-nascimento', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.sexo, 'sexo', arrayCamposIncorretor);
    // APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.timeCoracao, 'time-coracao', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.filhos, 'filhos', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.estadoCivil, 'estado-civil', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateCep(dados.cep), 'cep', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.endereco, 'endereco', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.numero, 'numero', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.complemento, 'complemento', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.bairro, 'bairro', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.cidade, 'cidade', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.uf.length < 2, 'uf', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.referencia, 'referencia', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateSenha(dados.senha), 'senha', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.confirmarAtual !== dados.senha || dados.confirmarAtual == '', 'confirmacao-senha', arrayCamposIncorretor);
    APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!dados.regulamento.is(':checked'), 'aceite-regulamento-cadastro', arrayCamposIncorretor);
    // Abre modal com os campos incorretos
    return arrayCamposIncorretor.filter((item, index) => arrayCamposIncorretor.indexOf(item) === index);
  }
};
