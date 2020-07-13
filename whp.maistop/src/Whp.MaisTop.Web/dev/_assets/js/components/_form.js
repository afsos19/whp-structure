/*
|--------------------------------------------------------------------------
| Form
|--------------------------------------------------------------------------
*/

APP.component.Form = {
  init() {},

  // VALIDAÇÔES ###############################################################################################################################################

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
    var re = /^(?:\()[0-9]{2}(?:\))\s?[0-9]{5}(?:-)[0-9]{4}$/;
    return re.test(String(celular));
  },

  validateTelefone(telefone) {
    var re = /^(?:\()[0-9]{2}(?:\))\s?[0-9]{4}(?:-)[0-9]{4,5}$/;
    return re.test(String(telefone));
  },

  validateCep(cep) {
    var re = /^[0-9]{5}(?:-)[0-9]{3}$/;
    return re.test(String(cep));
  },

  validateSenha(senha) {
    var re = /^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[\!\@\#\$\%\&\?]).*$/;
    // var re = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    return re.test(String(senha));
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

  segurancaDoEmail(email) {
    const regexp = /(^.{3})(.*)[\w]*@(...*)/;
    const matches = regexp.exec(email);
    const emailSeguranca = `${matches[1]}${'*'.repeat(matches[2].length)}@${matches[3]}`;
    return emailSeguranca;
  },

  segurancaDoTelefone(mensagem) {
    const regexp = /(^.{3})(.*)[\w]*@(...*)/;
    const matches = regexp.exec(email);
    const emailSeguranca = `${matches[1]}${'*'.repeat(matches[2].length)}@${matches[3]}`;
    return emailSeguranca;
  },

  // FORMATAÇÔES ###############################################################################################################################################

  formatDate(date) {
    return date
      .slice(0, 10)
      .split('-')
      .reverse()
      .join('/');
  },

  formatCep(cep) {
    return `${cep.substring(0, 5)}-${cep.substring(5, 8)}`;
  },

  formatCellPhone(cell) {
    return `(${cell.substring(0, 2)}) ${cell.substring(2, 7)}-${cell.substring(7)}`;
  },

  formatPhone(phone) {
    return `(${phone.substring(0, 2)}) ${phone.substring(2, 6)}-${phone.substring(6)}`;
  },

  formatDataNascParaBanco(data) {
    return data
      .split('/')
      .reverse()
      .join('-');
  },

  limpaPhone(phone) {
    return phone.replace(/[^\d]+/g, '');
  },

  limpaCep(cep) {
    return cep.replace('-', '');
  },

  limpaCpf(cpf) {
    return cpf.replace(/[\.\-]+/g, '');
  }
};
