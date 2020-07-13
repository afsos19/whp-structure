using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class orderItens
    {

        public string codeItem { get; set; }
        public string productName { get; set; }
        public string supplier { get; set; }
        public string department { get; set; }
        public string category { get; set; }
        public decimal unitPointsValue { get; set; }
        public decimal unitCurrencyValue { get; set; }
        public int quantity { get; set; }
        

       public orderItens()
       {
           codeItem = String.Empty;
           productName = String.Empty;
           supplier = String.Empty;
           department = String.Empty;
           category = String.Empty;
           unitCurrencyValue = 0;
           unitPointsValue = 0;
           quantity = 0;

       }
    }
}
