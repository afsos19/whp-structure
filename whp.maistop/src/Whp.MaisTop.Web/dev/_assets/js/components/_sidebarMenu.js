/*
|--------------------------------------------------------------------------
| Erro
|--------------------------------------------------------------------------
*/
APP.component.SidebarMenu = {
  init() {
    this.ativarEdicaoTelefone();
    this.sairDaConta();
  },

  ativarEdicaoTelefone() {
    $('body').on('click', '#alterar-telefone', function(e) {
      e.preventDefault();
      const valorAntigo = $('#info-telefone').val();
      $('#info-telefone').val('');
      $(this).addClass('desactive');
      $('#info-telefone').prop('disabled', false);
      $('#info-telefone').focus();
      $('.telefone-container').addClass('active');
      $('.confirmar-cancelar-telefone').addClass('active');
      APP.component.SidebarMenu.cancelarEdicao(valorAntigo);
    });
  },

  cancelarEdicao(valor) {
    $('body').on('click', '.cancelar-telefone', function(e) {
      e.preventDefault();
      $('#info-telefone').prop('disabled', true);
      $('#info-telefone').val(valor);
      $('.confirmar-cancelar-telefone').removeClass('active');
      $('.telefone-container').removeClass('active');
      $('#alterar-telefone').removeClass('desactive');
    });
  },

  sairDaConta() {
    $('body').on('click', '#sair-conta', function() {
      sessionStorage.removeItem('token');
      window.location.href = '/Login';
    });
  },

  preencheInfosLaterais(result) {
    if (result.number.length == 0 || !result.complement || result.complement.trim().length == 0) {
      APP.component.Modal.closeModal();
      $('#modal').load('/_modais/modalAtualizarCadastro.html', function() {
        $('body').addClass('blockScroll');
        $('#modal').addClass('box-modal-ativo');
        $('.modal').show();
        $('#bg-modal').addClass('active');
        APP.component.Modal.modalAtualizacaoDados(result);
      });
    }
    //
    APP.component.SidebarMenu.preencheProdutosFoco();
    APP.component.SidebarMenu.alterarTelefoneSidebar(result);
    const img = APP.controller.Api.pathImagem();
    //
    const fotoPerfil = !result.photo || result.photo == 'null' || result.photo == '' || result.photo == 'undefined' ? '/_assets/img/usuario/avatar_generico.png' : img + result.photo;
    $('#info-nome').text(result.name.toLowerCase());
    $('#info-nome').attr('data-user', result.cpf);
    const telefone = APP.component.Form.limpaPhone(result.cellPhone);
    $('#info-telefone').val(APP.component.Form.formatCellPhone(telefone));
    $('img#info-foto-header').attr('src', fotoPerfil);
    $('img#info-foto-aside').attr('src', fotoPerfil);
    APP.controller.Main.maskInit();
  },

  preenchePontuacaoLateral(user) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');

    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: `${pathApi.pegarExtratoGeral}?userId=${user.id}`,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        $('#info-pontuacao').text(String(result.balance).includes('.') ? String(result.balance).replace('.', ',') : result.balance);
      },
      error: function(result) {}
    });
  },

  preencheProdutosFoco() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: pathApi.produtosFoco,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      success: function(result) {
        let gruposAdicionado = [];
        const produto = APP.controller.Api.pathProdutos();
        result.forEach(item => {
          let skuProdutos = [];
          skuProdutos.push(item.product.sku);
          if (gruposAdicionado.indexOf(item.groupProduct.name) == -1) {
            $('.produtos-foco-lista').append(`
          <div class="produtos-foco-lista-produtos" data-grupo-supertops="${item.groupProduct.name.toLowerCase().replace(/\s/g, '-')}">
            <p class="produtos-foco-lista-produtos-title">${item.groupProduct.name.toLowerCase()}</p>
            <div class="produtos-foto-tipo">
              <div class="produtos-foto-tipo-foto">
                <picture><img src="${!item.product.photo ? '/_assets/img/produtos/produto-foco-default.png' : produto + item.product.photo}"] .produtos-foto-tipo-foto" /></picture>
              </div>
              <div class="produtos-foto-tipo-code"><p>${item.product.sku}</p></div>
            </div>
          </div>
          `);
            gruposAdicionado.push(item.groupProduct.name);
          } else {
            $(`[data-grupo-supertops="${item.groupProduct.name.toLowerCase().replace(/\s/g, '-')}"] .produtos-foto-tipo-code`).append(`
              <p>${item.product.sku}</p>
              `);
            if (item.product.photo) {
              $(`[data-grupo-supertops="${item.groupProduct.name.toLowerCase().replace(/\s/g, '-')}"] .produtos-foto-tipo-foto`).html('');
              $(`[data-grupo-supertops="${item.groupProduct.name.toLowerCase().replace(/\s/g, '-')}"] .produtos-foto-tipo-foto`).append(`
              <picture><img src="${produto + item.product.photo}" /></picture>
              `);
            }
          }
        });
        const data = new Date();
        const options = { month: 'long' };
        $('#data-produtos-foco').text(`${data.toLocaleDateString('pt-br', options)}/${data.getFullYear()}`);
      },
      error: function(result) {}
    });
  },

  alterarTelefoneSidebar(result) {
    const dados = result;
    $('body').on('click', '.confirmar-telefone', function(e) {
      e.preventDefault();
      const telefone = $('#info-telefone').val();
      if (APP.component.Form.validateCelular(telefone)) {
        const pathApi = APP.controller.Api.pathsApi();
        const token = sessionStorage.getItem('token');
        //FormData
        const formData = new FormData();
        formData.append('Address', dados.address);
        formData.append('BithDate', dados.bithDate);
        formData.append('CellPhone', APP.component.Form.limpaPhone(telefone));
        formData.append('City', dados.city);
        formData.append('CivilStatus', dados.civilStatus);
        formData.append('CEP', dados.cep);
        formData.append('Complement', dados.complement);
        formData.append('CPF', dados.cpf);
        formData.append('Email', dados.email);
        formData.append('Photo', dados.photo);
        formData.append('Gender', dados.gender);
        formData.append('HeartTeam', dados.heartTeam);
        formData.append('Id', dados.id);
        formData.append('Name', dados.name);
        formData.append('Neighborhood', dados.neighborhood);
        formData.append('Number', dados.number);
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
            $('#info-telefone').blur();
            APP.component.Loading.show();
          },
          success: function(result) {
            APP.component.Modal.mensagemModalSuperior('Sucesso!', 'O número do seu telefone foi alterado.');
            $('.cancelar-telefone').click();
            $('#info-telefone').val(telefone);
          },
          error: function(result) {
            APP.component.Modal.mensagemModalSuperior('Erro!', 'Não conseguimos salvar seus dados. Por favor, tente novamente em alguns instantes.');
          },
          complete: function(result) {
            APP.component.Loading.hide();
          }
        });
      } else {
        $('#info-telefone').blur();
        APP.component.Modal.mensagemModalSuperior('Erro', 'Por favor, insira um número de celular válido.');
        APP.component.Modal.closeModalSuperior();
        $('.cancelar-telefone').click();
      }
    });
  }
};
