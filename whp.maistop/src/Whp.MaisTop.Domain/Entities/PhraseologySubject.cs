using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class PhraseologySubject : BaseEntity
    {
        public PhraseologyCategory PhraseologyCategory { get; set; }
        public string Description { get; set; }
    }
}
