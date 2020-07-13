using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class PhraseologyDto
    {
        public int Id { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public bool Activated { get; set; }
        [Required]
        public int PhraseologyTypeSubjectId { get; set; }
        public PhraseologyTypeSubject PhraseologyTypeSubject { get; set; }
    }
}
