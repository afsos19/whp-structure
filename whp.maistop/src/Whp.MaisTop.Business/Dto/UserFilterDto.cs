using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class UserFilterDto
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Office { get; set; }
        public int UserStatus { get; set; }
        public string cnpj { get; set; }
        public int network { get; set; }

    }
}
