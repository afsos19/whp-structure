/*
|--------------------------------------------------------------------------
| Controller Fale Conosco
|--------------------------------------------------------------------------
*/

APP.component.ValidaCpf = {
  
  init : function () {    
    this.setup();
  },
  
  setup : function () {
  },
    
    validateCpf: function(cpf) {
      var messageCPFInvalido = 'Por favor, preencha um cpf válido!';
      cpf = cpf.replace(/\D/g, '');

        if (cpf == '') {
          APP.component.Alerta.alerta("Por favor, preencha o cpf");
          return false;
        }
        // Validação do CPF
        if (
          cpf.length > 14 || 
          cpf == '00000000000' ||
          cpf == '11111111111' || 
          cpf == '22222222222' || 
          cpf == '33333333333' || 
          cpf == '44444444444' || 
          cpf == '55555555555' || 
          cpf == '66666666666' || 
          cpf == '77777777777' || 
          cpf == '88888888888' || 
          cpf == '99999999999'
          ) {
            APP.component.Alerta.alerta(messageCPFInvalido);
            return false;
          }     
          
          // var add = 0;	
          // var rev;
          //   for (var i=0; i < 9; i ++)		
          //     add += parseInt(cpf.charAt(i)) * (10 - i);	
          //     rev = 11 - (add % 11);	

          // if (rev == 10 || rev == 11) {
          //   rev = 0;
          // }
            
          // if (rev != parseInt(cpf.charAt(9)))	{
          //   APP.component.Alerta.alerta(messageCPFInvalido);
          //   return false;		
          // }

          // var addSecond = 0;
          // var revSecond;
            
          // for (var i = 0; i < 10; i ++)		
          // addSecond += parseInt(cpf.charAt(i)) * (11 - i);	
	        //   revSecond = 11 - (addSecond % 11);	
	        //   if (revSecond == 10 || revSecond == 11)	{
          //     revSecond = 0;	
          //   }
		        
          //   if (revSecond != parseInt(cpf.charAt(10))) {
          //     APP.component.Alerta.alerta(messageCPFInvalido);
          //     return false;		
          //   }             

            return true;
        
      }
      
    };