using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class GroupProduct : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
