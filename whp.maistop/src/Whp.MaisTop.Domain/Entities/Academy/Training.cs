using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities.Academy
{
    public class Training : BaseEntity
    {
        public string Name { get; set; }
        public DateTime TrainingStartedAt { get; set; }
        public DateTime TrainingEndedAt { get; set; }
        public bool hasTrainingMaterial { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int trainingKind { get; set; }
    }
}
