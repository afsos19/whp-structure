/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.Login = {
  init() {
    this.verificaIpParaLogar();
    APP.component.Modal.closeModalSuperior();
  },

  verificaIpParaLogar() {
    const btnEntrar = $('#btn-entrar');
    btnEntrar.on('click', function(e) {
      e.preventDefault();
      const pathApi = APP.controller.Api.pathsApi();
      $.ajax({
        type: 'GET',
        url: pathApi.verificaIp,
        success: function(result) {
          APP.controller.Login.login(result);
        }
      });
    });
  },

  login(ipUsuario) {
    const dadosLogin = {
      Cpf: $('#cpf')
        .val()
        .replace(/[^\d]+/g, ''),
      Password: $('#senha').val(),
      Ip: ipUsuario
    };
    if (dadosLogin.Cpf === '' || dadosLogin.Password === '') {
      return APP.component.Modal.mensagemModalSuperior('Erro', 'Preencha todos os campos para continuar.');
    } else if (!APP.component.Form.validateCpf(dadosLogin.Cpf)) {
      return APP.component.Modal.mensagemModalSuperior('Erro', 'CPF inválido. Por favor, verifique.');
    }
    const pathApi = APP.controller.Api.pathsApi();
    $.ajax({
      type: 'POST',
      data: JSON.stringify(dadosLogin),
      dataType: 'json',
      contentType: 'application/json',
      cache: false,
      url: pathApi.login,
      beforeSend: function() {
        APP.component.Loading.show();
      },
      success: function(result) {
        sessionStorage.removeItem('firstAcess');
        if (result.message == 'Usuário encontrado com sucesso') {
          sessionStorage.setItem('token', result.token);
          window.location.href = '/';
        } else if (result.message == 'Senha expirada!') {
          sessionStorage.setItem('senhaExpirada', result.token);
          window.location.href = '/SenhaExpirada';
        } else if (result.message == 'Usuario somente catalogo') {
          window.open(result.link);
        }
      },
      error: function(result) {
        APP.component.Modal.mensagemModalSuperior('Erro!', 'Usuário ou senha inválidos. Tente novamente.');
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  }
};
