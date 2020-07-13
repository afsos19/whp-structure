/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Product = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/PRODUTO/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      this.getProducts();
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTO/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      this.getProducers(false);
      this.getCategories(false);
    } else if (window.location.href.toUpperCase().indexOf('/PRODUTO/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      this.getProducers(true);
      this.getCategories(true);
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.Product.setProduct(id);
    }
  },
  setProduct:(id) =>{
    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: `${pathsApi.getProductById}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

          if(response.photo)
              $("#currentPoho").prop("src",APP.controller.Api.pathProdutos()+response.photo);

          $("#editHdId").val(response.id);
          $("#editTxtName").val(response.name);
          $("#editTxtEan").val(response.ean);
          $("#editTxtSku").val(response.sku);
          $("#editCmbCategory option").each(function()
          {
              if(parseInt($(this).val()) === parseInt(response.categoryProduct.id))
                $(this).prop("selected",true);
          });
          $("#editCmbProducer option").each(function()
          {
              if(parseInt($(this).val()) === parseInt(response.producer.id))
                $(this).prop("selected",true);
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
  updateProduct: () => {

    if ($('#editTxtSku').val().length == 0 || $('#editTxtName').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja editar esse produto ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, tenho certeza!',
        cancelButtonText: 'Não, cancelar!'
      }).then((result) => {
        if (result.value) {

          const formData = new FormData();
          formData.append('Id', $('#editHdId').val());
          formData.append('ProducerId', $('#editCmbProducer option:selected').val());
          formData.append('CategoryProductId', $('#editCmbCategory option:selected').val());
          formData.append('Sku', $('#editTxtSku').val());
          formData.append('Ean', $('#editTxtEan').val());
          formData.append('Name', $('#editTxtName').val());

          if (!!$('#editFilePhoto')[0].files[0]) {
            formData.append('formFile', $('#editFilePhoto')[0].files[0]);
          }

          const pathsApi = APP.controller.Api.pathApi();
          const token = sessionStorage.getItem('tokenAdmin');

          $.ajax({
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            cache: false,
            url: pathsApi.updateProduct,
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.controller.Main.getUser();
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Produto/Listar";
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
      })
    }

  },
  saveProduct: () => {

    if ($('#txtSku').val().length == 0 || $('#txtName').val().length == 0) {
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

          const formData = new FormData();
          formData.append('ProducerId', $('#cmbProducer option:selected').val());
          formData.append('CategoryProductId', $('#cmbCategory option:selected').val());
          formData.append('Sku', $('#txtSku').val());
          formData.append('Ean', $('#txtEan').val());
          formData.append('Name', $('#txtName').val());

          if (!!$('#filePhoto')[0].files[0]) {
            formData.append('formFile', $('#filePhoto')[0].files[0]);
          }

          const pathsApi = APP.controller.Api.pathApi();
          const token = sessionStorage.getItem('tokenAdmin');

          $.ajax({
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            cache: false,
            url: pathsApi.saveProduct,
            headers: { Authorization: `Bearer ${token}` },
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Produto/Listar";
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
      })
    }

  },
  getProducers: (edit) => {

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
          $(`${(edit === true ? "#editCmbProducer" : "#cmbProducer")}`).append(`
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
      type: 'GET',
      url: pathsApi.GetProducts,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        APP.controller.Product.mountTableProduct(response);
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
  getCategories: (edit) => {

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
          $(`${(edit === true ? "#editCmbCategory" : "#cmbCategory")}`).append(`
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
  mountTableProduct: function (response) {
    $('.tbody-product').html('');
    if (response.length > 0) {
      response.map(product => {
        $('.tbody-product').append(`      
        
        <tr>
        <th data-status="${product.id}">
          ${product.id}
        </th>
        <th data-descricao="${product.producer.name}">
          ${product.producer.name}
        </th>
        <th>${product.categoryProduct.name}</th>
        <th>${product.sku}</th>
        <th>${product.ean}</th>
        <th>${product.name}</th>
        <th>
          <a href="/Produto/Editar?id=${product.id}" class="editar" >Editar</a>
        </th>
      </tr>
        `);
      });
    }

    APP.controller.Product.setDataTable(true);

  },
  setDataTable: function (option) {
    $('#tabela-produto').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
