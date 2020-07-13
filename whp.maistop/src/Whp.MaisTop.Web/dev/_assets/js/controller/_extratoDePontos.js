/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/
APP.controller.ExtratoDePontos = {
  init() {
    const token = sessionStorage.getItem('token');
    if (!token) {
      window.location = '/Login';
    } else {
      APP.component.Acessos.verificaAcesso();
    }
  },

  acessoPermitido(result, decoded) {
    $('#app').load('/_logadas/extrato-de-pontos.html', function() {
      APP.component.Acessos.init(result, decoded);
      APP.controller.ExtratoDePontos.mudaAbas();
      APP.controller.ExtratoDePontos.abaVendasRealizadas(decoded);
      APP.controller.ExtratoDePontos.criarFiltro();
      APP.controller.ExtratoDePontos.carregaPontuacaoInicial(result);
      APP.controller.ExtratoDePontos.filtrosExtrato();
    });
  },

  mudaAbas() {
    const abas = $('[data-extrato-target]');
    abas.each(function() {
      $(this).on('click', function() {
        const target = $(this).data('extrato-target');
        $('[data-extrato-target]').removeClass('active');
        $(this).addClass('active');
        $('[data-extrato]').removeClass('active');
        $(`[data-extrato="${target}"]`).addClass('active');
        target == 'extrato' ? $('.barra-superior-extrato-data').fadeOut() : $('.barra-superior-extrato-data').fadeIn();
      });
    });
  },

  abaVendasRealizadas(decoded) {
    const perfil = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (perfil != 'VENDEDOR' && perfil != 'ACESSO ESPELHO') {
      $('[data-extrato-target="vendas-realizadas"]').remove();
    }
  },

  criarFiltro() {
    const data = new Date().toLocaleDateString().split('/');
    const mesAtual = data[1];
    const anoAtual = data[2];
    $('#extrato-pontos-mes').val(mesAtual);
    for (let ano = anoAtual; ano > 2016; ano--) {
      $('#extrato-pontos-ano').append(`
        <option value="${ano}">${ano}</option>
      `);
    }
  },

  carregaPontuacaoInicial(result) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    $.ajax({
      type: 'GET',
      dataType: 'json',
      contentType: 'application/json',
      url: `${pathApi.pegarExtratoGeral}?userId=${result.id}`,
      cache: false,
      headers: { Authorization: `Bearer ${token}` },
      beforeSend: function() {
        const dataSplit = new Date().toLocaleDateString().split('/');
        if (dataSplit[0] <= 15) {
          $('#data-pontos-a-expirar').text(`15/${dataSplit[1]}/${dataSplit[2]}`);
        } else {
          $('#data-pontos-a-expirar').text(`15/${Number(dataSplit[1]) + 1}/${dataSplit[2]}`);
        }
      },
      success: function(result) {
        const data = result.dateLastCredit.split('T');
        $('#creditos-totais').text(String(result.credit).replace('.', ','));
        $('#debitos-totais').text(String(result.debit).replace('.', ','));
        $('#pontos-expirados').text(String(result.expiredPunctuation).replace('.', ','));
        $('#pontos-a-expirar').text(String(result.nextAmoutToExpire).replace('.', ','));
        $('.extrato-total-de-pontos').append(`
          <p class="extrato-total-de-pontos-pontos">${
            String(result.balance).includes('.') ? String(result.balance).replace('.', ',') : result.balance
          }<span class="extrato-total-de-pontos-pts">pts</span></p>
        `);
        $('.extrato-pontos-ultimo-credito').append(`
          <p>Último crédito: <span>${data[0]
            .split('-')
            .reverse()
            .join('/')} - ${data[1].slice(0, 5)}</span></p>
        `);
        const filtro = `{"currentMonth": ${parseInt($('#extrato-pontos-mes').val())}, "currentYear": ${$('#extrato-pontos-ano').val()} }`;
        APP.controller.ExtratoDePontos.filtroCreditos(pathApi, token, filtro);
        APP.controller.ExtratoDePontos.filtroVendas(pathApi, token, filtro);
        APP.controller.ExtratoDePontos.filtroHistorico(pathApi, token, filtro);
      },
      error: function(result) {
        $('#creditos-totais').text('0');
        $('#debitos-totais').text('0');
        $('#pontos-expirados').text('0');
        $('#pontos-a-expirar').text('0');
        $('.extrato-total-de-pontos').append(`
          <p class="extrato-total-de-pontos-pontos">0<span class="extrato-total-de-pontos-pts">pts</span></p>
        `);
        $('[data-extrato="creditos"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
        $('[data-extrato="vendas-realizadas"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
        $('[data-extrato="historico-de-resgates"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
      }
    });
  },

  filtrosExtrato() {
    $('body').on('change', '#extrato-pontos-mes, #extrato-pontos-ano', function() {
      const pathApi = APP.controller.Api.pathsApi();
      const token = sessionStorage.getItem('token');
      const filtro = `{"currentMonth": ${parseInt($('#extrato-pontos-mes').val())}, "currentYear": ${$('#extrato-pontos-ano').val()} }`;
      APP.controller.ExtratoDePontos.filtroCreditos(pathApi, token, filtro);
      APP.controller.ExtratoDePontos.filtroVendas(pathApi, token, filtro);
      APP.controller.ExtratoDePontos.filtroHistorico(pathApi, token, filtro);
    });
  },

  filtroCreditos(pathApi, token, filtro) {
    $('[data-extrato="creditos"] .extrato-pontos-extrato').html('');
    $.ajax({
      url: pathApi.filtroExtrato,
      method: 'POST',
      cache: false,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: filtro,
      success: function(result) {
        $('[data-extrato="creditos"] .extrato-pontos-extrato').append(`
          <div class="extrato-pontos-extrato-item extrato-topo">
            <p>Tipo</p>
            <p class="creditos-pontos">Pontos</p>
          </div>
        `);
        result.forEach(item => {
          if ($(`.extrato-pontos-extrato-item[data-creditos="${item.description}"]`).length > 0) {
            $(`[data-creditos="${item.description}"] .creditos-pontos`).text(+$(`[data-creditos="${item.description}"] .creditos-pontos`).text() + item.punctuation);
          } else {
            $('[data-extrato="creditos"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item" data-creditos="${item.description}">
            <p>${item.description}</p>
            <p class="creditos-pontos">${String(item.punctuation)}</p>
            </div>
            `);
          }
        });
      },
      error: function(result) {
        $('[data-extrato="creditos"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
      }
    });
  },

  filtroVendas(pathApi, token, filtro) {
    $('[data-extrato="vendas-realizadas"] .extrato-pontos-extrato').html('');
    $.ajax({
      url: pathApi.filtroVendas,
      method: 'POST',
      cache: false,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: filtro,
      success: function(result) {
        $('[data-extrato="vendas-realizadas"] .extrato-pontos-extrato').append(`
          <div class="extrato-pontos-extrato-item extrato-topo">
            <p class="creditos-sku">SKU</p>
            <p class="creditos-nome">Categoria</p>
            <p class="creditos-quantidade">Qtde</p>
            <p class="creditos-pontos pontuacao-vendas-realizadas">Total</p>
          </div>
        `);
        result.forEach(item => {
          if ($(`[data-extrato="vendas-realizadas"] [data-sku="${item.product.sku}"]`).length > 0) {
            const produtoQuantidade = $(`[data-sku="${item.product.sku}"] .creditos-quantidade`);
            const produtoTotalValor = $(`[data-sku="${item.product.sku}"] .creditos-pontos`);
            produtoQuantidade.text(+produtoQuantidade.text() + item.amout);
            produtoTotalValor.text(String(+produtoTotalValor.text() + item.punctuation));
          } else {
            $('[data-extrato="vendas-realizadas"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item" data-sku="${item.product.sku}">
              <p class="creditos-sku">${item.product.sku}</p>
              <p class="creditos-nome">${item.product.categoryProduct.name}</p>
              <p class="creditos-quantidade">${item.amout}</p>
              <p class="creditos-pontos pontuacao-vendas-realizadas">${String(item.punctuation)}</p>
            </div>
          `);
          }
        });
      },
      error: function(result) {
        $('[data-extrato="vendas-realizadas"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
      }
    });
  },

  filtroHistorico(pathApi, token, filtro) {
    $('[data-extrato="historico-de-resgates"] .extrato-pontos-extrato').html('');
    $.ajax({
      url: pathApi.historico,
      method: 'POST',
      cache: false,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: filtro,
      success: function(result) {
        $('[data-extrato="historico-de-resgates"] .extrato-pontos-extrato').append(`
          <div class="extrato-pontos-extrato-item extrato-topo">
            <p class="campo-historico-numero">Número</p>
            <p class="campo-historico-data">Data</p>
            <p class="campo-historico-pedido">Descrição</p>
            <p class="campo-historico-status">Status</p>
            <p class="creditos-pontos pontos">Valor</p>
          </div>
        `);
        result.forEach(item => {
          const data = item.createdAt.split('T');
          $('[data-extrato="historico-de-resgates"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item">
              <p class="campo-historico-numero">${item.externalOrderId}</p>
              <p class="campo-historico-data">${data[0]
                .split('-')
                .reverse()
                .join('/')}</p>
              <p class="campo-historico-pedido">${item.description}</p>
              <p class="campo-historico-status">${item.orderStatus.name}</p>
              <p class="creditos-pontos pontos">${String(item.total).replace('.', ',')}</p>
            </div>
          `);
        });
      },
      error: function(result) {
        $('[data-extrato="historico-de-resgates"] .extrato-pontos-extrato').append(`
            <div class="extrato-pontos-extrato-item-vazio">
              <p>Sem dados para o período</p>
            </div>
        `);
      }
    });
  }
};
