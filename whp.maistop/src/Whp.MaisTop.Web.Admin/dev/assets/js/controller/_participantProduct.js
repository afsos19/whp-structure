/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.ParticipantProduct = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/PRODUTOPARTICIPANTE/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      $("#ParticipantProductMonth").val(new Date().getMonth());
      $("#ParticipantProductYear").val(new Date().getFullYear());
      APP.controller.ParticipantProduct.setDataTable(true);
      this.getRedes();
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTOPARTICIPANTE/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      this.getRedes()
      this.getProducers();
      this.getCategories();
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTOPARTICIPANTE/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      this.getRedes()
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.ParticipantProduct.setProduct(id);
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
          $('#networkParticipantProduct').append(`
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

  deleteParticipantProduct: (id) => {

  
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja deletar esse produto ?",
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
            type: 'DELETE',
            url: `${pathsApi.DeleteParticipantProduct}/${id}`,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/ProdutoParticipante/Listar";
            },
            error: function (result) {
              location.href = "/ProdutoParticipante/Listar";
            },
            complete: function (result) {
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
  getProducts: () => {

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
        response.map(result => {
          $("#swalProduct").append(`
            <option value="${result.id}" >${result.sku} - ${result.name}</option>
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
  saveParticipantProduct: () => {

   
    if ($('#cadastraParticipantProductPontuacao').val().length == 0 || $('#cadastraParticipantProductYear').val().length == 0 || $('#cadastraParticipantProductMonth').val().length == 0 || $('#swalProduct option:selected').val() < 1 || $('#swalProduct option:selected').val() === undefined) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja cadastrar esse produto ?",
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
            type: 'POST',
            data: JSON.stringify({
              NetworkId: $("#networkParticipantProduct option:selected").val(),
              CurrentMonth: $("#cadastraParticipantProductMonth").val(),
              CurrentYear: $("#cadastraParticipantProductYear").val(),
              Punctuation: $("#cadastraParticipantProductPontuacao").val(),
              GroupProductId: $("#cmbProdutoGrupo option:selected").val(),
              ProductId: $("#swalProduct option:selected").val()
            }),
            url: pathsApi.saveParticipantProduct,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/ProdutoParticipante/Listar";
            },
            error: function (result) {
              location.href = "/ProdutoParticipante/Listar";
            },
            complete: function (result) {
              APP.component.Loading.hide();
            }
          });
        }
      })
    }

  },

  getParticipantProducts: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'POST',
      data: JSON.stringify({ network: $("#networkParticipantProduct option:selected").val(), currentMonth: $("#ParticipantProductMonth").val(), currentYear: $("#ParticipantProductYear").val() }),
      url: pathsApi.GetParticipantProducts,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.ParticipantProduct.mountTableParticipantProduct(response);
      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },
  mountTableParticipantProduct: function (response) {

    $('.tbody-ParticipantProduct').html('');
    if (response.length > 0) {
      response.map(product => {
        
        $('.tbody-ParticipantProduct').append(`     
        <tr>
        <th data-status="${product.id}">
          ${product.id}
        </th>
        <th data-descricao="${product.product.name}">
          ${product.product.name}
        </th>
        <th data-descricao="${product.network.name}">
          ${product.network.name}
        </th>
        <th>${product.punctuation}</th>
        <th>${product.currentMonth}</th>
        <th>${product.currentYear}</th>
        <th>
          <a onclick="{APP.controller.ParticipantProduct.deleteParticipantProduct(${product.id})}" >Deletar</a>
        </th>
      </tr>
        `);
      });
    }

  },
  setDataTable: function (option) {
    $('#tabela-ParticipantProduct').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
