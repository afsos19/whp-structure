using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class LoginDto
    {
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Password { get; set; }
        public string Ip { get; set; }
    }
}
