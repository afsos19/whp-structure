using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ExpiredPasswordDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
