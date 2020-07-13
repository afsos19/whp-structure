/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.FocusProduct = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/PRODUTOSUPERTOP/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      $("#superTopMonth").val(new Date().getMonth());
      $("#superTopYear").val(new Date().getFullYear());
      APP.controller.FocusProduct.setDataTable(true);
      this.getRedes();
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTOSUPERTOP/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      this.getRedes()
      this.getProducers();
      this.getCategories();
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTOSUPERTOP/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      this.getRedes()
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.FocusProduct.setProduct(id);
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
          $('#networkSuperTop').append(`
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

  deleteFocusProduct: (id) => {

  
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
            url: `${pathsApi.DeleteFocusProduct}/${id}`,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/ProdutoSuperTop/Listar";
            },
            error: function (result) {
              location.href = "/ProdutoSuperTop/Listar";
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
  saveFocusProduct: () => {

   
    if ($('#cadastraSuperTopPontuacao').val().length == 0 || $('#cadastraSuperTopYear').val().length == 0 || $('#cadastraSuperTopMonth').val().length == 0 || $('#swalProduct option:selected').val() < 1 || $('#swalProduct option:selected').val() === undefined) {
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
              NetworkId: $("#networkSuperTop option:selected").val(),
              CurrentMonth: $("#cadastraSuperTopMonth").val(),
              CurrentYear: $("#cadastraSuperTopYear").val(),
              Punctuation: $("#cadastraSuperTopPontuacao").val(),
              GroupProductId: $("#cmbProdutoGrupo option:selected").val(),
              ProductId: $("#swalProduct option:selected").val()
            }),
            url: pathsApi.saveFocusProduct,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/ProdutoSuperTop/Listar";
            },
            error: function (result) {
              location.href = "/ProdutoSuperTop/Listar";
            },
            complete: function (result) {
              APP.component.Loading.hide();
            }
          });
        }
      })
    }

  },

  getFocusProducts: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'POST',
      data: JSON.stringify({ network: $("#networkSuperTop option:selected").val(), currentMonth: $("#superTopMonth").val(), currentYear: $("#superTopYear").val() }),
      url: pathsApi.GetFocusProducts,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.FocusProduct.mountTableFocusProduct(response);
      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });

  },
  mountTableFocusProduct: function (response) {
    $('.tbody-focusProduct').html('');
    if (response.length > 0) {
      response.map(product => {
        $('.tbody-focusProduct').append(`      
        
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
        <th>${product.groupProduct.name}</th>
        <th>${product.punctuation}</th>
        <th>${product.currentMonth}</th>
        <th>${product.currentYear}</th>
        <th>
          <a onclick="{APP.controller.FocusProduct.deleteFocusProduct(${product.id})}" >Deletar</a>
        </th>
      </tr>
        `);
      });
    }

    APP.controller.Product.setDataTable(true);

  },
  setDataTable: function (option) {
    $('#tabela-focusProduct').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
