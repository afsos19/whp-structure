using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OccurrenceMessage : BaseEntity
    {
        public Occurrence Occurrence { get; set; }
        public User User { get; set; }
        public OccurrenceMessageType OccurrenceMessageType { get; set; }
        public string Message { get; set; }
        public string File { get; set; }
        public bool Internal { get; set; }
        public bool Catalog { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
