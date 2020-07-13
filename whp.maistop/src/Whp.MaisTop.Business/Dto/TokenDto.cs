using System;
using System.Collections.Generic;
using System.Text;

namespace Ssg.MaisSamsung.Business.Dto
{
    public class TokenDto
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string IdUsuario { get; set; }
    }
}
