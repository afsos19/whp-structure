/*
|--------------------------------------------------------------------------
| Controller Fale Conosco
|--------------------------------------------------------------------------
*/

APP.component.ValidaEmail = {
  
  init : function () {    
    this.setup();
  },
  
  setup : function () {
  },
    
  validateEmail: function(email) {
    var messageEmail = 'Por favor, preencha um email válido!';
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(email) == false) {
      APP.component.Alerta.alerta(messageEmail);
    } else if (email.indexOf('combr') != -1) {
      APP.component.Alerta.alerta(messageEmail);
    }
    return re.test(email);
  }   
      
};