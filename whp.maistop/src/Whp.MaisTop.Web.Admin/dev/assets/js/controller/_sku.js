/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Sku = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/SKUCLASSIFICACAO') !== -1) {
      this.getRedes();
      //APP.controller.Main.getUser();
      //this.getPendingClassification();
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
          $('#skuRedeSelect').append(`
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
  classificate: (productDescription, NotExisting, element) => {

   var productId = NotExisting === false ? $('#swalProduct option:selected').val() : 0;
    APP.controller.Main.getUser();
    
    Swal.fire({
      title: 'Você tem certeza?',
      text: "Você deseja classificar esse item ?",
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
          url: pathsApi.UpdateSaleFileDataSku,
          dataType: 'json',
          contentType: 'application/json',
          headers: { Authorization: `Bearer ${token}` },
          data: JSON.stringify({
            ProductDescription: productDescription,
            Product: productId,
            NotExisting: NotExisting
          }),
          cache: false,
          beforeSend: function () {
            APP.controller.Main.getUser();
            APP.component.Loading.show();
          },

          success: function (response) {
            $(`#${element}`).remove();
          },
          error: function(jqXHR, textStatus, errorThrown){
              $(`#${element}`).remove();
          },
          complete: function () {
            APP.component.Loading.hide();
          }
        });
      }
    })

  },
  getProducers: () => {


    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.GetProducers,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        response.map(result => {
          $("#swalProducer").append(`
            <option value="${result.id}" >${result.name}</option>
          `)
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
  getProducts: (currentMonth, currentYear, network) => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'POST',
      url: pathsApi.GetProductsSku,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      data: JSON.stringify({
        CategoryProductId: $('#swalCategory option:selected').val(),
        ProducerId: $('#swalProducer option:selected').val()
      }),
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        $("#btnClassification").prop("disabled", false)
        $("#swalProduct").html('');

        response.map(result => {
          $("#swalProduct").append(`
            <option value="${result.id}" >${result.sku} - ${result.name}</option>
          `)
        });
      },

      error: function () {
        APP.controller.Main.getUser();
        $("#swalProduct").html('');
        $("#btnClassification").prop("disabled", true)
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },
  getCategories: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.GetCategories,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        response.map(result => {
          $("#swalCategory").append(`
            <option value="${result.id}" >${result.name}</option>
          `)
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

    $('#tabela-sku')
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
            "createdRow": function (row, data, index) {
              
              $('td', row).parent().attr("id",data[1].replace(/\s/g, ""));
              $('td', row).eq(4).html(`<a hre="#" onclick="APP.controller.Sku.setClassification('${data[1]}','${data[2].split("/")[0]}','${data[2].split("/")[1]}','${data[4]}','${data[1].replace(/\s/g, "")}')" data-id="${data[1]}">Classificar</a> /
              <a hre="#" class="unapproved" onclick="APP.controller.Sku.classificate('${data[1]}',true,'${data[1].replace(/\s/g, "")}')" data-id="${data[1]}">Inexistente</a>`);
            },
            "processing": true,
            "serverSide": true,
            "deferRender": true,
            "scrollX": true,
            searching: false,
            "ajax": {
              "url": pathsApi.GetPendingClassification,
              "type": "POST",
              "contentType": 'application/json',
              "headers": { Authorization: `Bearer ${token}` },
               data:function ( d ) { 
                return JSON.stringify({ Start:d.start, Length: d.length,network: $("#skuRedeSelect option:selected").val() });
              },
              'error': function(jqXHR, textStatus, errorThrown){
                  APP.controller.PreProcessing.setDataTable(true)
              }
          },
            "pagingType": "numbers"
    });
  },
  setClassification: function (description,currentMonth,currentYear,network, element) {
    
      Swal.fire({
        title: description,
        type: 'info',
        html:
          `    
                <div style='margin-top:5%'>
                    <label>Marca</label>
                    <select onchange="APP.controller.Sku.getProducts(${currentMonth},${currentYear},${network})" id="swalProducer" class="form-control">
                        <option value="0" disabled selected >SELECIONE</option>
                    </select>
                </div>
                <div style='margin-top:5%'>
                    <label>Categoria</label>
                    <select onchange="APP.controller.Sku.getProducts(${currentMonth},${currentYear},${network})" id="swalCategory" class="form-control">
                        <option value="0" disabled selected >SELECIONE</option>
                    </select>
                </div>
                <div style='margin-top:5%'>
                    <label>Produto</label>
                    <select id="swalProduct" class="form-control">
                    </select>
                </div>
                <div style='margin-top:5%'>
                    <input type="button" id="btnClassification" onclick="APP.controller.Sku.classificate('${description}',false,'${element}')" disabled="true" class="btn btn-primary" value="Classificar" />
                </div>
                `,
        animation: true,
        showCloseButton: true,
        showCancelButton: false,
        showConfirmButton: false,
        onOpen: () => {
          APP.controller.Sku.getProducers();
          APP.controller.Sku.getCategories();
        }
      })
    
  }
};
