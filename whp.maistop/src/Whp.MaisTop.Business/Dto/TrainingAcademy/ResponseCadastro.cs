using System;

namespace Whp.MaisTop.Business.Dto.TrainingAcademy
{
    public class ResponseCadastro
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public UsuarioLoginIntegracao  Data { get; set; }

        public ResponseCadastro()
        {

            Sucesso = false;
            Mensagem = String.Empty;
            Data = null;
        }
    }
}
