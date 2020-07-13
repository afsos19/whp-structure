using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Occurrence : BaseEntity
    {
        public OccurrenceStatus OccurrenceStatus { get; set; }
        public OccurrenceSubject OccurrenceSubject { get; set; }
        public OccurrenceContactType OccurrenceContactType { get; set; }
        public User User { get; set; }
        public string Code { get; set; }
        public string File { get; set; }
        public string BrazilCTCall { get; set; }
        public bool Critical { get; set; }
        public bool Participant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? RedirectedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool Activated { get; set; }
        public string LastIteration { get; set; }
    }
}
