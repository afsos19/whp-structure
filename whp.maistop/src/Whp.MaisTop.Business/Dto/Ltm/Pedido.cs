using System;
using System.Collections.Generic;

namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class Pedido
    {
        public string accessToken { get; set; }
        public string login { get; set; }
        public int orderId { get; set; }
        public string parentOrderId { get; set; }
        public decimal totalPoints { get; set; }
        public decimal totalOrder { get; set; }
        public decimal shippingValue { get; set; }
        public decimal conversionRate { get; set; }
        public string authorizationCode { get; set; }
        public List<orderItems> OrderItems { get; set; }
    }
}
