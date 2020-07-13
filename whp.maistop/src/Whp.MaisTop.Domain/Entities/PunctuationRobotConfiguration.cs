using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class PunctuationRobotConfiguration : BaseEntity
    {
        public int IdRobotPunctuation { get; set; }
        public int IdMechanicsRobotPunctuation { get; set; }
        public bool Activated { get; set; }
    }
}
