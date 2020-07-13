using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int MaxQuestionAmout { get; set; }
    }
}
