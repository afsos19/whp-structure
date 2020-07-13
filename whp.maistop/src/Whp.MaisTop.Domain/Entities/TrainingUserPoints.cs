using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class TrainingUserPoints : BaseEntity
    {
        public User User { get; set; }
        public int TrainingId { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingStatus { get; set; }
        public int ResultId { get; set; }
        public DateTime TrainingStartedAt { get; set; }
        public DateTime TrainingEndedAt { get; set; }
        public DateTime TrainingDoneAt { get; set; }
        public decimal Punctuation { get; set; }
        public decimal Percentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
