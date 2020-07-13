using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Order : BaseOrder
    {
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Total { get; set; }
        public decimal OrderValue { get; set; }
        public decimal Freight { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public DateTime? ReversedAt { get; set; }
    }
}
