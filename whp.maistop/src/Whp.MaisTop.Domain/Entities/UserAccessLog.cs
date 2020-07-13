using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserAccessLog : BaseEntity
    {
        public User User { get; set; }
        public string Ip { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Device { get; set; }
        public string Description { get; set; }
    }
}
