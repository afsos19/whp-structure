using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class PhraseologyCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
