/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.PreProcessing = {
  init() {
    this.setup();
    $('#tabela-preProcessing').hide();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/RELATORIOS/PREPROCESSAMENTO') !== -1) {
      APP.controller.Main.getUser();
      $("#preProcessingMonth").val(new Date().getMonth());
      $("#preProcessingYear").val(new Date().getFullYear());
      this.getRedes();
    }
  },
  Approve:()=>{
    Swal.fire({
      title: 'Você tem certeza?',
      text: `Você deseja aprovar os arquivos do mes ${$("#preProcessingMonth").val()} e ano ${$("#preProcessingYear").val()} ?`,
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, tenho certeza!',
      cancelButtonText: 'Não, cancelar!'
    }).then((result) => {
      if (result.value) {
        const token = sessionStorage.getItem('tokenAdmin');
        const pathsApi = APP.controller.Api.pathApi();
        $.ajax({
          type: 'POST',
          url: pathsApi.ApprovePreProcessing,
          dataType: 'json',
          contentType: 'application/json',
          headers: { Authorization: `Bearer ${token}` },
          data: JSON.stringify({ network: $("#preProcessingRede option:selected").val(), currentMonth: $("#preProcessingMonth").val(), currentYear: $("#preProcessingYear").val() }),
          cache: false,
          beforeSend: function () {
            APP.controller.Main.getUser();
            APP.component.Loading.show();
          },

          success: function (response) {
            APP.component.Alerta.alerta("Atenção", `Os arquivos do mes ${$("#preProcessingMonth").val()} e ano ${$("#preProcessingYear").val()} foram agendados para o processamento de vendas`);  
          },

          error: function (response) {
            APP.controller.Main.getUser();
            APP.component.Alerta.alerta("Atenção", response.responseText);  
          },

          complete: function () {
            APP.component.Loading.hide();
          }
        });
      }
    })
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
          $('#preProcessingRede').append(`
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

    $('#tabela-preProcessing').show();

    $('#tabela-preProcessing')
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
            searching: false,
            "ajax": {
              "url": pathsApi.GetPreProcessingSku,
              "type": "POST",
              "contentType": 'application/json',
              "headers": { Authorization: `Bearer ${token}` },
               data:function ( d ) { 
                return JSON.stringify({ Start:d.start, Length: d.length,network: $("#preProcessingRede option:selected").val(), currentMonth: $("#preProcessingMonth").val(), currentYear: $("#preProcessingYear").val() });
              },
              'error': function(jqXHR, textStatus, errorThrown){
                if(jqXHR.status !== 404)
                  APP.controller.PreProcessing.setDataTable(true)
                else{
                  $('#tabela-preProcessing').DataTable().destroy();
                  $('#tabela-preProcessing').hide();
                  APP.component.Alerta.alerta("Atenção", jqXHR.responseText);   
                }
              }
          },
            "pagingType": "numbers"
    });
  },

};
