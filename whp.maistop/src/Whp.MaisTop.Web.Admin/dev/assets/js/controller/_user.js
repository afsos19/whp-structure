/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.User = {
  Object: {},
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/USUARIO/LISTAR') !== -1) {
      $('#userListCpf').mask('000.000.000-00');
      $('#tabela-usuarios').DataTable({
        language: {
          url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
        },
        "pageLength": 10
      });
      APP.controller.Main.getUser();
      this.getCargos(true);
      this.getRedes(true);
      this.getUserStatus(true);
    } else if (window.location.href.toUpperCase().indexOf('/USUARIO/EDITAR') !== -1) {
      var id = window.location.href.toUpperCase().split('=')[1];
      this.getCargos(false);
      setTimeout(() => {
        this.loadUser(id);
        this.getShop(id);
      }, 1000);

    }
  },
  ExportUsersToExcel: () => {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: pathsApi.exportUsersToExcel,
      xhrFields: {
        responseType: 'blob'
      },
      headers: { Authorization: `Bearer ${token}` },
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (response) {
        var a = document.createElement('a');
        var url = window.URL.createObjectURL(response);
        a.href = url;
        a.download = 'report_geral_usuarios.xlsx';
        document.body.append(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);
      },

      error: function (response) {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });
  },
  UpdateUser: () => {

    APP.controller.Main.Object.UserStatusId = parseInt($("#userEditStatus option:selected").val());
    APP.controller.Main.Object.OfficeId = parseInt($("#userListOffice option:selected").val());
    APP.controller.Main.Object.bithDate = moment($("#userEditBirthday").val(), "YYYY-MM-DD");
    APP.controller.Main.Object.email = $("#userEditEmail").val();
    APP.controller.Main.Object.Cep = $("#userEditCep").val();
    APP.controller.Main.Object.Phone = $("#userEditTelefone").val();
    APP.controller.Main.Object.CellPhone = $("#userEditCelular").val();
    APP.controller.Main.Object.Uf = $("#userEditUf").val();
    APP.controller.Main.Object.City = $("#userEditCity").val();
    APP.controller.Main.Object.Neighborhood = $("#userEditNeighborhood").val();
    APP.controller.Main.Object.Address = $("#userEditAddress").val();
    APP.controller.Main.Object.Number = parseInt($("#userEditNumber").val());
    APP.controller.Main.Object.Complement = $("#userEditComplement").val();
    APP.controller.Main.Object.ReferencePoint = $("#userEditPointReference").val();
    APP.controller.Main.Object.Password = "";
    delete APP.controller.Main.Object.offIn;
    APP.controller.Main.getUser();

    Swal.fire({
      title: 'Você tem certeza?',
      text: "Você deseja alterar os dados do usuario ?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, tenho certeza!',
      cancelButtonText: 'Não, cancelar!'
    }).then((result) => {
      if (result.value) {

        const pathsApi = APP.controller.Api.pathApi();
        const token = sessionStorage.getItem('tokenAdmin');

        $.ajax({
          type: 'PUT',
          data: JSON.stringify(APP.controller.Main.Object),
          url: pathsApi.updateUser,
          dataType: 'json',
          contentType: 'application/json',
          headers: { Authorization: `Bearer ${token}` },
          beforeSend: function () {
            APP.component.Loading.show();
          },
          success: function (result) {
            location.reload();
          },
          error: function (response) {
            APP.controller.Main.getUser();
            APP.component.Alerta.alerta('Erro', response.responseText);
          },
          complete: function (result) {
            APP.component.Loading.hide();
          }
        });
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
          $('#userListOffice').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#userListOffice').append(`
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
  getShop: function (id) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: `${pathsApi.getShopByUser}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        $("#userEditShop").val(response[0].name)
        $("#userEditShop").prop("disabled", true)
        $("#userEditNetwork").val(response[0].network.name)
        $("#userEditNetwork").prop("disabled", true)

      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });
  },
  loadUser: function (id) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: `${pathsApi.getusuarioById}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        APP.controller.Main.Object = response;
        $("#userEditName").val(response.name);
        $("#userEditName").prop("disabled", true)
        $("#userEditCpf").val(response.cpf);
        $('#userEditCpf').mask('000.000.000-00');
        $("#userEditCpf").prop("disabled", true)

        APP.controller.User.loadBalance(response.cpf);

        $("#userEditStatus option").map(function () {
          if (parseInt($(this).val()) === parseInt(response.userStatus.id))
            $(this).prop("selected", true);
        })

        $("#userListOffice option").map(function () {
          if (parseInt($(this).val()) === parseInt(response.office.id))
            $(this).prop("selected", true);
        })

        $("#userEditBirthday").val(moment(response.bithDate).format('L'));
        $('#userEditBirthday').mask('00/00/0000');
        $("#userEditEmail").val(response.email);
        $("#userEditPassword").val(response.password);
        $("#userEditTelefone").val(response.phone);
        $('#userEditTelefone').mask('(00) 00000-0000');
        $("#userEditCelular").val(response.cellPhone);
        $('#userEditCelular').mask('(00) 00000-0000');
        $("#userEditCep").val(response.cep);
        $("#userEditUf").val(response.uf);
        $("#userEditCity").val(response.city);
        $("#userEditNeighborhood").val(response.neighborhood);
        $("#userEditAddress").val(response.address);
        $("#userEditNumber").val(response.number);
        $("#userEditComplement").val(response.complement);
        $("#userEditPointReference").val(response.referencePoint);
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
  loadBalance: function (cpf) {
    const pathsApi = APP.controller.Api.pathApi();
    const token = sessionStorage.getItem('tokenAdmin');

    $.ajax({
      type: 'GET',
      url: `${pathsApi.GetUserBalance}/${cpf}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,

      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        $("#userEditBalance").val(response.balance);
        $("#userEditBalance").prop("disabled", true)

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
          $('#userListUserStatus').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#userListUserStatus').append(`
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
          $('#userListNetwork').append(`
          <option value="0" >TODOS</option>
        `);
        }

        response.map(item => {
          $('#userListNetwork').append(`
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
  setDataTable: function (option) {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $('#tabela-usuarios')
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
          $('td', row).eq(6).html("<a target='_blank' href='/Usuario/Editar?id=" + data[6] + "'>Editar</a> ");
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
              Cpf: $("#userListCpf").val().replace(/[^\d]+/g, ''),
              Name: $("#userListName").val(),
              Email: $("#userListEmail").val(),
              Office: $("#userListOffice option:selected").val(),
              Network: $("#userListNetwork option:selected").val(),
              UserStatus: $("#userListUserStatus option:selected").val()
            });
          },
          'error': function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status !== 404) {
              APP.controller.User.setDataTable(true)
            } else {
              $('#tabela-usuarios').DataTable().destroy();
              APP.component.Alerta.alerta("Atenção", jqXHR.responseText);
            }
          }
        },
        "pagingType": "numbers"
      });
  },

};
