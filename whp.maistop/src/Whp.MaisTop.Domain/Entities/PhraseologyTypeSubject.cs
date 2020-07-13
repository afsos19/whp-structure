using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class PhraseologyTypeSubject : BaseEntity
    {
        public PhraseologySubject PhraseologySubject { get; set; }
        public string Description { get; set; }
    }
}
