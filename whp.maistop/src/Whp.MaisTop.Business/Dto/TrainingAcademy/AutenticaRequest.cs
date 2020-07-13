using System;


namespace Whp.MaisTop.Business.Dto.TrainingAcademy
{
   public class AutenticaRequest
    {
        public string Login { get; set; }
        public string Ip { get; set; }

        public AutenticaRequest()
        {
            Login = String.Empty;
            Ip = String.Empty;
        }
    }
}
