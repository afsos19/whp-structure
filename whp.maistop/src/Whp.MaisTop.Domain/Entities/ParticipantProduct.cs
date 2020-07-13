using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class ParticipantProduct : BaseEntity
    {
        public Product Product { get; set; }
        public Network Network { get; set; }
        public decimal Punctuation { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
