using System;


namespace Whp.MaisTop.Business.Dto.TrainingAcademy
{
    public class UsuarioLoginIntegracao
    {
        public int idUsuario { get; set; }
        public int idPerfil { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public UsuarioLoginIntegracao()
        {
            idUsuario = 0;
            idPerfil = 0;
            Login = String.Empty;
            Nome = String.Empty;
            Email = String.Empty;

        }
    }
}
