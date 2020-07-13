using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserPunctuationReserved : BaseEntity
    {
        public decimal Punctuation { get; set; }
        public User User { get; set; }
        public long ExternalOrderId { get; set; }
        public Order Order { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
