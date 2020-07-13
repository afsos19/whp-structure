/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.MeuPerfil = {
  init() {
    APP.component.Modal.closeModalSuperior();
    APP.component.Modal.closeModal();
    this.clickNaFotoParaUpload();
    this.verificaTokenOuFirstAcess();
  },

  // Ativa as funções da página se for confirmado primeiro acesso ou que está logado
  acessoPermitido() {
    APP.controller.Main.maskInit();
    this.mudarEtapaMenuSup();
    this.proximaEtapaBtnForm();
    this.maxSizeUpload();
    this.cadastroEndereco();
  },

  // Verifica se tem algum token e envia para o script certo (logado / não logado)
  verificaTokenOuFirstAcess() {
    const token = sessionStorage.getItem('token');
    const firstAcess = sessionStorage.getItem('firstAcess');
    if (!token && !firstAcess) {
      window.location = '/Login';
    } else if (token && !firstAcess) {
      APP.component.MeuPerfilLogado.init();
    } else {
      APP.component.MeuPerfilPrimeiroAcesso.init();
    }
  },

  clickNaFotoParaUpload(){
    $('body').on('click', '.form-imagem-container', function(){
      $('#foto-perfil').click();
    })
  },

  // Funação para limitar tamanho do upload da imagem
  maxSizeUpload() {
    $('#foto-perfil').on('change', function(e) {
      if (e.target.files[0].size > 2097152) {
        APP.component.Modal.mensagemModalSuperior('Erro', 'Tamanho do arquivo muito grande!');
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
  },

  // Muda a etapa do cadastro pelos botões de próximo/anterior
  proximaEtapaBtnForm() {
    const botoesProximaEtapa = $('[data-btn-etapa]');
    botoesProximaEtapa.each(function() {
      $(this).click(function(e) {
        e.preventDefault();
        const proximaEtapa = $(this).data('btn-etapa');
        APP.controller.MeuPerfil.mudaEtapa(proximaEtapa);
      });
    });
  },

  //  Função para verificar qual etapa deve trazer ao clicar no botões de proximo/anterior
  mudaEtapa(etapa) {
    $('[data-btn-topo-etapa]').removeClass('active');
    $('[data-form-etapa]').removeClass('active');
    $(`[data-btn-topo-etapa="${etapa}"]`).addClass('active');
    $(`[data-form-etapa="${etapa}"]`).addClass('active');
  },

  // Muda a etapa do cadastro no menu superior
  mudarEtapaMenuSup() {
    const botoesProximaEtapa = $('[data-btn-topo-etapa]');
    botoesProximaEtapa.each(function() {
      $(this).on('click', function() {
        const etapa = $(this).data('btn-topo-etapa');
        APP.controller.MeuPerfil.mudaEtapa(etapa);
      });
    });
  },

  // Adiciona uma classe para os itens de separação de conteúdo no modal de erro
  agrupamentoDosCamposNoModalDeErro() {
    const itemLista = $('#campos-incorretos-modal li');
    itemLista.each(function() {
      $(this).text() == '1 - Dados Cadastrais' || $(this).text() == '2 - Endereço' || $(this).text() == '3 - Senha'
        ? $(this).addClass('agrupamentoLista')
        : false;
    });
  },

  // Objeto com todos os valores do primeiro acesso
  pegarDadosFormulario() {
    const dadosForm = {
      bairro: $('#bairro').val(),
      celular: $('#celular').val(),
      cep: $('#cep').val(),
      cpfAside: $('[data-user]').data('user'),
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
      regulamento: $('#aceite-regulamento-cadastro'),
      senha: $('#senha').val(),
      senhaAnterior: $('#senha-atual').val(),
      sexo: $('#sexo').val(),
      telefone: $('#telefone').val(),
      timeCoracao: $('#time-coracao').val(),
      uf: $('#uf').val()
    };
    return dadosForm;
  },

  // Faz a busca do CEP e preenche os dados que retornam
  cadastroEndereco: function() {
    $('#cep').on('keyup', function() {
      if ($(this).val().length != 9) {
        return false;
      }
      $.ajax({
        type: 'GET',
        dataType: 'json',
        cache: false,
        url: `https://viacep.com.br/ws/${$('#cep').val()}/json/`,
        success: function(result) {
          if (!result.cep) {
            $('#cep').val('');
            $('#cep').prop('disabled', true);
            setTimeout(() => {
              $('#cep').prop('disabled', false);
              $('#cep').focus();
            }, 1500);
            return APP.component.Modal.mensagemModalSuperior('Erro', 'CEP não encontrado. Tente novamente.');
          }
          $('#endereco').val(result.logradouro);
          $('#bairro').val(result.bairro);
          $('#cidade').val(result.localidade);
          $('#uf').val(result.uf);
          $('#numero').focus();
        },
        error: function(result) {
          APP.component.Modal.mensagemModalSuperior('Erro', 'CEP não encontrado. Tente novamente.');
        }
      });
    });
  }
};
