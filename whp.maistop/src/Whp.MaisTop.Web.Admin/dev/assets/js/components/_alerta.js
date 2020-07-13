
/*
|--------------------------------------------------------------------------
| Controller Contato
|--------------------------------------------------------------------------
*/

APP.component.Alerta = {


  init : function (_txt, _reload, _href, _color) {
      this.setup();
      this.alerta(_txt, _reload, _href, _color);

  },

  setup : function () {

  },

  alerta : function (title, msg) {
      
      $('#alerta .modal-title').html(title);
      $("#alerta .modal-body p").html(msg);
      $('#alerta').modal('show');

  },

}