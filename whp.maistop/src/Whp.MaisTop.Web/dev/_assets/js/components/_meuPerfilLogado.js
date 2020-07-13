/*
|--------------------------------------------------------------------------
| Date
|--------------------------------------------------------------------------
*/

APP.component.MeuPerfilLogado = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.MeuPerfilLogado.verificaAcesso();
    }
  },

  verificaAcesso() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const decoded = jwt_decode(token);
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: `${pathApi.getUser}/${decoded.id}`,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      beforeSend: function() {
        APP.component.Loading.show();
      },
      success: function(result) {
        APP.component.MeuPerfilLogado.acessoPermitido(result, decoded);
      },
      error: function(result) {
        window.location = '/Login';
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/meu-perfil.html', function() {
      APP.controller.MeuPerfil.acessoPermitido();
      APP.component.SidebarMenu.preencheInfosLaterais(result);
      APP.component.Acessos.retornaLogoRedeUsuario(decoded);
      APP.component.Acessos.setPerfilUsuario(result);
      APP.component.SidebarMenu.preenchePontuacaoLateral(result);
      APP.component.MeuPerfilLogado.setup();
      APP.component.MeuPerfilLogado.buscaDadosPerfil();
      APP.component.MeuPerfilLogado.salvarPrimeiroPasso();
      APP.component.MeuPerfilLogado.salvarSegundoPasso();
      APP.component.MeuPerfilLogado.salvarTerceiroPasso();
      APP.component.MeuPerfilLogado.verificaModalAtualizacaoEndereco();
    });
  },

  setup() {
    $('[data-btn-etapa="1"]').remove();
    $('[data-btn-etapa="2"]').remove();
    $('[data-btn-etapa="3"]').remove();
    $('.aceite-regulamento-primeiro-acesso').remove();
    $('#senha').data('formulario', 'Nova Senha');
  },

  buscaDadosPerfil() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const decoded = jwt_decode(token);
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      cache: false,
      url: `${pathApi.meuPerfil}/${decoded.id}`,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        APP.component.MeuPerfilLogado.preencheCampos(result, decoded);
      },
      error: function(result) {
        if (result.status == 401) {
          sessionStorage.removeItem('token');
          window.location.href = '/Login';
        }
        window.location.href = '/Login';
      }
    });
  },

  // Objeto com todos os valores do primeiro acesso
  pegarDadosFormulario() {
    const dadosForm = {
      bairro: $('#bairro').val(),
      celular: $('#celular').val(),
      cep: $('#cep').val(),
      cidade: $('#cidade').val(),
      complemento: $('#complemento').val(),
      confirmarAtual: $('#confirmacao-senha').val(),
      confirmarEmail: $('#conf-email').val(),
      dataNascimento: $('#data-nascimento').val(),
      email: $('#email').val(),
      endereco: $('#endereco').val(),
      estadoCivil: $('#estado-civil').val(),
      filhos: $('#filhos').val(),
      foto: $('#foto-usuario-cadastro').attr('src'),
      nome: $('#nome').val(),
      numero: $('#numero').val(),
      referencia: $('#referencia').val(),
      senha: $('#senha').val(),
      sexo: $('#sexo').val(),
      telefone: $('#telefone').val(),
      timeCoracao: $('#time-coracao').val(),
      uf: $('#uf').val()
    };
    return dadosForm;
  },

  infoLoja() {
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
        $('#rede').val(result[0].network.name);
        $('#loja').val(result[0].cnpj);
      },
      error: function(result) {}
    });
  },

  preencheCampos(usuario, decoded) {
    // Retorna url de dev ou homolog / prod
    const img = APP.controller.Api.pathImagem();
    // Retorno da API getUser
    const u = usuario;
    $('#cargo').val(u.office.description);
    if (u.office.description != 'GERENTE REGIONAL') {
      APP.component.MeuPerfilLogado.infoLoja();
    } else {
      $('#rede')
        .parent()
        .hide();
      $('#loja')
        .parent()
        .hide();
    }
    //
    const fotoPerfil = !u.photo || u.photo == 'null' || u.photo == '' || u.photo == 'undefined' ? '/_assets/img/usuario/avatar_generico.png' : img + u.photo;
    $('#foto-usuario-cadastro').attr('src', fotoPerfil);
    //
    $('#nome').val(u.name);
    $('#email').val(u.email);
    $('#conf-email').val(u.email);
    const celular = APP.component.Form.limpaPhone(u.cellPhone);
    $('#celular').val(APP.component.Form.formatCellPhone(celular));
    if (u.phone) {
      const telefone = APP.component.Form.limpaPhone(u.phone);
      $('#telefone').val(APP.component.Form.formatPhone(telefone));
    }
    $('#data-nascimento').val(APP.component.Form.formatDate(u.bithDate));
    $('#sexo').val(u.gender);
    $('#time-coracao').val(u.heartTeam);
    $('#filhos').val(u.sonAmout);
    $('#estado-civil').val(u.civilStatus);
    $('#cep').val(APP.component.Form.formatCep(u.cep));
    $('#endereco').val(u.address);
    $('#numero').val(u.number);
    $('#complemento').val(`${u.complement == 'null' || u.complement == null ? '' : u.complement}`);
    $('#bairro').val(u.neighborhood);
    $('#cidade').val(u.city);
    $('#uf').val(u.uf);
    $('#referencia').val(u.referencePoint);
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

  //SALVAR PRIMEIRO E SEGUNDO PASSO ###########################################################################################################################################################################################################

  salvarPrimeiroPasso() {
    $('body').on('click', '#salvar-etapa-01', function(e) {
      e.preventDefault();
      const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
      if (!!APP.component.MeuPerfilLogado.verificaCamposDadosCadastrais(dados)) {
        const formData = APP.component.MeuPerfilLogado.criaFormDataDadosCadastrais();
        APP.component.MeuPerfilLogado.enviarInformacoes(formData);
      } else {
        APP.component.MeuPerfilLogado.erroAoSalvar(false);
      }
    });
  },

  salvarSegundoPasso() {
    $('body').on('click', '#salvar-etapa-02', function(e) {
      e.preventDefault();
      const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
      if (!!APP.component.MeuPerfilLogado.verificaCamposDadosCadastrais(dados)) {
        const formData = APP.component.MeuPerfilLogado.criaFormDataDadosCadastrais();
        APP.component.MeuPerfilLogado.enviarInformacoes(formData);
      } else {
        APP.component.MeuPerfilLogado.erroAoSalvar(false);
      }
    });
  },

  salvarTerceiroPasso() {
    $('body').on('click', '#salvar-etapa-03', function(e) {
      e.preventDefault();
      const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
      if (!!APP.component.MeuPerfilLogado.verificaCamposDadosCadastrais(dados) && !!APP.component.MeuPerfilLogado.verificaCamposTerceiroPasso(dados)) {
        const formData = APP.component.MeuPerfilLogado.criaFormDataDadosSenha();
        APP.component.MeuPerfilLogado.enviarInformacoes(formData);
      } else {
        APP.component.MeuPerfilLogado.erroAoSalvar(true);
      }
    });
  },

  verificaModalAtualizacaoEndereco(){
    if (sessionStorage.getItem('atualizarCadastroModal')){
      sessionStorage.removeItem('atualizarCadastroModal');
      $('[data-btn-topo-etapa="2"]').click();
    }
  },

  //ERRO AO SALVAR ###########################################################################################################################################################################################################

  erroAoSalvar(verificaSenha) {
    APP.component.Alert.customMessage('camposIncorretos');
    const arr = APP.component.MeuPerfilLogado.informaCamposIncorretosDadosCadastrais(verificaSenha);
    setTimeout(() => {
      for (let i = 0; i < arr.length; i++) {
        $('#campos-incorretos-modal').append(`<li>${arr[i]}</li>`);
        if (i == arr.length - 1) {
          APP.controller.MeuPerfil.agrupamentoDosCamposNoModalDeErro();
        }
      }
    }, 150);
  },

  //VERIFICAÇÕES ###########################################################################################################################################################################################################

  verificaCamposDadosCadastrais(dados) {
    // Limpa os campos incorretos para setar novamente
    $('[data-form-etapa="1"] [data-formulario]').removeClass('incorreto');
    $('[data-form-etapa="2"] [data-formulario]').removeClass('incorreto');
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
      !!dados.referencia
    );
  },

  informaCamposIncorretosDadosCadastrais(verificaSenha) {
    const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
    // Array para salvar os campos incorretos
    const arrayCamposIncorretor = [];
    // Verificação dos campos incorretos
    // APP.component.MeuPerfilLogado.functionVerificaCampos(dados.foto === '/_assets/img/usuario/img-default.jpg', 'foto-usuario-cadastro', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.nome, 'nome', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!APP.component.Form.validateEmail(dados.email), 'email', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(dados.confirmarEmail !== dados.email || dados.confirmarEmail == '', 'conf-email', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!APP.component.Form.validateCelular(dados.celular), 'celular', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!APP.component.Form.validateTelefone(dados.telefone), 'telefone', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!APP.component.Form.validateDataNascimento(dados.dataNascimento), 'data-nascimento', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.sexo, 'sexo', arrayCamposIncorretor);
    // APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.timeCoracao, 'time-coracao', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.filhos, 'filhos', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.estadoCivil, 'estado-civil', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!APP.component.Form.validateCep(dados.cep), 'cep', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.endereco, 'endereco', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.numero, 'numero', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.complemento, 'complemento', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.bairro, 'bairro', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.cidade, 'cidade', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(dados.uf.length < 2, 'uf', arrayCamposIncorretor);
    APP.component.MeuPerfilLogado.functionVerificaCampos(!dados.referencia, 'referencia', arrayCamposIncorretor);
    if (!!verificaSenha) {
      APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.senhaAnterior == '', 'senha-atual', arrayCamposIncorretor);
      APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(!APP.component.Form.validateSenha(dados.senha), 'senha', arrayCamposIncorretor);
      APP.component.MeuPerfilPrimeiroAcesso.functionVerificaCampos(dados.confirmarAtual !== dados.senha || dados.confirmarAtual == '', 'confirmacao-senha', arrayCamposIncorretor);
    }
    // Abre modal com os campos incorretos
    return arrayCamposIncorretor.filter((item, index) => arrayCamposIncorretor.indexOf(item) === index);
  },

  //TERCEIRA ETAPA ###########################################################################################################################################################################################################

  verificaCamposTerceiroPasso(dados) {
    $('[data-form-etapa="3"] [data-formulario]').removeClass('incorreto');
    return dados.senhaAnterior != '' && !!APP.component.Form.validateSenha(dados.senha) && dados.confirmarAtual === dados.senha;
  },

  //SALVAR DADOS ###########################################################################################################################################################################################################

  criaFormDataDadosCadastrais() {
    const usuario = APP.controller.MeuPerfil.pegarDadosFormulario();
    const token = sessionStorage.getItem('token');
    const decoded = jwt_decode(token);
    const formData = new FormData();
    const caminho = APP.controller.Api.pathImagem();
    const imagem = $('#foto-perfil')[0].files[0];
    const imagemName = imagem
      ? imagem.name
      : $('#foto-usuario-cadastro')
          .attr('src')
          .split(caminho)[1];
    //
    formData.append('Address', usuario.endereco);
    formData.append('BithDate', APP.component.Form.formatDataNascParaBanco(usuario.dataNascimento));
    formData.append('CellPhone', APP.component.Form.limpaPhone(usuario.celular));
    formData.append('City', usuario.cidade);
    formData.append('CivilStatus', usuario.estadoCivil);
    formData.append('CEP', APP.component.Form.limpaCep(usuario.cep));
    formData.append('Complement', usuario.complemento);
    formData.append('CPF', usuario.cpfAside);
    formData.append('Email', usuario.email);
    formData.append('file', imagem);
    formData.append('Photo', imagemName);
    formData.append('Gender', usuario.sexo);
    formData.append('HeartTeam', usuario.timeCoracao);
    formData.append('Id', decoded.id);
    formData.append('Name', usuario.nome);
    formData.append('Neighborhood', usuario.bairro);
    formData.append('Number', usuario.numero);
    formData.append('Phone', APP.component.Form.limpaPhone(usuario.telefone));
    formData.append('PrivacyPolicy', true);
    formData.append('ReferencePoint', usuario.referencia);
    formData.append('SonAmout', usuario.filhos);
    formData.append('Uf', usuario.uf);
    //
    return formData;
  },

  criaFormDataDadosSenha() {
    const usuario = APP.controller.MeuPerfil.pegarDadosFormulario();
    const formDataSenha = APP.component.MeuPerfilLogado.criaFormDataDadosCadastrais();
    //
    formDataSenha.append('Password', usuario.senha);
    formDataSenha.append('OldPassword', usuario.senhaAnterior);
    //
    return formDataSenha;
  },

  //ENVIAR INFORMAÇÕES ###########################################################################################################################################################################################################

  enviarInformacoes(formData) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'POST',
      data: formData,
      contentType: false,
      processData: false,
      cache: false,
      url: pathApi.atualizarCadastro,
      headers: { Authorization: `Bearer ${token}` },
      beforeSend: function() {
        $('#info-telefone').blur();
        APP.component.Loading.show();
      },
      success: function(result) {
        APP.component.Modal.mensagemModalSuperior('Sucesso', 'Suas informações foram salvas!');
        $('#info-foto-aside').attr('src', $('#foto-usuario-cadastro').attr('src'));
        $('#info-nome').text($('#nome').val());
        $('#info-telefone').val($('#celular').val());
        $('#senha-atual').val('');
        $('#senha').val('');
        $('#confirmacao-senha').val('');
      },
      error: function(result) {
        if (result.status == 500) {
          return APP.component.Modal.mensagemModalSuperior('Erro!', 'A senha digitada não confere com a atual. Tente novamente.');
        }
        APP.component.Modal.mensagemModalSuperior('Erro!', 'Não conseguimos salvar seus dados. Por favor, tente novamente em alguns instantes.');
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  }
};
