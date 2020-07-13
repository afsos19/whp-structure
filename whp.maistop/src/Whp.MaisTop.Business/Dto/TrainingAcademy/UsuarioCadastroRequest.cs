using System;


namespace Whp.MaisTop.Business.Dto.TrainingAcademy
{
    public class UsuarioCadastroRequest
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int IdPerfil { get; set; }

        public UsuarioCadastroRequest()
        {
            Login = String.Empty;
            Nome = String.Empty;
            Email = String.Empty;
            Ip = String.Empty;
            IdPerfil = 0;
        }

    }
}
