using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserAccessCodeInvite : BaseEntity
    {
        public User User { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
