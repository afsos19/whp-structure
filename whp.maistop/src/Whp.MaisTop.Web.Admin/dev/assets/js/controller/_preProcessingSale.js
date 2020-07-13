/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.PreProcessingSale = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/RELATORIOS/PREPROCESSAMENTOVENDA') !== -1) {
      APP.controller.Main.getUser();
      $("#PreProcessingSaleMonth").val(new Date().getMonth());
      $("#PreProcessingSaleYear").val(new Date().getFullYear());
    }
  },
  Approve:()=>{
    APP.controller.Main.getUser();
    Swal.fire({
      title: 'Você tem certeza?',
      text: `Você deseja aprovar os arquivos do mes ${$("#PreProcessingSaleMonth").val()} e ano ${$("#PreProcessingSaleYear").val()} ?`,
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
          url: pathsApi.ApprovePreProcessingSale,
          dataType: 'json',
          contentType: 'application/json',
          headers: { Authorization: `Bearer ${token}` },
          data: JSON.stringify({ currentMonth: $("#PreProcessingSaleMonth").val(), currentYear: $("#PreProcessingSaleYear").val() }),
          cache: false,
          beforeSend: function () {
            APP.controller.Main.getUser();
            APP.component.Loading.show();
          },

          success: function (response) {
            $(".tbody-PreProcessingSale").html('');
            APP.component.Alerta.alerta("Atenção", `Os arquivos do mes ${$("#PreProcessingSaleMonth").val()} e ano ${$("#PreProcessingSaleYear").val()} foram agendados para o processamento de vendas`);  
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
  setDataTable: function (option) {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'POST',
      url: pathsApi.GetPreProcessingSale,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      data: JSON.stringify({ currentMonth: $("#PreProcessingSaleMonth").val(), currentYear: $("#PreProcessingSaleYear").val() }),
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

         response.map(ret =>{
           $(".tbody-PreProcessingSale").append(`
           <tr>
            <th>${ret.network}</th>
            <th>${ numeral(ret.salesman).format('0,0.00') }</th>
            <th>${numeral(ret.regionManager).format('0,0.00') }</th>
            <th>${numeral(ret.manager).format('0,0.00')}</th>
            <th>${numeral(ret.total).format('0,0.00') }</th>
           </tr>
           `)
         })

         $('#tabela-PreProcessingSale').DataTable({
          destroy:true,
          language: {
            url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
          }
        });
      },

      error: function (response) {
        APP.controller.Main.getUser();
        APP.component.Alerta.alerta("Atenção", response.responseText);  
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },

};
