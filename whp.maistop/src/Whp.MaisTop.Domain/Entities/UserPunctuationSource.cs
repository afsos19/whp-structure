using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserPunctuationSource : BaseEntity
    {
        public PunctuationRobotConfiguration PunctuationRobotConfiguration { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
