/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Phraseology = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      this.getPhraseologies();
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      APP.controller.Phraseology.getCategories(0);
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.Phraseology.setPhraseology(id);
    }
  },
  setPhraseology:(id) =>{
    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: `${pathsApi.phraseologyGetById}/${id}`,
      dataType: 'json',
      async: false,
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (response) {

          $("#fraseologiaEditHdId").val(response.id);
          $("#fraseologiaRespostaEdit").val(response.answer);
          APP.controller.Phraseology.getCategories(response.phraseologyTypeSubject.phraseologySubject.phraseologyCategory.id);
          APP.controller.Phraseology.getSubjectsByCategory(response.phraseologyTypeSubject.phraseologySubject.phraseologyCategory.id,'Edit',false, response.phraseologyTypeSubject.phraseologySubject.id);
          APP.controller.Phraseology.getTypeSubjectBySubject(response.phraseologyTypeSubject.phraseologySubject.id,'Edit',response.phraseologyTypeSubject.id);
          
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

    if ($('#fraseologiaRespostaEdit').val().length == 0 || $('#FraseologiaTipoAssuntoEdit option:selected').val() === 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja editar essa fraseologia?",
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
            url: pathsApi.phraseologyUpdate,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Id:$("#fraseologiaEditHdId").val(),Activated: 1, Answer: $('#fraseologiaRespostaEdit').val(), PhraseologyTypeSubjectId: $('#FraseologiaTipoAssuntoEdit option:selected').val()}),
            cache: false,
            beforeSend: function () {
              APP.controller.Main.getUser();
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Listar";
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
    if ($('#fraseologiaRespostaCad').val().length == 0 || $('#FraseologiaTipoAssuntoCad option:selected').val() === 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja cadastrar essa fraseologia ?",
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
            url: pathsApi.phraseologySave,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Activated: 1, Answer: $('#fraseologiaRespostaCad').val(), PhraseologyTypeSubjectId: $('#FraseologiaTipoAssuntoCad option:selected').val()}),
            cache: false,
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Listar";
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
  getCategories: (id) => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.phraseologyCategoryList,
      dataType: 'json',
      async: false,
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        
        $(`${(id !== 0 ? "#fraseologiaCategoriaEdit" : "#fraseologiaCategoriaCad")}`).html("<option value='0'>Selecione</option>");
        $(`${(id !== 0 ? "#FraseologiaTipoAssuntoEdit" : "#FraseologiaTipoAssuntoCad")}`).html("<option value='0'>Selecione</option>");
        $(`${(id !== 0 ? "#FraseologiaAssuntoEdit" : "#FraseologiaAssuntoCad")}`).html("<option value='0'>Selecione</option>");

        response.map(result => {
          $(`${(id !== 0 ? "#fraseologiaCategoriaEdit" : "#fraseologiaCategoriaCad")}`).append(`
            <option ${parseInt(id) !== 0 && parseInt(id) === parseInt(result.id) ? 'selected': '' } value="${result.id}" >${result.name}</option>
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
  getTypeSubjectBySubject: (id,type, selected = 0) => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();
    
   
    $.ajax({
      type: 'GET',
      url: `${pathsApi.phraseologyTypeSubjectsBySubjectId}/${id}`,
      dataType: 'json',
      async: false,
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        $("#FraseologiaTipoAssunto"+type).html("<option value='0'>Selecione</option>")
        response.map(result => {
          $("#FraseologiaTipoAssunto"+type).append(`
            <option ${(selected > 0 && selected === result.id ? 'selected' : '')} value="${result.id}" >${result.description}</option>
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
  getSubjectsByCategory: (id,type,clean = true, selected = 0) => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      async: false,
      url: `${pathsApi.phraseologySubjectsByCategoryId}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        if(clean)
          $("#FraseologiaTipoAssunto"+type).html("<option value='0'>Selecione</option>");

        $("#FraseologiaAssunto"+type).html("<option value='0'>Selecione</option>")
        response.map(result => {
          $("#FraseologiaAssunto"+type).append(`
            <option ${(selected > 0 && selected === result.id ? 'selected' : '')} value="${result.id}" >${result.description}</option>
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
  getPhraseologies: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.phraseologyList,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        console.log(response)
        $('.tbody-fraseologia').html('');
        if (response.length > 0) {
          response.map(phraseology => {
            $('.tbody-fraseologia').append(`      
            
            <tr>
            <th data-status="${phraseology.id}">
              ${phraseology.id}
            </th>
            <th data-descricao="${phraseology.phraseologyTypeSubject.description}">
              ${phraseology.phraseologyTypeSubject.description}
            </th>
            <th data-descricao="${phraseology.answer}">
              ${phraseology.answer}
            </th>
            <th data-descricao="${phraseology.activated ? 'SIM' : 'NÃO'}">
              ${phraseology.activated ? 'SIM' : 'NÃO'}
            </th>
            <th>
              <a href="/Fraseologia/Editar?id=${phraseology.id}" class="editar" >Editar</a>
            </th>
          </tr>
            `);
          });
        }
    
        APP.controller.Phraseology.setDataTable(true);

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
    $('#tabela-fraseologia').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
