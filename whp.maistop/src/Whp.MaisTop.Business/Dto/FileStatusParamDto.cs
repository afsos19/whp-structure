using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class FileStatusParamDto
    {
        [Required]
        public int CurrentMonth { get; set; }
        [Required]
        public int CurrentYear { get; set; }
    }
}
