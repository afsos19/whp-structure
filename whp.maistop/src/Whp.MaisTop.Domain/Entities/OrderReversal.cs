using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OrderReversal : BaseOrder
    {
        public Order Order { get; set; }
        public decimal Value { get; set; }
        public string Message { get; set; }
    }
}
