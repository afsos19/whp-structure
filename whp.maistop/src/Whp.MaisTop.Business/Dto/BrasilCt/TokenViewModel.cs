using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class TokenViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string IdUsuario { get; set; }

        public TokenViewModel()
        {
            access_token = String.Empty;
            token_type = String.Empty;
            expires_in = 0;
            IdUsuario = String.Empty;
        }
    }
}
