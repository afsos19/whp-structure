using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserAccessCodeExpiration : BaseEntity
    {
        public User User { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
