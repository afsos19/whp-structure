using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Producer : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
