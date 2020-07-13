/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.PhraseologyTypeSubject = {
  init() {
    this.setup();
  },

  setup() {
    if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/TIPOASSUNTO/LISTAR') !== -1) {
      APP.controller.Main.getUser();
      this.getPhraseologyTypeSubjects();
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/TIPOASSUNTO/CADASTRAR') !== -1) {
      APP.controller.Main.getUser();
      APP.controller.PhraseologyTypeSubject.getSubjects(0);
    } else if (window.location.href.toUpperCase().indexOf('/FRASEOLOGIA/TIPOASSUNTO/EDITAR') !== -1) {
      APP.controller.Main.getUser();
      const id = window.location.href.split('?')[1].split('=')[1];
      APP.controller.PhraseologyTypeSubject.setTypeSubject(id);
    }
  },
  setTypeSubject:(id) =>{
    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: `${pathsApi.phraseologyTypeSubjectGetById}/${id}`,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },
      success: function (response) {

          $("#fraseologiaTipoAssuntoEditHdId").val(response.id);
          $("#fraseologiaTipoAssuntoEditDescricao").val(response.description);
          APP.controller.PhraseologyTypeSubject.getSubjects(response.phraseologySubject.id);
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

    if ($('#fraseologiaTipoAssuntoEditDescricao').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja editar esse tipo assunto ?",
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
            url: pathsApi.phraseologyTypeSubjectUpdate,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Description: $('#fraseologiaTipoAssuntoEditDescricao').val(), PhraseologySubjectId: $('#cmbFraseologiaTipoAssuntoEdit option:selected').val(),Id: $('#fraseologiaTipoAssuntoEditHdId').val()}),
            cache: false,
            beforeSend: function () {
              APP.controller.Main.getUser();
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/TipoAssunto/Listar";
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
    if ($('#fraseologiaTipoAssuntoCadDescricao').val().length == 0) {
      APP.component.Alerta.alerta(
        'Falha',
        'Por favor, preencha os campos obrigatórios.'
      );
    } else {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Você deseja cadastrar esse tipo assunto ?",
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
            url: pathsApi.phraseologyTypeSubjectSave,
            dataType: 'json',
            contentType: 'application/json',
            headers: { Authorization: `Bearer ${token}` },
            data: JSON.stringify({ Description: $('#fraseologiaTipoAssuntoCadDescricao').val(), PhraseologySubjectId: $('#cmbFraseologiaTipoAssuntoCad option:selected').val()}),
            cache: false,
            beforeSend: function () {
              APP.component.Loading.show();
            },
            success: function (result) {
              location.href = "/Fraseologia/TipoAssunto/Listar";
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
  getSubjects: (id) => {

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

        response.map(result => {
          $(`${(id !== 0 ? "#cmbFraseologiaTipoAssuntoEdit" : "#cmbFraseologiaTipoAssuntoCad")}`).append(`
            <option ${parseInt(id) !== 0 && parseInt(id) === parseInt(result.id) ? 'selected': '' } value="${result.id}" >${result.description}</option>
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
  getPhraseologyTypeSubjects: () => {

    const token = sessionStorage.getItem('tokenAdmin');
    const pathsApi = APP.controller.Api.pathApi();

    $.ajax({
      type: 'GET',
      url: pathsApi.phraseologyTypeSubjectList,
      dataType: 'json',
      contentType: 'application/json',
      headers: { Authorization: `Bearer ${token}` },
      cache: false,
      beforeSend: function () {
        APP.component.Loading.show();
      },

      success: function (response) {
        console.log(response)
        $('.tbody-fraseologia-tipo-assunto').html('');
        if (response.length > 0) {
          response.map(subject => {
            $('.tbody-fraseologia-tipo-assunto').append(`      
            
            <tr>
            <th data-status="${subject.id}">
              ${subject.id}
            </th>
            <th data-descricao="${subject.phraseologySubject.description}">
              ${subject.phraseologySubject.description}
            </th>
            <th data-descricao="${subject.description}">
              ${subject.description}
            </th>
            <th>
              <a href="/Fraseologia/TipoAssunto/Editar?id=${subject.id}" class="editar" >Editar</a>
            </th>
          </tr>
            `);
          });
        }
    
        APP.controller.PhraseologyTypeSubject.setDataTable(true);

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
