using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities.Academy
{
    public class TrainingUser : BaseEntity
    {
        public Training Training { get; set; }
        public TrainingResult TrainingResult { get; set; }
        public UserAcademy UserAcademy { get; set; }
        public decimal Punctuation { get; set; }
        public decimal Percentage { get; set; }
        public DateTime TrainingDoneAt { get; set; }

    }
}
