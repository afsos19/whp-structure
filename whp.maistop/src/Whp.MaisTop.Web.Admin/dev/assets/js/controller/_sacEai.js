/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.SacEai = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/SACEAI') !== -1) {
      $('#cpf').mask('000.000.000-00');
      APP.controller.Main.getUser();
      this.verificaLogado();
      this.toogleLinks();
      this.toggleLimparOcorrencia();
      this.getusuarioOcorrencia();
      this.enviarMensagem();
      this.getAssuntos();
      this.salvarOcorrenciaEai();
    }
  },
  getAssuntos: function () {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: pathsApi.GetAssuntos,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        response.map(item => {
          if (item.description.indexOf('CATÁLOGO') !== -1) {

            $('#idAssuntoCadastro').append(`
            <option value="${item.id}" >${item.description}</option>
          `);
          }
        });


        $('#subject').append(`
          <option value="0" >Todos</option>
        `);
        response.map(item => {
          if (item.description.indexOf('CATÁLOGO') !== -1) {
            $('#subject').append(`
               <option value="${item.id}" >${item.description}</option>
             `);
          }
        });

        $('#idAssunto').append(`
        <option value="0" >Todos</option>
      `);
        response.map(item => {
          $('#idAssunto').append(`
             <option value="${item.id}" >${item.description}</option>
           `);
        });

      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });
  },

  enviarMensagem: function () {

    $('#btnSalvarEnviarMensagemEai').click(function (e) {

      if ($('#txtDescricaoEai').val() == '' || $('#idOcorrenciaEai').val() == '1') {
        APP.component.Alerta.alerta('Erro!', 'Preencha a mensagem!');
      } else {

        const formData = new FormData();
        formData.append('Occurrence.Id', $('#idOcorrenciaEai').val());
        formData.append('OccurrenceMessageTypeId', 3);
        formData.append('Internal', true);
        formData.append('Catalog', true);
        formData.append('Message', $('#txtDescricaoEai').val());

        if (!!$('#anexoMensagem')[0].files[0]) {
          formData.append('formFile', $('#anexoMensagem')[0].files[0]);
        }

        const pathsApi = APP.controller.Api.pathApi();
        const token = sessionStorage.getItem('tokenAdmin');

        $.ajax({
          type: 'POST',
          data: formData,
          contentType: false,
          processData: false,
          cache: false,
          url: pathsApi.salvarMensagem,
          headers: { Authorization: `Bearer ${token}` },
          beforeSend: function () {
            APP.controller.Main.getUser();
            APP.component.Loading.show();
          },
          success: function (result) {
            console.log("eai");
            $('#txtDescricaoEai').val('');
            $('#anexoMensagem').val('');
            APP.component.Alerta.alerta(
              'Sucesso!',
              'Mensagem enviada com sucesso!'
            );
            location.reload();
          },
          error: function (result) {
            APP.controller.Main.getUser();
            APP.component.Alerta.alerta(
              'Falha',
              'Por favor, tente novamente em alguns instantes.'
            );
          },
          complete: function (result) {
            APP.component.Loading.hide();
          }
        });
      }
    });
  },

  verificaLogado: function () {
    const token = sessionStorage.getItem('tokenAdmin');

    if (window.location.href.indexOf('detalhe') > -1) {
      var id = window.location.href.toUpperCase().split('=')[1];
      this.loadDetalheOcorrencia(id);
    }

  },

  toogleLinks: function () {
    $('.toggle-link').click(function (e) {
      e.preventDefault();
      $(this)
        .next('.nav-second-level')
        .slideToggle('fast');
    });
  },

  toggleLimparOcorrencia: function () {
    $('#btnLimparOcorrencia').on('click', function (e) {
      e.preventDefault();
      APP.controller.SacEai.limparOcorrencia();
    });
  },

  limparOcorrencia: function () {
    $('#titulo').val('');
    $('#assunto').val('');
    $('#contato').val('');
    $('#tipo').val('');
    $('#status').val('');
    $('#nome').val('');
    $('#cpf').val('');
    $('#email').val('');
    $('#telefone').val('');
    $('#celular').val('');
    $('#descricao').val('');
  },

  getusuarioOcorrencia: function () {
    $('#cpfBusca').mask('000.000.000-00');
    var me = this;
    $('#btnBuscarOcorrencia').on('click', function (e) {
      e.preventDefault();

      const pathsApi = APP.controller.Api.pathApi();
      const token = sessionStorage.getItem('tokenAdmin');
      const cpf = $('#cpfBusca').val().replace(/[^\d]+/g, '');

      $.ajax({
        type: 'GET',
        url: pathsApi.getusuario + "/" + cpf,
        dataType: 'json',
        headers: { Authorization: `Bearer ${token}` },
        contentType: 'application/json',

        beforeSend: function () {
          APP.component.Loading.show();
        },

        success: function (response) {
          $("#found").val("1")
          me.setInfoUsuario(response.user, response.shop, response.network);
        },

        error: function () {
          APP.controller.Main.getUser();
          $("#found").val("0")
          APP.component.Alerta.alerta(
            'Falha',
            'CPF não encontrado!'
          );
          $("#BtnAcessoEspelhoCadastro").hide();
        },

        complete: function () {
          APP.component.Loading.hide();
        }
      });
    });
  },


  setInfoUsuario: function (usuario, loja, rede) {

    $('#nome').val(usuario.name);
    $('#email').val(usuario.email);
    $('#telefone').val(usuario.phone);
    $('#celular').val(usuario.cellPhone);
    $('#loja').val(loja.name);
    $('#rede').val(rede.name);

  },

  salvarOcorrenciaEai: function () {
    $('#cpf, #txtCPF').mask('000.000.000-00');
    $('#telefone').mask('(00) 0000-0000');
    $('#celular').mask('(00) 0000-00000');

    var me = this;

    $('#btnSalvarOcorrencia').click(function (e) {
      e.preventDefault();
      me.setSalvarOcorrenciaEai(1);
    });
    $('#btnFinalizarOcorrencia').click(function (e) {
      e.preventDefault();
      me.setSalvarOcorrenciaEai(3);
    });
  },

  preencheCodigo: function (response) {
    $('#codigo').val(response.dsCodigo);
  },

  setSalvarOcorrenciaEai: function (statusOcorrencia) {
    const pathsApi = APP.controller.Api.pathApi();

    const formData = new FormData();
    formData.append('Critical', $("#Critical").is(":checked"));
    formData.append('OccurrenceContactTypeId', $('#idMeioContatoCadastro option:selected').val());
    formData.append('OccurrenceSubjectId', $('#idAssuntoCadastro option:selected').val());
    formData.append('OccurrenceStatusId', statusOcorrencia);
    formData.append('Cpf', $('#cpfBusca').val().replace(/[^\d]+/g, ''));
    formData.append('BrazilCTCall', $('#brasilct').val());
    formData.append('OccurrenceMessage.Message', $('#cadastroDescricao').val());
    formData.append('OccurrenceMessage.OccurrenceMessageTypeId', 1);
    formData.append('OccurrenceMessage.Internal', true);
    formData.append('OccurrenceMessage.Catalog', true);


    if (!!$('#cadastroAnexoMensagem')[0].files[0]) {
      formData.append('formFile', $('#cadastroAnexoMensagem')[0].files[0]);
    }

    if ($("#found").val() == "0") {
      APP.component.Alerta.alerta('Erro!', 'Procure um participante válido');
      return false;
    }else if ($('#cadastroDescricao').val() == ""){
      APP.component.Alerta.alerta('Erro!', 'Digita a mensagem !');
      return false;
    } else {
      const token = sessionStorage.getItem('tokenAdmin');
      $.ajax({
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        cache: false,
        url: pathsApi.salvarOcorrencia,
        headers: { Authorization: `Bearer ${token}` },
        beforeSend: function () {
          APP.controller.Main.getUser();
          APP.component.Loading.show();
        },


        success: function (response) {

          window.location.href = '/SACEAI/listarOcorrencia';
          APP.component.Alerta.alerta('Sucesso!', 'Ocorrência cadastrada!');

        },

        error: function (response) {
          APP.controller.Main.getUser();
          APP.component.Alerta.alerta('Erro!', 'Ocorreu um erro ao tentar cadastrar uma ocorrência!');
        },

        complete: function () {
          APP.component.Loading.hide();
        }
      });
    }
  },

  listaOcorrencia: function () {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    const createdAt = $('#createdAt').val()
      ? moment($('#createdAt').val(), "DD/MM/YYYY").format('YYYY-MM-DD')
      : '';
    const closedAt = $('#closedAt').val()
      ? moment($('#closedAt').val(), "DD/MM/YYYY").format('YYYY-MM-DD')
      : '';

    const codigo = $('#code').val();
    const subject = $('#subject option:selected').val();
    const cpf = $('#cpf')
      .val()
      .replace('.', '')
      .replace('.', '')
      .replace('-', '');
    const name = $('#name').val();

    const data = {
      code: codigo,
      cpf: cpf,
      name: name,
      createdAt: createdAt,
      closedAt: closedAt,
      subject: parseInt(subject),
    };

    $.ajax({
      type: 'POST',
      url: pathsApi.listarOcorrenciaEAI,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      data: JSON.stringify(data),
      cache: false,

      beforeSend: function () {

        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.SacEai.mountTableOcorrencia(response);
        //APP.controller.SacEai.exportar(response);
        // $('#btnTodosOcorrencia').attr('disabled', true);
      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },
  loadDetalheOcorrencia: function (id) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: `${pathsApi.listarOcorrenciaEaiPorId}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.controller.Main.getUser();
        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.SacEai.montaDetalheOcorrencia(response);
      },

      error: function (response) {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
        $('.detalhesOcorrencia').fadeIn();
        $('.listasOcorrencia').hide();
      }
    });
  },
  loadUserStatus: function (id) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: `${pathsApi.getusuarioStatusLog}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {

        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.SacEai.montaUserStatus(response);
      },

      error: function (response) {
        APP.controller.Main.getUser();


        $('.tbody-user-status').append(`
          <tr>
            <td>${$("#dataCadastro").val()}</td>
            <td>Sem cadastro</td>
            <td>PRE CADASTRO</td>
            <td>Primeiro cadastro</td>
          }
          </tr>
        `);
      },

      complete: function () {
        APP.component.Loading.hide();
        $('.detalhesOcorrencia').fadeIn();
        $('.listasOcorrencia').hide();
      }
    });
  },
  detalheOcorrencia: function (idParam) {

    const id =
      $('#idOcorrenciaEai').val() == '1'
        ? idParam
        : $('#idOcorrenciaEai').val();

    window.open(`${location.href}?detalhe=${id}`, '_blank');

  },
  montaUserStatus: function (response) {

    if (response.length > 0) {

      response.map(item => {
        $('.tbody-user-status').append(`
          <tr>
            <td>${moment(item.createdAt).format('L')}</td>
            <td>${item.userStatusFrom.description}</td>
            <td>${item.userStatusTo.description}</td>
            <td>${item.description}</td>
          }
          </tr>
        `);
      });
    }

    APP.controller.SacEai.backNavigation();
    //APP.controller.SacEai.salvarDetalheOcorrencia(response);
  },
  montaDetalheOcorrencia: function (response) {

    if (response.length > 0) {

      $("#idOcorrenciaEai").val(response[0].occurrence.id);

      $('#tableDetalheOcorrenciaEai').html('');

      $('#tableDetalheOcorrenciaEai').append(`
        <thead>
          <tr>
            <th>Data</th>
            <th>Descrição</th>
            <th>Usuário</th>
            <th>Arquivo</th>
          </tr>
        </thead>
        <tbody class="tbody-detalhe-ocorrencia">
          
        </tbody>
      `);

      response.map(item => {
        $('.tbody-detalhe-ocorrencia').append(`
          <tr>
            <td>${moment(item.createdAt).format('L')}</td>
            <td>${item.message}</td>
            <td>${item.user.name}</td>
            ${
          item.file
            ? '<td><a target="_blank" href="' +
            APP.controller.Api.pathOcorrencias() +
            item.file +
            '">Visualizar</a> </td>'
            : '<td></td>'
          }
          </tr>
        `);
      });
    } else {
      $('#tableDetalheOcorrenciaEai').html('');
      $('#tableDetalheOcorrenciaEai').append(
        '<p style="text-align: center">Sem histórico de mensagens</p>'
      );
    }

    APP.controller.SacEai.backNavigation();
    //APP.controller.SacEai.salvarDetalheOcorrencia(response);
  },

  backNavigation: function () {
    $('#btnVoltarListaOcorrencia').click(function (e) {
      e.preventDefault(e);
      $('.detalhesOcorrencia').hide();
      $('.listasOcorrencia').fadeIn();
    });
  },

  exportar: data => {
    const nomeRelatorio = `Relatorio_Ocorrencias_${moment()
      .local()
      .format('DDMMYYYYHHmmss')}`;

    const dados = APP.controller.SacEai.getDados(data);
    APP.controller.SacEai.setupExcelExport(dados, nomeRelatorio);
  },

  getDados: data => {
    return data.map(ocorrencia => {
      return [
        { text: ocorrencia.dsCodigo },
        { text: APP.controller.SacEai.getTitulo(ocorrencia) },
        { text: APP.controller.SacEai.getAssunto(ocorrencia) },
        { text: APP.controller.SacEai.getContato(ocorrencia) },
        { text: APP.controller.SacEai.getTipo(ocorrencia) },
        { text: APP.controller.SacEai.getStatus(ocorrencia) },
        { text: ocorrencia.dsCPF },
        { text: ocorrencia.dsNome },
        { text: ocorrencia.dsDescricao },
        { text: moment(ocorrencia.dtCadastro).format('L') }
      ];
    });
  },

  setupExcelExport: (data, fileName) => {
    const tabularData = [
      {
        sheetName: 'Relatorio',
        data: [
          [
            { text: 'Código' },
            { text: 'Título' },
            { text: 'Assunto' },
            { text: 'Meio de Contato' },
            { text: 'Tipo' },
            { text: 'Status' },
            { text: 'CPF' },
            { text: 'Nome' },
            { text: 'Descrição' },
            { text: 'Cadastro' }
          ],
          ...data
        ]
      }
    ];

    const XLSbutton = document.getElementById('btnExcel');

    XLSbutton.addEventListener('click', function (e) {
      Jhxlsx.export(tabularData, { fileName: fileName, maxCellWidth: 50 });
    });
  },

  mountTableOcorrencia: function (response) {
    const tableOcorrencia = $('#tabela-ocorrenciaeai');

    tableOcorrencia.html('');

    tableOcorrencia.append(`
      <thead>
        <tr>
          <th><b>Status</b></th>
          <th><b>Última Interação</b></th>
          <th><b>CPF</b></th>
          <th><b>Nome</b></th>
          <th><b>Assunto</b></th>
          <th><b>Meio Contato</b></th>
          <th><b>Abertura</b></th>
          <th><b>Fechamento</b></th>
          <th><b>Direcionado Catalogo</b></th>
          <th><b>Retornado Catalogo</b></th>
          <th></th>
        </tr>
      </thead>
      <tbody class="tbody-ocorrenciaeai">
      </tbody>
    `);

    response.map(ocorrencia => {
      $('.tbody-ocorrenciaeai').append(`      
      
      <tr>
      <th data-status="${ocorrencia.occurrenceStatus.id}">
      ${ocorrencia.occurrenceStatus.description}
    </th>
    <th data-status="${ocorrencia.lastIteration}">
      ${ocorrencia.lastIteration}
    </th>
    <th data-cpf="${ocorrencia.user.cpf}">${ocorrencia.user.cpf}</th>
    <th data-name="${ocorrencia.user.name}">${ocorrencia.user.name}</th>
        <th data-tipo="${
        ocorrencia.occurrenceSubject !== null
          ? ocorrencia.occurrenceSubject.id
          : ''
        }">
          ${
        ocorrencia.occurrenceSubject !== null
          ? ocorrencia.occurrenceSubject.description
          : ''
        }
        </th>
        <th data-contact-type="${ocorrencia.occurrenceContactType.id}">${
        ocorrencia.occurrenceContactType.description
        }</th>
        <th data-hora="${moment(ocorrencia.createdAt).format('DD/MM/YYYY HH:mm')}">${moment(
          ocorrencia.createdAt
        ).format('DD/MM/YYYY HH:mm')}</th>
      <th data-hora="${moment(ocorrencia.closedAt).format('DD/MM/YYYY HH:mm')}">${
        ocorrencia.occurrenceStatus.id === 3
          ? (ocorrencia.closedAt != '0001-01-01T00:00:00' ? moment(ocorrencia.closedAt).format('DD/MM/YYYY HH:mm') : '')
          : ''
        }</th>
        <th >${
        ocorrencia.redirectedAt !== null
          ? moment(ocorrencia.redirectedAt).format('DD/MM/YYYY HH:mm')
          : ''
        }</th>
          <th >${
        ocorrencia.returnedAt !== null
          ? moment(ocorrencia.returnedAt).format('DD/MM/YYYY HH:mm')
          : ''
        }</th>
        <th><a onclick="{ APP.controller.SacEai.detalheOcorrencia('${ocorrencia.id}')}" hre="#" class="ver-detalhes-eai" >Detalhes</a></th>
      </tr>
      
      
      `);
    });

    APP.controller.SacEai.setDataTable(true);
  },

  mountTableOcorrenciaFiltrada: function (response) {
    APP.controller.SacEai.setDataTable(true);
  },

  setDataTable: function (option) {
    $('#tabela-ocorrenciaeai').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

  getTitulo: ({ idTitulo }) => {
    return idTitulo == 1
      ? 'Cadastro'
      : idTitulo == 2
        ? 'Campanha'
        : idTitulo == 3
          ? 'Campanha 2016'
          : idTitulo == 4
            ? 'Outros'
            : idTitulo == 5
              ? 'Premiação'
              : idTitulo == 6
                ? 'Resgate de Prêmios'
                : idTitulo == 7
                  ? 'Site'
                  : '';
  },

  getAssunto: ({ idAssunto }) => {
    return idAssunto == 1
      ? 'Dúvidas'
      : idAssunto == 2
        ? 'Elogios'
        : idAssunto == 3
          ? 'Outros'
          : idAssunto == 4
            ? 'Reclamação'
            : '';
  },

  getContato: ({ idContato }) => {
    return idContato == 1
      ? 'E-mail'
      : idContato == 2
        ? '0800'
        : idContato == 3
          ? 'WhatsApp'
          : '';
  },

  getTipo: ({ idTipo }) => {
    return idTipo == 1
      ? 'Consulta'
      : idTipo == 2
        ? 'Contestação'
        : idTipo == 3
          ? 'Recuperar Senha'
          : idTipo == 4
            ? 'Alteração de cadastro'
            : idTipo == 5
              ? 'Troca de item'
              : idTipo == 6
                ? 'Atraso na entrega'
                : idTipo == 7
                  ? 'Outros'
                  : '';
  },

  getStatus: ({ idStatus }) => (idStatus == 1 ? 'Em Aberto' : 'Finalizado'),

  getPrioridade: ({ idPrioridade }) => {
    return idPrioridade == 1
      ? 'Alta'
      : idPrioridade == 2
        ? 'Média'
        : idPrioridade == 3
          ? 'Baixa'
          : '';
  }
};
