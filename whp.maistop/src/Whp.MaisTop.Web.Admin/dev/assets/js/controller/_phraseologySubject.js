/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.PhraseologySubject = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/ASSUNTO/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      this.getPhraseologySubjects();
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/ASSUNTO/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      APP.controller.PhraseologySubject.getCategories(0);
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/ASSUNTO/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.PhraseologySubject.setSubject(id);
    }
  },
  setSubject:(id) =>{
    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: `${pathsApi.phraseologySubjectGetById}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (response) {

          $("#fraseologiaAssuntoEditHdId").val(response.id);
          $("#fraseologiaAssuntoEditDescricao").val(response.description);
          APP.controller.PhraseologySubject.getCategories(response.phraseologyCategory.id);
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

    if ($('#fraseologiaAssuntoEditDescricao').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja editar esse assunto ?",
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
            url: pathsApi.phraseologySubjectUpdate,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Description: $('#fraseologiaAssuntoEditDescricao').val(), PhraseologyCategoryId: $('#cmbFraseologiaAssuntoEdit option:selected').val(),Id: $('#fraseologiaAssuntoEditHdId').val()}),
            cache: false,
            beforeSend: function () {
              APP.controller.Main.getUser();
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Assunto/Listar";
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
    if ($('#fraseologiaAssuntoCadDescricao').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja cadastrar esse assunto ?",
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
            url: pathsApi.phraseologySubjectSave,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Description: $('#fraseologiaAssuntoCadDescricao').val(), PhraseologyCategoryId: $('#cmbFraseologiaAssuntoCad option:selected').val()}),
            cache: false,
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/Assunto/Listar";
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
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        response.map(result => {
          $(`${(id !== 0 ? "#cmbFraseologiaAssuntoEdit" : "#cmbFraseologiaAssuntoCad")}`).append(`
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
  getPhraseologySubjects: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.phraseologySubjectList,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {

        $('.tbody-fraseologia-assunto').html('');
        if (response.length > 0) {
          response.map(subject => {
            $('.tbody-fraseologia-assunto').append(`      
            
            <tr>
            <th data-status="${subject.id}">
              ${subject.id}
            </th>
            <th data-descricao="${subject.phraseologyCategory.name}">
              ${subject.phraseologyCategory.name}
            </th>
            <th data-descricao="${subject.description}">
              ${subject.description}
            </th>
            <th>
              <a href="/Fraseologia/Assunto/Editar?id=${subject.id}" class="editar" >Editar</a>
            </th>
          </tr>
            `);
          });
        }
    
        APP.controller.PhraseologySubject.setDataTable(true);

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
    $('#tabela-fraseologia-assunto').DataTable({
      destroy: option,
      language: {
        url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese.json'
      }
    });
  },

};
