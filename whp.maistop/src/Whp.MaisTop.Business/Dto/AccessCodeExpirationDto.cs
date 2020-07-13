using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class AccessCodeExpirationDto
    {
        [Required]
        public string Cellphone { get; set; }
    }
}
