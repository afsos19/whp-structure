using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class AccessCodeUserInviteDto : AccessCodeInviteDto
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
