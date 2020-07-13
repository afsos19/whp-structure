using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class ExpiredConfigurationPoints : BaseEntity
    {
        public DateTime ExpiresIn { get; set; }
        public bool Activated { get; set; }
    }
}
