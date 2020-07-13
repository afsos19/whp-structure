using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class PhraseologySubjectDto
    {
        public int Id { get; set; }
        [Required]
        public int PhraseologyCategoryId { get; set; }
        public PhraseologyCategory PhraseologyCategory { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
