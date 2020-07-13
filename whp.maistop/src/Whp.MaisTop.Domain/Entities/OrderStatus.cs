using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Method { get; set; }
    }
}
