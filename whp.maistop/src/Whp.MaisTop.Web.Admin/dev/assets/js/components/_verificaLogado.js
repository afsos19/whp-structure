/*
|--------------------------------------------------------------------------
| Controller Fale Conosco
|--------------------------------------------------------------------------
*/

APP.component.VerificaLogado = {
  
  init : function () {    
    this.setup();
  },
  
  setup : function () {
  },
    
  verificaTokenExpirado: function() {
    const token = sessionStorage.getItem('tokenAdmin');
    if (!token) {
      
    }else{
      var user = sessionStorage.getItem("a");
      var password = sessionStorage.getItem("b");
      if(user && password){
        APP.controller.Login.getUser(atob(user), atob(password),false);
      }
    } 
  }
      
};