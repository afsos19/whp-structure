
using System;
namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class orderItems
    {
        public string codeItem { get; set; }
        public string productName { get; set; }
        public string supplier { get; set; }
        public string department { get; set; }
        public string category { get; set; }
        public decimal unitPointsValue { get; set; }
        public decimal unitCurrencyValue { get; set; }
        public int quantity { get; set; }
        public string forecastDate { get; set; }
        
    }
}
