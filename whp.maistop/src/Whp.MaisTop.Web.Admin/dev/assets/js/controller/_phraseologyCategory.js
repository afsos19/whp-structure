/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.PhraseologyCategory = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/CATEGORIA/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      this.getPhraseologyCategories();
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/CATEGORIA/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/CATEGORIA/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.PhraseologyCategory.setCategory(id);
    }
  },
  setCategory:(id) =>{
    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: `${pathsApi.phraseologyCategoryGetById}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (response) {

          $("#fraseologiaCategoriaEditHdId").val(response.id);
          $("#fraseologiaCategoriaEditName").val(response.name);
      },

      error: function () {
        APP.controller.Main.getUser();
      },

      complete: function () {
        APP.component.Loading.hide();
      }
    });
  },
  Update: () => {

    if ($('#fraseologiaCategoriaEditName').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja editar essa categoria ?",
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
            url: pathsApi.phraseologyCategoryUpdate,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Name: $('#fraseologiaCategoriaEditName').val(), Id: $('#fraseologiaCategoriaEditHdId').val() }),
            cache: false,
            beforeSend: function () {
              APP.controller.Main.getUser();
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Categoria/Listar";
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
  Save: () => {

    APP.controller.Main.getUser();
    if ($('#fraseologiaCategoriaCadName').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja cadastrar essa categoria ?",
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
            url: pathsApi.phraseologyCategorySave,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Name: $('#fraseologiaCategoriaCadName').val() }),
            cache: false,
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Categoria/Listar";
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
  getPhraseologyCategories: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.phraseologyCategoryList,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        $('.tbody-fraseologia-categoria').html('');
        if (response.length > 0) {
          response.map(category => {
            $('.tbody-fraseologia-categoria').append(`      
            
            <tr>
            <th data-status="${category.id}">
              ${category.id}
            </th>
            <th data-descricao="${category.name}">
              ${category.name}
            </th>
            <th>
              <a href="/Fraseologia/Categoria/Editar?id=${category.id}" class="editar" >Editar</a>
            </th>
          </tr>
            `);
          });
        }
    
        APP.controller.PhraseologyCategory.setDataTable(true);

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
    $('#tabela-fraseologia-categoria').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
