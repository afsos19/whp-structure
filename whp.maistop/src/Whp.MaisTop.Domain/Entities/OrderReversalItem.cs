using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OrderReversalItem : BaseEntity
    {

        public OrderItem OrderItem { get; set; }
        public long ExternalOrderId { get; set; }
        public int CodeItem { get; set; }
        public string Reason { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public int Ammout { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
