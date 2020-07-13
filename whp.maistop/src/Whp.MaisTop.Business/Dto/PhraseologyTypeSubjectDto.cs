using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class PhraseologyTypeSubjectDto
    {
        public int Id { get; set; }
        [Required]
        public int PhraseologySubjectId { get; set; }
        public PhraseologySubject PhraseologySubject { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
