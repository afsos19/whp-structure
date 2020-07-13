/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.PrimeiroAcesso = {
  init() {
    this.setup();
    this.primeiroAcesso();
    this.salvarDados();
    APP.component.Modal.closeModalSuperior();
    APP.component.Modal.closeModal();
  },

  setup() {
    //
  },

  primeiroAcesso() {
    $('body').on('click', '#entrar', function(e) {
      e.preventDefault();
      const dadosPrimeiroAcesso = {
        Cpf: $('#cpf')
          .val()
          .replace(/[^\d]+/g, '')
      };
      if (!APP.component.Form.validateCpf(dadosPrimeiroAcesso.Cpf)) {
        return APP.component.Modal.mensagemModalSuperior('Erro', 'CPF inválido. Por favor, verifique.');
      }
      const pathApi = APP.controller.Api.pathsApi();
      $.ajax({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        url: `${pathApi.primeiroAcesso}/${dadosPrimeiroAcesso.Cpf}`,
        beforeSend: function() {
          APP.component.Loading.show();
        },
        success: function(result) {
          const dadosAcesso = {
            nome: result.user.name,
            cpf: dadosPrimeiroAcesso.Cpf,
            id: result.user.id,
            rede: result.shops[0].network.name,
            loja: result.shops[0].cnpj,
            cargo: result.user.office.description,
            nomePdf: result.shops[0].network.siteShortName
          };
          if (result.user.userStatus.description === 'PRE-CADASTRO') {
            sessionStorage.removeItem('token');
            sessionStorage.setItem('firstAcess', JSON.stringify(dadosAcesso));
            window.location.href = '/MeuPerfil';
          } else {
            APP.component.Modal.mensagemModalSuperior('Erro', 'CPF já cadastrado, faça o login para entrar.');
          }
        },
        error: function(result) {
          if (result.responseText == 'Usuário não encontra-se no estado de pré cadastro!') {
            return APP.component.Modal.mensagemModalSuperior('Erro', 'CPF já cadastrado, faça o login para entrar.');
          }
          APP.component.Alert.customMessage('cpfNaoCadastradoGi');
        },
        complete: function(result) {
          APP.component.Loading.hide();
        }
      });
    });
  },

  salvarDados() {
    $('body').on('click', '#salvar-etapa-03', function(e) {
      e.preventDefault();
      const dados = APP.controller.MeuPerfil.pegarDadosFormulario();
    });
  }
};
