APP.component.ValidaData = {
  init: function() {
    this.setup();
  },

  setup: function() {},

  validaData: function(valor) {
    let date = valor;
    let ardt = new Array();
    const ExpReg = new RegExp(
      "(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}"
    );
    let erro = false;

    ardt = date.split("/");

    if (date == "") {
      erro == false;
    } else if (
      (ardt[1] == 4 || ardt[1] == 6 || ardt[1] == 9 || ardt[1] == 11) &&
      ardt[0] > 30
    ) {
      erro = true;
    } else if (ardt[1] == 2) {
      if (ardt[0] > 28 && ardt[2] % 4 != 0) {
        erro = true;
      }
      if (ardt[0] > 29 && ardt[2] % 4 == 0) {
        erro = true;
      }
    } else if (ardt[2] < 1900) {
      erro = true;
    } else if (date.search(ExpReg) == -1) {
      erro = true;
    }

    if (erro) return false;

    return true;
  }
};
