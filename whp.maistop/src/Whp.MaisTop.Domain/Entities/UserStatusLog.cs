using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class UserStatusLog : BaseEntity
    {
        public User User { get; set; }
        public UserStatus UserStatusTo { get; set; }
        public UserStatus UserStatusFrom { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
