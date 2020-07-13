/*
|--------------------------------------------------------------------------
| Planilhas Funcionários
|--------------------------------------------------------------------------
*/

APP.component.PlanilhasFuncionarios = {
  init() {},

  envioStatusFuncionarios() {
    const periodo = {
      CurrentMonth: $('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-mes').val(),
      CurrentYear: $('[data-planilha="funcionarios"] .subida-planilhas-dados-importacao-ano').val()
    };
    const data = new Date().toLocaleDateString().split('/');
    const mesAtual = data[1];
    const anoAtual = data[2];
    if (periodo.CurrentMonth > mesAtual && periodo.CurrentYear == anoAtual) {
      APP.component.Modal.closeModalSuperior();
      return APP.component.Modal.mensagemModalSuperior('Erro', 'Data escolhida não permitida.');
    }
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    //
    $.ajax({
      async: true,
      crossDomain: true,
      method: 'POST',
      url: pathApi.verificarArquivoFuncionarios,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify(periodo),
      beforeSend: function() {
        $('.subida-planilhas-funcionarios-container-upload').html('');
        $('.subida-planilhas-funcionarios-container-processo').html('');
        $('.subida-planilhas-funcionarios-container-progresso').html('');
        APP.component.Loading.show();
      },
      success: function(result) {
        if (result.fileStatus.description == 'PENDENTE') {
          $('.subida-planilhas-funcionarios-container-processo').append(`
            <div class="subida-planilhas-etapas-processo">
              <div class="subida-planilhas-etapas-processo-bg"></div>
              <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="upload">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Upload</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="validando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Validando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="importacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Importação</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-funcionarios-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>33,33</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor" style="width: 33.33%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'EM ANDAMENTO') {
          $('.subida-planilhas-funcionarios-container-processo').append(`
            <div class="subida-planilhas-etapas-processo">
              <div class="subida-planilhas-etapas-processo-bg"></div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="upload">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Upload</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="validando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Validando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="importacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Importação</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-funcionarios-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>66,66</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor" style="width: 66.6%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'FINALIZADO COM SUCESSO') {
          $('.subida-planilhas-funcionarios-container-processo').append(`
              <div class="subida-planilhas-etapas-processo">
                <div class="subida-planilhas-etapas-processo-bg"></div>
                <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="upload">
                  <div class="subida-planilhas-etapas-processo-item-img"></div>
                  <p>Upload</p>
                </div>
                <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="validando">
                  <div class="subida-planilhas-etapas-processo-item-img"></div>
                  <p>Validando Dados</p>
                </div>
                <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="importacao">
                  <div class="subida-planilhas-etapas-processo-item-img"></div>
                  <p>Importação</p>
                </div>
              </div>
            `);
          $('.subida-planilhas-funcionarios-container-progresso').append(`
              <div class="subida-planilhas-barra-progresso">
                <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
                <p class="subida-planilhas-barra-progresso-porcentagem cor-branca"><span>100</span>%</p>
                <div class="subida-planilhas-barra-progresso-cor" style="width: 100%;"></div>
              </div>
            `);
        } else if (result.fileStatus.description == 'FINALIZADO COM ERRO') {
          const erros = result.saleFileDataError.map(item => {
            return {
              linha: item.line,
              erro: item.description,
              planilha: item.spreedsheet
            };
          });
          $('.subida-planilhas-funcionarios-container-processo').append(`
            <div class="subida-planilhas-etapas-processo">
              <div class="subida-planilhas-etapas-processo-bg"></div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="upload">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Upload</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item error" data-etapa-publicacao="validando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Validando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="importacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Importação</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-funcionarios-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <div class="div-erro-subida" style="left: calc(66.66% - 13px)">
                <span>Finalizado com erro</span>
              </div>
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>66,66</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor error" style="width: 66.66%;"></div>
            </div>
            <div class="subida-planilhas-aviso-erro">
              <table id="data-table-erros-funcionarios"></table>
            </div>
          `);
          $('#data-table-erros-funcionarios').DataTable({
            data: erros,
            bProcessing: true,
            destroy: true,
            deferRender: true,
            ordering: false,
            columns: [{ title: 'Linha', data: 'linha' }, { title: 'Erro', data: 'erro' }, { title: 'Planilha', data: 'planilha' }],
            oLanguage: {
              sProcessing: 'Processando...',
              sLengthMenu: 'Mostrar _MENU_ registros por página',
              sSearch: 'Pesquisar:',
              sInfo: 'Mostrar _START_ até _END_ de _MAX_ registros',
              sInfoFiltered: '(de um total de _MAX_ registros)',
              sInfoEmpty: 'Mostrando 0 até 0 de 0 registros',
              sZeroRecords: 'Nenhum resultado encontrado',
              sInfoPostFix: '',
              sInfoThousands: '',
              sUrl: '',
              oPaginate: {
                sFirst: 'Primeira',
                sPrevious: 'Anterior',
                sNext: 'Próxima',
                sLast: 'Última'
              }
            }
          });
          $('.subida-planilhas-aviso-erro').append(`
            <button class="btn btn-reiniciar" id="subida-planilhas-btn-funcionarios-reiniciar">Reiniciar</button>
          `);
        }
      },
      error: function(result) {
        $('.subida-planilhas-funcionarios-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
              <div class="subida-planilhas-upload-arquivo-titulo">
                <p>Arquivo:</p>
              </div>
              <div class="subida-planilhas-upload-arquivo-campos">
                <input
                  class="subida-planilhas-upload-arquivo-input-hidden"
                  type="file"
                  id="importar-arquivo-funcionarios-anexo"
                  accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                />
                <div class="subida-planilhas-upload-arquivo-info-arquivo">
                  <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-funcionarios-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                  <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
                </div>
                <div class="subida-planilhas-upload-arquivo-container">
                  <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-funcionarios">Importar</a>
                  <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-hierarquia.xlsx" target="_blank" rel="noopener noreferrer"
                    >Baixar modelo de arquivo</a
                  >
                </div>
              </div>
            </div>
        `);
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  enviarPlanilhaFuncionarios(perfil) {
    const pathApi = APP.controller.Api.pathsApi();
    const token = sessionStorage.getItem('token');
    const formData = new FormData();
    const imagem = $(`[data-planilha="${perfil}"] #importar-arquivo-${perfil}-anexo`)[0].files[0];
    formData.append('CurrentMonth', $(`[data-planilha="${perfil}"] .subida-planilhas-dados-importacao-mes`).val());
    formData.append('CurrentYear', $(`[data-planilha="${perfil}"] .subida-planilhas-dados-importacao-ano`).val());
    formData.append('file', imagem);

    $.ajax({
      type: 'POST',
      data: formData,
      contentType: false,
      processData: false,
      cache: false,
      url: pathApi.enviarArquivoFuncionarios,
      headers: {
        Authorization: `Bearer ${token}`
      },
      beforeSend: function() {
        $('.subida-planilhas-funcionarios-container-upload').html('');
        $('.subida-planilhas-funcionarios-container-processo').html('');
        $('.subida-planilhas-funcionarios-container-progresso').html('');
        APP.component.Loading.show();
      },
      success: function(result) {
        if (result[0] == 'Carga recebida com sucesso. Aguarde a validação dos seus dados.') {
          $('.subida-planilhas-funcionarios-container-processo').append(`
          <div class="subida-planilhas-etapas-processo">
            <div class="subida-planilhas-etapas-processo-bg"></div>
            <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="upload">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Upload</p>
            </div>
            <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="validando">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Validando Dados</p>
            </div>
            <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="importacao">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Importação</p>
            </div>
          </div>
        `);
          $('.subida-planilhas-funcionarios-container-progresso').append(`
          <div class="subida-planilhas-barra-progresso">
            <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
            <p class="subida-planilhas-barra-progresso-porcentagem"><span>33,33</span>%</p>
            <div class="subida-planilhas-barra-progresso-cor" style="width: 33.33%;"></div>
          </div>
        `);
        } else {
          $('.subida-planilhas-funcionarios-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
              <div class="subida-planilhas-upload-arquivo-titulo">
                <p>Arquivo:</p>
              </div>
              <div class="subida-planilhas-upload-arquivo-campos">
                <input
                  class="subida-planilhas-upload-arquivo-input-hidden"
                  type="file"
                  id="importar-arquivo-funcionarios-anexo"
                  accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                />
                <div class="subida-planilhas-upload-arquivo-info-arquivo">
                  <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-funcionarios-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                  <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
                </div>
                <div class="subida-planilhas-upload-arquivo-container">
                  <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-funcionarios">Importar</a>
                  <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-hierarquia.xlsx" target="_blank" rel="noopener noreferrer"
                    >Baixar modelo de arquivo</a
                  >
                </div>
              </div>
            </div>
        `);
          APP.component.Modal.closeModalSuperior();
          return APP.component.Modal.mensagemModalSuperior('Erro', result[0]);
        }
      },
      error: function(result) {
        $('.subida-planilhas-funcionarios-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
              <div class="subida-planilhas-upload-arquivo-titulo">
                <p>Arquivo:</p>
              </div>
              <div class="subida-planilhas-upload-arquivo-campos">
                <input
                  class="subida-planilhas-upload-arquivo-input-hidden"
                  type="file"
                  id="importar-arquivo-funcionarios-anexo"
                  accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
                />
                <div class="subida-planilhas-upload-arquivo-info-arquivo">
                  <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-funcionarios-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                  <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
                </div>
                <div class="subida-planilhas-upload-arquivo-container">
                  <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-funcionarios">Importar</a>
                  <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-hierarquia.xlsx" target="_blank" rel="noopener noreferrer"
                    >Baixar modelo de arquivo</a
                  >
                </div>
              </div>
            </div>
        `);
        APP.component.Modal.closeModalSuperior();
        if (result.responseText == 'Arquivo não encontrado') {
          return APP.component.Modal.mensagemModalSuperior('Erro', 'Anexe algum arquivo para continuar.');
        }
        return APP.component.Modal.mensagemModalSuperior('Erro', 'Por favor, tente novamente em alguns instantes.');
      },
      complete: function(result) {
        APP.component.Loading.hide();
      }
    });
  },

  reiniciarSubidaFuncionarios() {
    $('body').on('click', '#subida-planilhas-btn-funcionarios-reiniciar', function(e) {
      e.preventDefault();
      $('.subida-planilhas-funcionarios-container-upload').html('');
      $('.subida-planilhas-funcionarios-container-processo').html('');
      $('.subida-planilhas-funcionarios-container-progresso').html('');
      $('.subida-planilhas-funcionarios-container-upload').append(`
        <div class="subida-planilhas-upload-arquivo">
          <div class="subida-planilhas-upload-arquivo-titulo">
            <p>Arquivo:</p>
          </div>
          <div class="subida-planilhas-upload-arquivo-campos">
            <input
              class="subida-planilhas-upload-arquivo-input-hidden"
              type="file"
              id="importar-arquivo-funcionarios-anexo"
              accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
            />
            <div class="subida-planilhas-upload-arquivo-info-arquivo">
              <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-funcionarios-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
              <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
            </div>
            <div class="subida-planilhas-upload-arquivo-container">
              <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-funcionarios">Importar</a>
              <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-hierarquia.xlsx" target="_blank" rel="noopener noreferrer"
                >Baixar modelo de arquivo</a
              >
            </div>
          </div>
        </div>
      `);
    });
  }
};
