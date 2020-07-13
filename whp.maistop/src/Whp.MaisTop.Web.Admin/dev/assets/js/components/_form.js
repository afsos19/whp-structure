/*
|--------------------------------------------------------------------------
| Form
|--------------------------------------------------------------------------
*/

APP.component.Form = {
  init() {
    this.setup();
  },

  setup() {},

  validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  },

  validateCpf(cpf) {
    var numeros, digitos, soma, i, resultado, digitos_iguais;
    digitos_iguais = 1;
    if (cpf.length < 11) return false;
    for (i = 0; i < cpf.length - 1; i++)
      if (cpf.charAt(i) != cpf.charAt(i + 1)) {
        digitos_iguais = 0;
        break;
      }
    if (!digitos_iguais) {
      numeros = cpf.substring(0, 9);
      digitos = cpf.substring(9);
      soma = 0;
      for (i = 10; i > 1; i--) soma += numeros.charAt(10 - i) * i;
      resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
      if (resultado != digitos.charAt(0)) return false;
      numeros = cpf.substring(0, 10);
      soma = 0;
      for (i = 11; i > 1; i--) soma += numeros.charAt(11 - i) * i;
      resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
      if (resultado != digitos.charAt(1)) return false;
      return true;
    } else return false;
  },

  validateCelular(celular) {
    var re = /^(?:\()[0-9]{2}(?:\))\s?[0-9]{4,5}(?:-)[0-9]{4}$/;
    return re.test(String(celular));
  },

  validateDataNascimento(data) {
    if (data == null) {
      return false;
    } else {
      const camposDataNascimento = data.split('/');
      if (
        data.length == 10 &&
        camposDataNascimento[0] > 0 &&
        camposDataNascimento[0] <= 31 &&
        camposDataNascimento[1] <= 12 &&
        camposDataNascimento[2] >= 1900 &&
        camposDataNascimento[2] <= new Date().getFullYear() - 18
      ) {
        return true;
      } else {
        return false;
      }
    }
  },

  maxSizeUpload() {
    $('#foto-perfil').on('change', function(e) {
      if (e.target.files[0].size > 2097152) {
        alert('Tamanho do arquivo muito grande!');
        $(this).val('');
        return false;
      }
      const input = this;
      if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
          $('.preview-img').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
      }
    });
  }
};
