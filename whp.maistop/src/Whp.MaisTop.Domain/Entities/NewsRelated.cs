using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class NewsRelated : BaseEntity
    {
        public News News { get; set; }
        public Office Office { get; set; }
        public Network Network { get; set; }
    }
}
