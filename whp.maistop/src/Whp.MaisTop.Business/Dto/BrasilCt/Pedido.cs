using System;
using System.Collections.Generic;

namespace Whp.MaisTop.Business.Dto.BrasilCt
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
        public DateTime forecastDate { get; set; }
        public List<orderItens> OrderItens { get; set; }

       public Pedido()
       {
           accessToken = String.Empty;
           login = String.Empty;
           orderId = 0;
           parentOrderId = String.Empty;
           totalPoints = 0;
           totalOrder = 0;
           shippingValue = 0;
           conversionRate = 0;
           authorizationCode = String.Empty;
           OrderItens = null;
           forecastDate = DateTime.MinValue;
       }
    }
}
