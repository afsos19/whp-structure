using System;

namespace Whp.MaisTop.Business.Dto.TrainingAcademy
{
   public class ResponseAutentica
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public UsuarioLoginIntegracao Data { get; set; }

        public ResponseAutentica()
        {
            Sucesso = false;
            Mensagem = String.Empty;
            Data = null;
        }
    }
}
