/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.MirrorAccess = {
  Object: {},
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/ACESSOESPELHO') !== -1) {
      $('#acessoEspelhoCpf').mask('000.000.000-00');
      $('#acessoEspelhoCnpj').mask('00.000.000/0000-00');
      $('#tabela-acesso-espelho').DataTable({
        language: {
          url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
        },
        "pageLength": 10
      });
      APP.controller.Main.getUser();
      this.getCargos(true);
      this.getRedes(true);
      this.getUserStatus(true);
    }
  },
  getUserStatus: function (allOption) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: pathsApi.UserStatus,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.controller.Main.getUser();
        APP.component.Loading.show();
      },

      success: function (response) {
        if (allOption) {
          $('#acessoEspelhoUserStatus').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#acessoEspelhoUserStatus').append(`
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
  getRedes: function (allOption) {
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
        APP.controller.Main.getUser();
        APP.component.Loading.show();
      },

      success: function (response) {
        if (allOption) {
          $('#acessoEspelhoNetwork').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#acessoEspelhoNetwork').append(`
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
  getCargos: function (allOption) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: pathsApi.getCargo,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.controller.Main.getUser();
        APP.component.Loading.show();
      },

      success: function (response) {
        if (allOption) {
          $('#acessoEspelhoOffice').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#acessoEspelhoOffice').append(`
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
  DoMirrorAccess: function (id) {

      const pathsApi = APP.controller.Api.pathApi();
      const token = sessionStorage.getItem('tokenAdmin');
      $.ajax({
        type: 'GET',
        url: `${pathsApi.MirrorAccessId}/${parseInt(id)}`,
        dataType: 'json',
        headers: { Authorization: `Bearer ${token}` },
        contentType: 'application/json',

        beforeSend: function () {
          APP.controller.Main.getUser();
          APP.component.Loading.show();
        },

        success: function (response) {
          localStorage.setItem('token', response.token);
          window.open('https://www.programamaistop.com.br?c=' + btoa(response.token), '_blank');

        },

        error: function (response) {
          APP.controller.Main.getUser();
          APP.component.Alerta.alerta('Erro', response.responseText);
        },

        complete: function () {
          APP.component.Loading.hide();
        }
      });
    
  },
  setDataTable: function (option) {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $('#tabela-acesso-espelho')
      .on('preXhr.dt', function (e, settings, data) {
        APP.controller.Main.getUser();
      })
      .DataTable({
        language: {
          url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
        },
        "pageLength": 10,
        "aaSorting": [],
        stateSave: true,
        destroy: true,
        dom: 'lfBrtip',
        buttons: [
          { extend: 'print', text: '<i class="fa fa-print"></i>&nbsp;Imprimir', className: 'BtnPrint', ToolTip: 'Imprimir' },
          { extend: 'pdf', text: '<i class="fa fa-file-pdf-o"></i>&nbsp;PDF', className: 'BtnPdf' },
          { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i>&nbsp;Excel', className: 'BtnExcel' }
        ],
        "processing": true,
        "serverSide": true,
        "deferRender": true,
        "scrollX": true,
        "createdRow": function (row, data, index) {
          $('td', row).eq(6).html("<a onclick='APP.controller.MirrorAccess.DoMirrorAccess("+data[6]+")'>Acessar</a> ");
        },
        searching: false,
        "ajax": {
          "url": pathsApi.getUserList,
          "type": "POST",
          "contentType": 'application/json',
          "headers": { Authorization: `Bearer ${token}` },
          data: function (d) {
            return JSON.stringify({
              Start: d.start,
              Length: d.length,
              Cpf: $("#acessoEspelhoCpf").val().replace(/[^\d]+/g, ''),
              Cnpj: $("#acessoEspelhoCnpj").val().replace(/[^\d]+/g, ''),
              Name: $("#acessoEspelhoName").val(),
              Email: $("#acessoEspelhoEmail").val(),
              Office: $("#acessoEspelhoOffice option:selected").val(),
              Network: $("#acessoEspelhoNetwork option:selected").val(),
              UserStatus: $("#acessoEspelhoUserStatus option:selected").val(),
            });
          },
          'error': function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status !== 404) {
              APP.controller.User.setDataTable(true)
            } else {
              $('#tabela-acesso-espelho').DataTable().destroy();
              APP.component.Alerta.alerta("Atenção", jqXHR.responseText);
            }
          }
        },
        "pagingType": "numbers"
      });
  },

};
