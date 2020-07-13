/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.MinhaEquipe = {
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
    if (perfil !== 'GERENTE REGIONAL' && perfil !== 'GERENTE' && perfil !== 'ACESSO ESPELHO') {
      window.location = '/';
    } else {
      $('#app').load('/_logadas/minha-equipe.html', function() {
        APP.component.Acessos.init(result, decoded);
        APP.controller.MinhaEquipe.start();
        APP.controller.MinhaEquipe.trocaAbas();
        //
        APP.controller.MinhaEquipe.criarFiltros(perfil);
        APP.controller.MinhaEquipe.filtroDadosCadastro();
        APP.controller.MinhaEquipe.filtroDadosTreinamento();
        APP.controller.MinhaEquipe.filtroDadosVendas();
        APP.controller.Main.ativaMenuPage();
      });
    }
  },

  start() {
    $('[data-aba-equipe="cadastro"]').addClass('active');
    $('[data-equipe-conteudo="cadastro"]').addClass('active');
  },

  trocaAbas() {
    $('body').on('click', '[data-aba-equipe]', function() {
      const nomeAba = $(this).data('aba-equipe');
      $('[data-aba-equipe]').removeClass('active');
      $('[data-equipe-conteudo]').removeClass('active');
      $(this).addClass('active');
      $(`[data-equipe-conteudo="${nomeAba}"]`).addClass('active');
    });
  },

  criarFiltros(perfil) {
    if (perfil == 'GERENTE') {
      $('#minha-equipe-cadastro-loja, #minha-equipe-treinamentos-loja, #minha-equipe-vendas-loja').prop('disabled', true);
    }
    APP.controller.MinhaEquipe.carregasLojasPerfil();
    const data = new Date().toLocaleDateString().split('/');
    const mesAtual = data[1];
    const anoAtual = data[2];
    $('#minha-equipe-cadastro-mes').val(mesAtual);
    $('#minha-equipe-treinamentos-mes').val(mesAtual);
    $('#minha-equipe-vendas-mes').val(mesAtual);
    for (let ano = anoAtual; ano > 2016; ano--) {
      $('#minha-equipe-cadastro-ano').append(`
        <option value="${ano}">${ano}</option>
      `);
      $('#minha-equipe-treinamentos-ano').append(`
        <option value="${ano}">${ano}</option>
      `);
      $('#minha-equipe-vendas-ano').append(`
        <option value="${ano}">${ano}</option>
      `);
    }
    setTimeout(() => {
      APP.controller.MinhaEquipe.carregaDadosIniciais();
    }, 1000);
  },

  carregasLojasPerfil() {
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
          $('#minha-equipe-cadastro-loja, #minha-equipe-treinamentos-loja, #minha-equipe-vendas-loja').append(`
            <option value="${item.id}">${item.name}</option>
          `);
        });
      }
    });
  },

  carregaDadosIniciais() {
    APP.controller.MinhaEquipe.carregaDadosCadastro();
    APP.controller.MinhaEquipe.carregaDadosTreinamento();
    APP.controller.MinhaEquipe.carregaDadosVendas();
  },

  filtroDadosCadastro() {
    $('body').on('change', '[data-equipe-conteudo="cadastro"] select', function() {
      APP.controller.MinhaEquipe.carregaDadosCadastro();
    });
  },

  filtroDadosTreinamento() {
    $('body').on('change', '[data-equipe-conteudo="treinamentos"] select', function() {
      APP.controller.MinhaEquipe.carregaDadosTreinamento();
    });
  },

  filtroDadosVendas() {
    $('body').on('change', '[data-equipe-conteudo="vendas"] select', function() {
      APP.controller.MinhaEquipe.carregaDadosVendas();
    });
  },

  carregaDadosCadastro() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const data = {
      Shop: $('#minha-equipe-cadastro-loja').val(),
      CurrentMonth: $('#minha-equipe-cadastro-mes').val(),
      CurrentYear: $('#minha-equipe-cadastro-ano').val()
    };
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.equipeCadastro,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify(data),
      beforeSend: function() {
        APP.component.Loading.show();
        $('#tabela-cadastro-equipe').html('');
      },
      success: function(result) {
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="ativos"] .minha-equipe-graficos-procentagem').text(`${result.activated}%`);
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="ativos"] .minha-equipe-graficos-item-barra div').css('width', `${result.activated}%`);
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="pre-cadastro"] .minha-equipe-graficos-procentagem').text(`${result.preRegistered}%`);
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="pre-cadastro"] .minha-equipe-graficos-item-barra div').css('width', `${result.preRegistered}%`);
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="inativos"] .minha-equipe-graficos-procentagem').text(`${result.inactivated}%`);
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="inativos"] .minha-equipe-graficos-item-barra div').css('width', `${result.inactivated}%`);
        $('#datatable-cadastro').DataTable({
          data: result.userList,
          bProcessing: true,
          destroy: true,
          deferRender: true,
          ordering: false,
          columns: [{ title: 'Nome', data: 'name' }, { title: 'Cargo', data: 'office.description' }, { title: 'Status', data: 'userStatus.description' }],
          oLanguage: {
            sProcessing: 'Processando...',
            sLengthMenu: 'Mostrar _MENU_ registros por página',
            sSearch: 'Pesquisar:',
            sInfo: 'Mostrar _START_ até _END_ de _MAX_ registros',
            sInfoFiltered: '(de um total de _MAX_ registros)',
            sInfoEmpty: 'Mostrando 0 até 0 de 0 registros',
            sZeroRecords: 'Nenhum resultado encontrado',
            sInfoPostFix: '',
            sInfoThousands: '',
            sUrl: '',
            oPaginate: {
              sFirst: 'Primeira',
              sPrevious: 'Anterior',
              sNext: 'Próxima',
              sLast: 'Última'
            }
          }
        });
      },
      error: function(result) {
        if ($('#datatable-cadastro').hasClass('dataTable')) {
          $('#datatable-cadastro')
            .DataTable()
            .destroy();
          $('#datatable-cadastro').html('');
        }
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="ativos"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="ativos"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="pre-cadastro"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="pre-cadastro"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="inativos"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="cadastro"] [data-grupo-equipe="inativos"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('#tabela-cadastro-equipe').append(`
        <div class="minha-equipe-tabela-tabela-item">
          <div class="minha-equipe-tabela-tabela-item-nome">
            <p>Nenhum dado encontrado</p>
          </div>
        </div>
        `);
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  carregaDadosTreinamento() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const data = {
      Shop: $('#minha-equipe-treinamentos-loja').val(),
      CurrentMonth: $('#minha-equipe-treinamentos-mes').val(),
      CurrentYear: $('#minha-equipe-treinamentos-ano').val()
    };
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.equipeTreinamento,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify(data),
      beforeSend: function() {
        APP.component.Loading.show();
        $('#tabela-treinamentos-equipe').html('');
      },
      success: function(result) {
        let idAnterior;
        const treinamentos = [];
        result.trainingList.forEach(item => {
          const idAtual = item.user.id;
          if (idAtual == idAnterior) {
            treinamentos[treinamentos.length - 1].treinamentoDois = item.trainingStatus;
          } else {
            treinamentos.push({
              nome: item.user.name,
              treinamentoUm: item.trainingStatus,
              treinamentoDois: 'PENDENTE'
            });
            idAnterior = idAtual;
          }
        });
        //
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="um-treinamento"] .minha-equipe-graficos-procentagem').text(`${result.trainingOneDone}%`);
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="um-treinamento"] .minha-equipe-graficos-item-barra div').css('width', `${result.trainingOneDone}%`);
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="dois-treinamentos"] .minha-equipe-graficos-procentagem').text(`${result.trainingTwoDone}%`);
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="dois-treinamentos"] .minha-equipe-graficos-item-barra div').css('width', `${result.trainingTwoDone}%`);
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="sem-treinamento"] .minha-equipe-graficos-procentagem').text(`${result.trainingNotDone}%`);
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="sem-treinamento"] .minha-equipe-graficos-item-barra div').css('width', `${result.trainingNotDone}%`);
        $('#datatable-treinamentos').DataTable({
          data: treinamentos,
          bProcessing: true,
          destroy: true,
          deferRender: true,
          ordering: false,
          columns: [{ title: 'Nome', data: 'nome' }, { title: 'Treinamento 01', data: 'treinamentoUm' }, { title: 'Treinamento 02', data: 'treinamentoDois' }],
          oLanguage: {
            sProcessing: 'Processando...',
            sLengthMenu: 'Mostrar _MENU_ registros por página',
            sSearch: 'Pesquisar:',
            sInfo: 'Mostrar _START_ até _END_ de _MAX_ registros',
            sInfoFiltered: '(de um total de _MAX_ registros)',
            sInfoEmpty: 'Mostrando 0 até 0 de 0 registros',
            sZeroRecords: 'Nenhum resultado encontrado',
            sInfoPostFix: '',
            sInfoThousands: '',
            sUrl: '',
            oPaginate: {
              sFirst: 'Primeira',
              sPrevious: 'Anterior',
              sNext: 'Próxima',
              sLast: 'Última'
            }
          }
        });
      },
      error: function(result) {
        if ($('#datatable-treinamentos').hasClass('dataTable')) {
          $('#datatable-treinamentos')
            .DataTable()
            .destroy();
          $('#datatable-treinamentos').html('');
        }
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="um-treinamento"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="um-treinamento"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="dois-treinamentos"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="dois-treinamentos"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="sem-treinamento"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="treinamentos"] [data-grupo-equipe="sem-treinamento"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('#tabela-treinamentos-equipe').append(`
        <div class="minha-equipe-tabela-tabela-item">
          <div class="minha-equipe-tabela-tabela-item-nome">
            <p>Nenhum dado encontrado</p>
          </div>
        </div>
        `);
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  carregaDadosVendas() {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const data = {
      Shop: $('#minha-equipe-vendas-loja').val(),
      CurrentMonth: $('#minha-equipe-vendas-mes').val(),
      CurrentYear: $('#minha-equipe-vendas-ano').val()
    };
    $.ajax({
      async: true,
      crossDomain: true,
      url: pathApi.equipeVendas,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify(data),
      beforeSend: function() {
        APP.component.Loading.show();
        $('#tabela-vendas-equipe').html('');
      },
      success: function(result) {
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-participantes"] .minha-equipe-graficos-procentagem').text(`${result.participant}%`);
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-participantes"] .minha-equipe-graficos-item-barra div').css('width', `${result.participant}%`);
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-supertops"] .minha-equipe-graficos-procentagem').text(`${result.superTop}%`);
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-supertops"] .minha-equipe-graficos-item-barra div').css('width', `${result.superTop}%`);
        $('#datatable-vendas').DataTable({
          data: result.listSale,
          bProcessing: true,
          destroy: true,
          deferRender: true,
          ordering: false,
          columns: [{ title: 'Nome', data: 'name' }, { title: 'Produtos Participantes', data: 'participant' }, { title: 'Produtos Supertops', data: 'superTop' }],
          oLanguage: {
            sProcessing: 'Processando...',
            sLengthMenu: 'Mostrar _MENU_ registros por página',
            sSearch: 'Pesquisar:',
            sInfo: 'Mostrar _START_ até _END_ de _MAX_ registros',
            sInfoFiltered: '(de um total de _MAX_ registros)',
            sInfoEmpty: 'Mostrando 0 até 0 de 0 registros',
            sZeroRecords: 'Nenhum resultado encontrado',
            sInfoPostFix: '',
            sInfoThousands: '',
            sUrl: '',
            oPaginate: {
              sFirst: 'Primeira',
              sPrevious: 'Anterior',
              sNext: 'Próxima',
              sLast: 'Última'
            }
          }
        });
      },
      error: function(result) {
        if ($('#datatable-vendas').hasClass('dataTable')) {
          $('#datatable-vendas')
            .DataTable()
            .destroy();
          $('#datatable-vendas').html('');
        }
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-participantes"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-participantes"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-supertops"] .minha-equipe-graficos-procentagem').text('0%');
        $('[data-equipe-conteudo="vendas"] [data-grupo-equipe="produtos-supertops"] .minha-equipe-graficos-item-barra div').css('width', '0%');
        $('#tabela-vendas-equipe').append(`
        <div class="minha-equipe-tabela-tabela-item">
          <div class="minha-equipe-tabela-tabela-item-nome">
            <p>Nenhum dado encontrado</p>
          </div>
        </div>
        `);
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  }
};
