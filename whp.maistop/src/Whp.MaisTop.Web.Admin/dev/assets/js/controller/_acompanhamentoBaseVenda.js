/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.AcompanhamentoBaseVenda = {
  init() {
    this.setup();
    $('#tabela-AcompanhamentoBaseVenda').hide();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/RELATORIOS/ACOMPANHAMENTOBASEVENDA') !== -1) {
      APP.controller.Main.getUser();
      $("#AcompanhamentoBaseVendaMonth").val(new Date().getMonth());
      $("#AcompanhamentoBaseVendaYear").val(new Date().getFullYear());
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
        response.map(item => {
          $('#AcompanhamentoBaseVendaRede').append(`
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

    $('#tabela-AcompanhamentoBaseVenda').show();

    $('#tabela-AcompanhamentoBaseVenda')
    .on('preXhr.dt', function ( e, settings, data ) {
      APP.controller.Main.getUser();
    })
    .DataTable({
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      },
      "iDisplayLength": 100,
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
              $('td', row).eq(3).html("<a target='_blank' href='"+data[3]+"'>Download</a> ");
            },
            searching: false,
            "ajax": {
              "url": pathsApi.BaseTrackingSale,
              "type": "POST",
              "contentType": 'application/json',
              "headers": { Authorization: `Bearer ${token}` },
               data:function ( d ) { 
                return JSON.stringify({ Start:d.start, Length: d.length,network: $("#AcompanhamentoBaseVendaRede option:selected").val(), currentMonth: $("#AcompanhamentoBaseVendaMonth").val(), currentYear: $("#AcompanhamentoBaseVendaYear").val(), FileStatusId: $("#AcompanhamentoBaseVendaStatus option:selected").val() });
              },
              'error': function(jqXHR, textStatus, errorThrown){
                if(jqXHR.status !== 404)
                  APP.controller.AcompanhamentoBaseVenda.setDataTable(true)
                else{
                  $('#tabela-AcompanhamentoBaseVenda').DataTable().destroy();
                  $('#tabela-AcompanhamentoBaseVenda').hide();
                  APP.component.Alerta.alerta("Atenção", jqXHR.responseText);   
                }
              }
          },
            "pagingType": "numbers"
    });
  },

};
