using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserPunctuation : BaseEntity
    {
        public User User { get; set; }
        public UserPunctuationSource UserPunctuationSource { get; set; }
        public char OperationType { get; set; }
        public int ReferenceEntityId { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public string Description { get; set; }
        public decimal Punctuation { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
