/*
|--------------------------------------------------------------------------
| Erro
|--------------------------------------------------------------------------
*/
APP.component.AlterarTelfone = {
  init() {
    this.setup();
    this.ativarEdicaoTelefone();
  },

  setup() {
    //
  },

  ativarEdicaoTelefone() {
    $('.alterar-telefone').on('click', function(e) {
      e.preventDefault();
      const valorAntigo = $('#info-telefone').val();
      $('#info-telefone').val('');
      $(this).addClass('desactive');
      $('#info-telefone').prop('disabled', false);
      $('#info-telefone').focus();
      $('.telefone-container').addClass('active');
      $('.confirmar-cancelar-telefone').addClass('active');
      APP.component.AlterarTelfone.cancelarEdicao(valorAntigo);
    });
  },

  cancelarEdicao(valor) {
    $('.cancelar-telefone').on('click', function(e) {
      e.preventDefault();
      $('#info-telefone').prop('disabled', true);
      $('#info-telefone').val(valor);
      $('.confirmar-cancelar-telefone').removeClass('active');
      $('.telefone-container').removeClass('active');
      $('.alterar-telefone').removeClass('desactive');
    });
  }
};
