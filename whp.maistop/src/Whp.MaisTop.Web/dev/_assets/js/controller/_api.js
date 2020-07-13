/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Api = {
  init() {},

  // DEV   ######################################################################################################################

  // pathImagem() {
  //   return 'https://localhost:44341/wwwroot/Content/PhotoPerfil/';
  // },
  // pathNews() {
  //   return 'https://localhost:44341/wwwroot/Content/News/';
  // },
  // pathOcorrencias() {
  //   return 'https://localhost:44341/wwwroot/Content/Occurrence/';
  // },
  // pathProdutos() {
  //   return 'https://localhost:44341/wwwroot/Content/Products/';
  // },

  // HOMOLOG ######################################################################################################################

  // pathImagem() {
  //   return 'http://novoprogramamaistop.fullbarhomologa.com.br/api/wwwroot/Content/PhotoPerfil/';
  // },
  // pathNews() {
  //   return 'http://novoprogramamaistop.fullbarhomologa.com.br/api/wwwroot/Content/News/';
  // },
  // pathOcorrencias() {
  //   return 'http://novoprogramamaistop.fullbarhomologa.com.br/api/wwwroot/Content/Occurrence/';
  // },
  // pathProdutos() {
  //   return 'http://novoprogramamaistop.fullbarhomologa.com.br/api/wwwroot/Content/Products/';
  // },

  // PROD ##########################################################################################################################

  pathImagem() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/PhotoPerfil/';
  },
  pathNews() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/News/';
  },
  pathCampanhas() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Campaign/';
  },
  pathOcorrencias() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Occurrence/';
  },
  pathProdutos() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Products/';
  },
  pathPesquisa() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/ImagensPesquisa/';
  },
  pathRedes() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/LogosRedes/';
  },
  pathRegulamentos() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Regulamentos/';
  },

  // API ############################################################################################################################
  pathsApi() {
    //DEV
    //  const url = 'https://localhost:44341';
    // HOMOLOG
    // const url = 'http://programamaistopapi.fullbarhomologa.com.br';
    // const url = 'http://novoprogramamaistop.fullbarhomologa.com.br/api';
    // PROD
    const url = 'https://programamaistop.com.br/api';
    //
    const paths = {
      getUser: `${url}/User/GetUser`, // Get User - utilizado para preencher as informações laterais do usuário nas páginas
      // Authentication
      esqueceuSenha: `${url}/Authentication/ForgotPassword`, // Esqueci Senha
      smsPrimeiroAcesso: `${url}/Authentication/GenerateAccessCode`, // Gerar código SMS
      primeiroAcesso: `${url}/Authentication/FirstAccess`, // Primeiro Acesso
      login: `${url}/Authentication/Login`, // Fazer login
      cadastroPrimeiroAcesso: `${url}/Authentication/PreRegistration`, // Finalizar cadastro primeiro acesso
      catalogoDePremios: `${url}/Authentication/ShopAuthenticate`,
      treinamentoAcademia: `${url}/Authentication/TrainingAcademyAuthenticate`,
      // Product
      produtosParticipantes: `${url}/Product/GetParticipantProduct`, // Produtos Participantes
      produtosFoco: `${url}/Product/GetFocusProduct`, // Produtos Foco
      // User
      atualizarCadastro: `${url}/User/UpdateUser`, // Atualizar cadastro
      meuPerfil: `${url}/User/GetUser`, // Recuperar dados do usuário no meu perfil
      senhaExpirada: `${url}/User/SendAccessCodeExpiration`,
      verificaCodigoSMS: `${url}/User/UpdateUserExpiredPassword`,
      // News
      noticias: `${url}/News/GetNews`, // retorna as noticias no usuario
      // Campanhas
      campanhas: `${url}/Campaign/GetCampaigns`, // retorna as campánhas no usuario
      // Occurrence
      abrirOcorrencia: `${url}/Occurrence/SaveOccurrence`, // Enviar primeira ocorrencia
      atualizarOcorrencia: `${url}/Occurrence/SaveMessage`, // envia nova mensagem na ocorrencia
      todasOcorrencias: `${url}/Occurrence/GetOccurrenceByUser`, // retorna todas ocorrencias do usuario
      mensagensOcorrencia: `${url}/Occurrence/GetMessagesOccurenceByUser`, // retorna as mensagens da ocorrencia do parametro
      // Punctuation
      pegarExtratoGeral: `${url}/Punctuation/GetUserExtract`, // retorna pontuação extrato geral
      filtroExtrato: `${url}/Punctuation/GetUserCredits`, // retorna o extrato filtrado por mes / ano
      filtroVendas: `${url}/Sale/GetUserSales`, // retorna as vendas no extrato
      historico: `${url}/Order/GetUserOrder`, // retorn historico do extrato
      // Shop
      pegarLoja: `${url}/Shop/GetShop`, // informações sobre a loja
      // Minha Equipe
      equipeCadastro: `${url}/User/GetTeamUsers`, // retorna minha equipe cadastro
      equipeTreinamento: `${url}/Training/GetTeamSales`, // retorna minha equipe treinamento
      equipeVendas: `${url}/Sale/GetTeamSales`, // retorna minha equipe vendas
      // Regulamento
      aceiteRegulamento: `${url}/User/UpdatePrivacityPolicy/`, // altera o aceite do regulamento > passa boolean na url
      // Quiz
      pesquisa: `${url}/Quiz/GetCurrentQuiz`, // pesquisa
      salvarPesquisa: `${url}/Quiz/SaveQuiz`, // salvar pesquisa
      // Importation
      enviarArquivoFuncionarios: `${url}/Importation/SendHierarchyFile`, // envia planilha de funcionarios
      enviarArquivoVendas: `${url}/Importation/SendSaleFile`, // envia planilha de vendas
      verificarArquivoFuncionarios: `${url}/Importation/GetHierarchyFileStatus`, // verifica status da planilha de funcionarios
      verificarArquivoVendas: `${url}/Importation/GetSaleFileStatus`, // verifica status da planilha de vendas
      //Friend Invite
      pegarCodigoDeConvite: `${url}/FriendInvite/GetUserAccessCode`, // retorna o codigo de convite
      enviarCodigoDeConviteSMS: `${url}/FriendInvite/SendAccessCodeInvite`, // retorna o codigo de convite
      historicoDeConvites: `${url}/FriendInvite/GetInvitedUsers`, // retorna o historico de convites enviados
      EnviarUsuarioConvidado: `${url}/FriendInvite/DoCaduserInvited`, // Envia o cadastro do amigos convidado
      // IP
      verificaIp: 'https://api.ipify.org' // pega e envia o ip na hora do login
    };
    return paths;
  }
};
