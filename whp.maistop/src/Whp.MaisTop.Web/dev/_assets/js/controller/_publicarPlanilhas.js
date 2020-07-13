/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.PublicarPlanilhas = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    const perfil = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (perfil !== 'GESTOR DA INFORMACAO' && perfil !== 'ACESSO ESPELHO') {
      window.location = '/';
    } else {
      $('#app').load('/_logadas/publicar-planilhas.html', function() {
        APP.component.Acessos.init(result, decoded);
        // $(`[data-planilha="home"]`).addClass('active');

        APP.controller.PublicarPlanilhas.acessaFuncnionariosVendas();
        APP.controller.PublicarPlanilhas.voltarTelaPesquisa();
        APP.controller.PublicarPlanilhas.criarFiltro();
        APP.controller.PublicarPlanilhas.carregarLojasPerfil();
        APP.controller.PublicarPlanilhas.uploadArquivo();
        //
        APP.controller.PublicarPlanilhas.verificarStatus();
        //
        APP.controller.PublicarPlanilhas.subirArquivoFuncionariosVendas();
        //
        APP.component.PlanilhasFuncionarios.reiniciarSubidaFuncionarios();
        APP.component.PlanilhasVendas.reiniciarSubidaVendas();
        APP.controller.Main.ativaMenuPage();
      });
    }
  },

  acessaFuncnionariosVendas() {
    $('body').on('click', '.btns-envio-arquivos-planilhas', function(e) {
      e.preventDefault();
      const aba = $(this).data('arquivo');
      $('[data-planilha]').removeClass('active');
      $(`[data-planilha="${aba}"]`).addClass('active');
    });
  },

  voltarTelaPesquisa() {
    $('body').on('click', '.btn-voltar-pesquisa', function(e) {
      e.preventDefault();
      $('[data-planilha]').removeClass('active');
      $('[data-planilha="home"]').addClass('active');
    });
  },

  criarFiltro() {
    const data = new Date().toLocaleDateString().split('/');
    let mesAtual = data[1];
    let anoAtual = data[2];
    if (mesAtual == '01') {
      mesAtual = '12';
      anoAtual--;
    } else {
      mesAtual <= '10' ? (mesAtual = '0' + (mesAtual - 1)) : mesAtual--;
    }
    for (let ano = anoAtual; ano > 2016; ano--) {
      $('.subida-planilhas-dados-importacao-ano').append(`
        <option value="${ano}">${ano}</option>
      `);
    }
    for (let mes = 1; mes <= mesAtual; mes++) {
      $('.subida-planilhas-dados-importacao-mes').append(`
        <option value="${mes < 10 ? '0' + mes : mes}">${mes < 10 ? '0' + mes : mes}</option>
      `);
    }
    //
    $('.subida-planilhas-dados-importacao-mes').val(mesAtual);
    $('.subida-planilhas-dados-importacao-ano').val(anoAtual);
    //
    $('body').on('change', '[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-ano', function() {
      $('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-mes').html('');
      if ($('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-ano').val() == anoAtual) {
        for (let mes = 1; mes <= mesAtual; mes++) {
          $('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-mes').append(`
            <option value="${mes < 10 ? '0' + mes : mes}">${mes < 10 ? '0' + mes : mes}</option>
          `);
        }
      } else {
        for (let mes = 1; mes <= 12; mes++) {
          $('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-mes').append(`
            <option value="${mes < 10 ? '0' + mes : mes}">${mes < 10 ? '0' + mes : mes}</option>
          `);
        }
      }
    });
    $('body').on('change', '[data-planilha="vendas"] .subida-planilhas-dados-importacao-ano', function() {
      $('[data-planilha="vendas"] .subida-planilhas-dados-importacao-mes').html('');
      if ($('[data-planilha="vendas"] .subida-planilhas-dados-importacao-ano').val() == anoAtual) {
        for (let mes = 1; mes <= mesAtual; mes++) {
          $('[data-planilha="vendas"] .subida-planilhas-dados-importacao-mes').append(`
            <option value="${mes < 10 ? '0' + mes : mes}">${mes < 10 ? '0' + mes : mes}</option>
          `);
        }
      } else {
        for (let mes = 1; mes <= 12; mes++) {
          $('[data-planilha="vendas"] .subida-planilhas-dados-importacao-mes').append(`
            <option value="${mes < 10 ? '0' + mes : mes}">${mes < 10 ? '0' + mes : mes}</option>
          `);
        }
      }
    });
  },

  carregarLojasPerfil() {
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
        result.forEach(item => {
          $('.subida-planilhas-dados-importacao-rede').append(`
            <option value="${item.id}">${item.network.name}</option>
          `);
        });
      }
    });
  },

  verificarStatus() {
    $('body').on('click', '.subida-planilhas-dados-importacao-buscar', function(e) {
      e.preventDefault();
      const tipo = $(this)
        .closest('[data-planilha]')
        .data('planilha');
      if (tipo == 'funcionarios') {
        APP.component.PlanilhasFuncionarios.envioStatusFuncionarios();
      }
      if (tipo == 'vendas') {
        APP.component.PlanilhasVendas.envioStatusVendas();
      }
    });
  },

  uploadArquivo() {
    $('body').on('change', '#importar-arquivo-funcionarios-anexo, #importar-arquivo-vendas-anexo', function() {
      if ($(this)[0].files[0]) {
        $(this)
          .parent()
          .find('.subida-planilhas-upload-arquivo-info-arquivo-nome')
          .text($(this)[0].files[0].name);
      } else {
        $(this)
          .parent()
          .find('.subida-planilhas-upload-arquivo-info-arquivo-nome')
          .text('');
      }
    });
  },

  subirArquivoFuncionariosVendas() {
    $('body').on('click', '#importar-arquivo-funcionarios, #importar-arquivo-vendas', function(e) {
      e.preventDefault();
      const tipo = $(this)
        .closest('[data-planilha]')
        .data('planilha');
      APP.controller.PublicarPlanilhas.montarObjetoEvnio(tipo);
    });
  },

  montarObjetoEvnio(perfil) {
    if (perfil == 'funcionarios') {
      APP.component.PlanilhasFuncionarios.enviarPlanilhaFuncionarios(perfil);
    }
    if (perfil == 'vendas') {
      APP.component.PlanilhasVendas.enviarPlanilhaVendas(perfil);
    }
  }
};
