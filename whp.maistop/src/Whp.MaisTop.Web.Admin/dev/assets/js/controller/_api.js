/*
|--------------------------------------------------------------------------
| Controller
|--------------------------------------------------------------------------
*/

APP.controller.Api = {
  init() {
    this.setup();
  },

  setup() {},

  pathImagem() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/PhotoPerfil/';
  },
  pathNews() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/News/';
  },
  pathOcorrencias() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Occurrence/';
  },
  pathProdutos() {
    return 'https://programamaistop.com.br/api/wwwroot/Content/Products/';
    //return 'https://localhost:44341/wwwroot/Content/Products/';
  },
  

  pathApi() {
    //local

    //const path = "https://localhost:44341";

    //dev

    //const path = "http://novoprogramamaistop.fullbarhomologa.com.br/api";

    //prod
    const path = "https://programamaistop.com.br/api";

    const paths = {
      phraseologySave: `${path}/Phraseology/SavePhraseology`,
      phraseologyUpdate: `${path}/Phraseology/UpdatePhraseology`,
      phraseologyList: `${path}/Phraseology/GetAllPhraseology`,
      phraseologyGetById: `${path}/Phraseology/GetPhraseology`,
      phraseologyTypeSubjectSave: `${path}/PhraseologyTypeSubject/SavePhraseologyTypeSubject`,
      phraseologyTypeSubjectUpdate: `${path}/PhraseologyTypeSubject/UpdatePhraseologyTypeSubject`,
      phraseologyTypeSubjectList: `${path}/PhraseologyTypeSubject/GetAllPhraseologyTypeSubject`,
      phraseologyTypeSubjectGetById: `${path}/PhraseologyTypeSubject/GetPhraseologyTypeSubject`,
      phraseologyTypeSubjectsBySubjectId: `${path}/PhraseologyTypeSubject/GetPhraseologyTypeSubjectBySubjectId`,
      phraseologySubjectSave: `${path}/PhraseologySubject/SavePhraseologySubject`,
      phraseologySubjectUpdate: `${path}/PhraseologySubject/UpdatePhraseologySubject`,
      phraseologySubjectList: `${path}/PhraseologySubject/GetAllPhraseologySubject`,
      phraseologySubjectGetById: `${path}/PhraseologySubject/GetPhraseologySubject`,
      phraseologySubjectsByCategoryId: `${path}/PhraseologySubject/GetPhraseologySubjectsByCategoryId`,
      phraseologyCategorySave: `${path}/PhraseologyCategory/SavePhraseologyCategory`,
      phraseologyCategoryUpdate: `${path}/PhraseologyCategory/UpdatePhraseologyCategory`,
      phraseologyCategoryList: `${path}/PhraseologyCategory/GetAllPhraseologyCategory`,
      phraseologyCategoryGetById: `${path}/PhraseologyCategory/GetPhraseologyCategory`,
      loginAdmin: `${path}/Authentication/Login`,
      getShopByUser:`${path}/Shop/GetShop`,
      getRedes: `${path}/Network/GetAll`,
      getCargo: `${path}/Office/GetAll`,
      updateUser:`${path}/User/UpdateUser`,
      getUserList:`${path}/User/GetUserListAdmin`,
      getusuario:`${path}/User/GetUserAdmin`,
      getusuarioById:`${path}/User/GetUser`,
      exportUsersToExcel:`${path}/User/ExportUsersToExcel`,
      getusuarioStatusLog:`${path}/User/GetUserStatusLog`,
      listarOcorrenciaPorId:`${path}/Occurrence/GetMessagesOccurenceByUser`,
      listarOcorrenciaEaiPorId:`${path}/Occurrence/GetMessagesOccurenceByUserEai`,
      listarOcorrencia: `${path}/Occurrence/GetOccurrenceAdmin`,
      listarOcorrenciaEAI: `${path}/Occurrence/GetOccurrenceAdminEai`,
      salvarMensagem: `${path}/Occurrence/SaveMessage`,
      salvarOcorrenciaDetalhe: `${path}/Occurrence/UpdateOccurence`,
      salvarOcorrencia: `${path}/Occurrence/SaveOccurrence`,
      MirrorAccess:`${path}/Authentication/MirrorAccessByCpf`,
      MirrorAccessId:`${path}/Authentication/MirrorAccessById`,
      UserStatus:`${path}/UserStatus/GetAll`,
      GetAssuntos:`${path}/Occurrence/GetAllOccurrenceSubject`,
      GetPendingClassification:`${path}/AdminValidation/GetPendingClassification`,
      GetProducers:`${path}/Product/GetProducers`,
      GetCategories:`${path}/Product/GetCategories`,
      GetProductsSku:`${path}/AdminValidation/GetProducts`,
      GetProducts:`${path}/Product/GetAllProducts`,
      saveProduct:`${path}/Product/SaveProduct`,
      updateProduct:`${path}/Product/UpdateProduct`,
      getProductById:`${path}/Product/GetProductById`,
      UpdateSaleFileDataSku:`${path}/AdminValidation/UpdateSaleFileDataSku`,
      GetFocusProducts:`${path}/FocusProduct/GetFocusProducts`,
      saveFocusProduct:`${path}/FocusProduct/SaveFocusProduct`,
      updateFocusProduct:`${path}/FocusProduct/UpdateFocusProduct`,
      getFocusProductById:`${path}/FocusProduct/GetFocusProductById`,
      DeleteFocusProduct:`${path}/FocusProduct/DeleteFocusProduct`,
      GetParticipantProducts:`${path}/ParticipantProduct/GetParticipantProducts`,
      saveParticipantProduct:`${path}/ParticipantProduct/SaveParticipantProduct`,
      updateParticipantProduct:`${path}/ParticipantProduct/UpdateParticipantProduct`,
      getParticipantProductById:`${path}/ParticipantProduct/GetParticipantProductById`,
      DeleteParticipantProduct:`${path}/ParticipantProduct/DeleteParticipantProduct`,
      GetPreProcessingSku:`${path}/Report/PreProcessingSales`,
      GetPreProcessingSale:`${path}/Report/PreProcessingSalesPunctuation`,
      GetUserBalance:`${path}/Punctuation/GetUserBalance`,
      BaseTrackingSale:`${path}/Report/BaseTrackingSale`,
      BaseTrackingHierarchy:`${path}/Report/BaseTrackingHierarchy`,
      ApprovePreProcessing:`${path}/AdminValidation/ApproveFiles`,
      ApprovePreProcessingSale:`${path}/Sale/ApproveSaleToProcessing`,
      GetTrainingManagersReport:`${path}/Training/GetTrainingManagersReport`
    };

    return paths;
  }
};
