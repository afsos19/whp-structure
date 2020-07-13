using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class addresses
    {
        public string street { get; set; }
        public string zipcode { get; set; }
        public string number { get; set; }
        public string department { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public addresses()
       {
           street = String.Empty;
           zipcode = String.Empty;
           number = String.Empty;

       }
    }
}
