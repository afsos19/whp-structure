using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Order Order { get; set; }
        public long ExternalOrderId { get; set; }
        public int CodeItem { get; set; }
        public string ProductName { get; set; }
        public string Partner { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public int Ammout { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
        public DateTime ForecastDate { get; set; }
    }
}
