/*
|--------------------------------------------------------------------------
| Planilhas Funcionários
|--------------------------------------------------------------------------
*/

APP.component.PlanilhasVendas = {
  init() {},

  envioStatusVendas() {
    const periodo = {
      CurrentMonth: $('[data-planilha="vendas"] .subida-planilhas-dados-importacao-mes').val(),
      CurrentYear: $('[data-planilha="vendas"] .subida-planilhas-dados-importacao-ano').val()
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
      url: pathApi.verificarArquivoVendas,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
        'cache-control': 'no-cache'
      },
      processData: false,
      data: JSON.stringify(periodo),
      beforeSend: function() {
        $('.subida-planilhas-vendas-container-upload').html('');
        $('.subida-planilhas-vendas-container-processo').html('');
        $('.subida-planilhas-vendas-container-progresso').html('');
        APP.component.Loading.show();
      },
      success: function(result) {
        if (result.fileStatus.description == 'PENDENTE') {
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>20</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor" style="width: 20%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'EM ANDAMENTO') {
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>40</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor" style="width: 40%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'FINALIZADO COM SUCESSO' || result.fileStatus.description == 'VALIDADO AUTOMATICAMENTE' || result.fileStatus.description == 'VALIDADO' || result.fileStatus.description == 'VENDAS PROCESSADA') {
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem cor-branca"><span>100</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor" style="width: 100%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'NÃO PARTICIPANTE' || result.fileStatus.description == 'NÃO VALIDADO' || result.fileStatus.description == 'NÃO LOCALIZADO') {
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item error" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
            <div class="subida-planilhas-aviso-erro">
              <p>${result.fileStatus.description}</p>
              <button class="btn btn-reiniciar" id="subida-planilhas-btn-vendas-reiniciar">Reiniciar</button>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <div class="div-erro-subida" style="left: calc(60% - 13px)">
                <span>Finalizado com erro</span>
              </div>
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>60</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor error" style="width: 60%;"></div>
            </div>
          `);
        } else if (result.fileStatus.description == 'EM ANDAMENTO' && result.saleFileData) {
          const erros = result.saleFileData.map(item => {
            return {
              sku: item.productDescription,
              erro: item.saleFileSkuStatus.description
            };
          });
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item error" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <div class="div-erro-subida" style="left: calc(60% - 13px)">
                <span>Finalizado com erro</span>
              </div>
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>60</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor error" style="width: 60%;"></div>
            </div>
            <div class="subida-planilhas-aviso-erro">
              <table id="data-table-erros-vendas"></table>
            </div>
          `);
          $('#data-table-erros-vendas').DataTable({
            data: erros,
            bProcessing: true,
            destroy: true,
            deferRender: true,
            ordering: false,
            columns: [{ title: 'SKU', data: 'sku' }, { title: 'Erro', data: 'erro' }],
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
            <button class="btn btn-reiniciar" id="subida-planilhas-btn-vendas-reiniciar">Reiniciar</button>
          `);
        } else if (result.fileStatus.description == 'FINALIZADO COM ERRO') {
          const erros = result.saleFileDataError.map(item => {
            return {
              linha: item.line,
              erro: item.description
            };
          });
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <div class="div-erro-subida" style="left: calc(40% - 13px)">
                <span>Finalizado com erro</span>
              </div>
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>40</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor error" style="width: 40%;"></div>
            </div>
            <div class="subida-planilhas-aviso-erro">
              <table id="data-table-erros-vendas"></table>
            </div>
          `);
          $('#data-table-erros-vendas').DataTable({
            data: erros,
            bProcessing: true,
            destroy: true,
            deferRender: true,
            ordering: false,
            columns: [{ title: 'Linha', data: 'linha' }, { title: 'Erro', data: 'erro' }],
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
            <button class="btn btn-reiniciar" id="subida-planilhas-btn-vendas-reiniciar">Reiniciar</button>
          `);
        } else if (result.fileStatus.description == 'PENDENTE DE CLASSIFICAÇÃO' || result.fileStatus.description == 'PENDENTE WHP') {
          const erros = result.saleFile.map(item => {
            return {
              linha: item.line,
              erro: item.description
            };
          });
          $('.subida-planilhas-vendas-container-processo').append(`
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
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Classificação de Produto</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item active" data-etapa-publicacao="processando">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Processando Dados</p>
              </div>
              <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
                <div class="subida-planilhas-etapas-processo-item-img"></div>
                <p>Pontos Disponíveis</p>
              </div>
            </div>
          `);
          $('.subida-planilhas-vendas-container-progresso').append(`
            <div class="subida-planilhas-barra-progresso">
              <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
              <p class="subida-planilhas-barra-progresso-porcentagem"><span>80</span>%</p>
              <div class="subida-planilhas-barra-progresso-cor error" style="width: 80%;"></div>
            </div>
            <div class="subida-planilhas-aviso-erro">
              <table id="data-table-erros-vendas"></table>
              <button class="btn btn-reiniciar" id="subida-planilhas-btn-vendas-reiniciar">Reiniciar</button>
            </div>
          `);
          $('#data-table-erros-vendas').DataTable({
            data: erros,
            bProcessing: true,
            destroy: true,
            deferRender: true,
            ordering: false,
            columns: [{ title: 'Linha', data: 'linha' }, { title: 'Erro', data: 'erro' }],
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
            <button class="btn btn-reiniciar" id="subida-planilhas-btn-vendas-reiniciar">Reiniciar</button>
          `);
        }
      },
      error: function(result) {
        $('.subida-planilhas-vendas-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
            <div class="subida-planilhas-upload-arquivo-titulo">
              <p>Arquivo:</p>
            </div>
            <div class="subida-planilhas-upload-arquivo-campos">
              <input
                class="subida-planilhas-upload-arquivo-input-hidden"
                type="file"
                id="importar-arquivo-vendas-anexo"
                accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
              />
              <div class="subida-planilhas-upload-arquivo-info-arquivo">
                <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-vendas-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
              </div>
              <div class="subida-planilhas-upload-arquivo-container">
                <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-vendas">Importar</a>
                <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-vendas.xlsx" target="_blank" rel="noopener noreferrer"
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
    //
  },

  enviarPlanilhaVendas(perfil) {
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
      url: pathApi.enviarArquivoVendas,
      headers: {
        Authorization: `Bearer ${token}`
      },
      beforeSend: function() {
        $('.subida-planilhas-vendas-container-upload').html('');
        $('.subida-planilhas-vendas-container-processo').html('');
        $('.subida-planilhas-vendas-container-progresso').html('');
        APP.component.Loading.show();
      },
      success: function(result) {
        if (result[0] == 'Carga recebida com sucesso. Aguarde a validação dos seus dados.') {
          $('.subida-planilhas-vendas-container-processo').append(`
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
            <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="classificacao">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Classificação de Produto</p>
            </div>
            <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="processando">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Processando Dados</p>
            </div>
            <div class="subida-planilhas-etapas-processo-item" data-etapa-publicacao="pontos">
              <div class="subida-planilhas-etapas-processo-item-img"></div>
              <p>Pontos Disponíveis</p>
            </div>
          </div>
        `);
          $('.subida-planilhas-vendas-container-progresso').append(`
          <div class="subida-planilhas-barra-progresso">
            <p class="subida-planilhas-barra-progresso-texto cor-branca">Progresso</p>
            <p class="subida-planilhas-barra-progresso-porcentagem"><span>20</span>%</p>
            <div class="subida-planilhas-barra-progresso-cor" style="width: 20%;"></div>
          </div>
        `);
        } else {
          $('.subida-planilhas-vendas-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
            <div class="subida-planilhas-upload-arquivo-titulo">
              <p>Arquivo:</p>
            </div>
            <div class="subida-planilhas-upload-arquivo-campos">
              <input
                class="subida-planilhas-upload-arquivo-input-hidden"
                type="file"
                id="importar-arquivo-vendas-anexo"
                accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
              />
              <div class="subida-planilhas-upload-arquivo-info-arquivo">
                <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-vendas-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
              </div>
              <div class="subida-planilhas-upload-arquivo-container">
                <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-vendas">Importar</a>
                <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-vendas.xlsx" target="_blank" rel="noopener noreferrer"
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
        $('.subida-planilhas-vendas-container-upload').append(`
          <div class="subida-planilhas-upload-arquivo">
            <div class="subida-planilhas-upload-arquivo-titulo">
              <p>Arquivo:</p>
            </div>
            <div class="subida-planilhas-upload-arquivo-campos">
              <input
                class="subida-planilhas-upload-arquivo-input-hidden"
                type="file"
                id="importar-arquivo-vendas-anexo"
                accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
              />
              <div class="subida-planilhas-upload-arquivo-info-arquivo">
                <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-vendas-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
                <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
              </div>
              <div class="subida-planilhas-upload-arquivo-container">
                <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-vendas">Importar</a>
                <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-vendas.xlsx" target="_blank" rel="noopener noreferrer"
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

  reiniciarSubidaVendas() {
    $('body').on('click', '#subida-planilhas-btn-vendas-reiniciar', function(e) {
      e.preventDefault();
      $('.subida-planilhas-vendas-container-upload').html('');
      $('.subida-planilhas-vendas-container-processo').html('');
      $('.subida-planilhas-vendas-container-progresso').html('');
      $('.subida-planilhas-vendas-container-upload').append(`
        <div class="subida-planilhas-upload-arquivo">
          <div class="subida-planilhas-upload-arquivo-titulo">
            <p>Arquivo:</p>
          </div>
          <div class="subida-planilhas-upload-arquivo-campos">
            <input
              class="subida-planilhas-upload-arquivo-input-hidden"
              type="file"
              id="importar-arquivo-vendas-anexo"
              accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
            />
            <div class="subida-planilhas-upload-arquivo-info-arquivo">
              <label class="subida-planilhas-upload-arquivo-info-arquivo-label" for="importar-arquivo-vendas-anexo"><img src="/_assets/img/planilhas/icon-upload-arquivo.svg"/></label>
              <p class="subida-planilhas-upload-arquivo-info-arquivo-nome"></p>
            </div>
            <div class="subida-planilhas-upload-arquivo-container">
              <a class="subida-planilhas-upload-arquivo-btn btn btn-laranja" id="importar-arquivo-vendas">Importar</a>
              <a class="subida-planilhas-upload-arquivo-baixar-modelo btn btn-download" download href="https://programamaistop.com.br/api/wwwroot/Content/ModelosPlanilhas/modelo-vendas.xlsx" target="_blank" rel="noopener noreferrer"
                >Baixar modelo de arquivo</a
              >
            </div>
          </div>
        </div>
      `);
    });
  }
};
