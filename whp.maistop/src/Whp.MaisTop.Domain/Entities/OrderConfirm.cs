using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OrderConfirm : BaseOrder
    {
        public Order Order { get; set; }
    }
}
