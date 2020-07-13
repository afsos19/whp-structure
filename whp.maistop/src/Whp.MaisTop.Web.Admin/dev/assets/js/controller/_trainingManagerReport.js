/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.TrainingManagerReport = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/TREINAMENTOGERENTE') !== -1) {
      $('#trainingManagerReportCpf').mask('000.000.000-00');
      $('#trainingManagerReportCnpj').mask('00.000.000/0000-00');
      APP.controller.Main.getUser();
      this.getRedes();

    }
  },

  getRedes: function () {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: pathsApi.getRedes,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {

        APP.component.Loading.show();
      },

      success: function (response) {
        $('#trainingManagerReportNetwork').append(`
          <option value="0" >Todos</option>
        `);
        response.map(item => {
          $('#trainingManagerReportNetwork').append(`
             <option value="${item.id}" >${item.name}</option>
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

  toogleLinks: function () {
    $('.toggle-link').click(function (e) {
      e.preventDefault();
      $(this)
        .next('.nav-second-level')
        .slideToggle('fast');
    });
  },


  listaReport: function () {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    const cpf = $('#trainingManagerReportCpf')
      .val()
      .replace('.', '')
      .replace('.', '')
      .replace('-', '');

      const cnpj = $('#trainingManagerReportCnpj')
      .val()
      .replace('.', '')
      .replace('.', '')
      .replace('/', '')
      .replace('-', '');

      var month = $("#trainingManagerReportMonth").val();
    const data = {
      Network: $("#trainingManagerReportNetwork").val(),
      Month: parseInt(month),
      Cpf: cpf,
      Cnpj: cnpj,
    };

    $.ajax({
      type: 'POST',
      url: pathsApi.GetTrainingManagersReport,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      data: JSON.stringify(data),
      cache: false,

      beforeSend: function () {

        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.TrainingManagerReport.mountTableTrainingManager(response);
      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },


  mountTableTrainingManager: function (response) {
    const tableOcorrencia = $('#tabela-treinamento-gerente');

    tableOcorrencia.html('');

    tableOcorrencia.append(`
      <thead>
        <tr>
          <th><b>Cnpj</b></th>
          <th><b>Rede</b></th>
          <th><b>Nome</b></th>
          <th><b>Total Vendedores</b></th>
          <th><b>Total Completaram Treinamento</b></th>
          <th><b>Percentual</b></th>
        </tr>
      </thead>
      <tbody class="tbody-treinamento-gerente">
      </tbody>
    `);

    response.map(report => {
      $('.tbody-treinamento-gerente').append(`      
      
      <tr>
      <th >${report.cnpj}</th>
      <th >${report.network}</th>
      <th >${report.name}</th>
      <th >${report.totalSalesman}</th>
      <th >${report.trainingCompleted}</th>
      <th >${report.porcentageCompleted}</th>
      </tr>
      `);
    });

    APP.controller.TrainingManagerReport.setDataTable(true);
  },


  setDataTable: function (option) {
    $('#tabela-treinamento-gerente').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
