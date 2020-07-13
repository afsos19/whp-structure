using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Total { get; set; }
        public decimal OrderValue { get; set; }
        public decimal Freight { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public string Description { get; set; }
        public string AccessToken { get; set; }
        public string Login { get; set; }
        public long ExternalOrderId { get; set; }
        public string AuthorizationCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReversedAt { get; set; }
        public bool Activated { get; set; }
    }
}
