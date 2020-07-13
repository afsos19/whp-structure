using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Phraseology : BaseEntity
    {
        public string Answer { get; set; }
        public bool Activated { get; set; }
        public PhraseologyTypeSubject PhraseologyTypeSubject { get; set; }
    }
}
