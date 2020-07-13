using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.ViewModels
{
    public class SKUClassificationVM
    {
        public string ProductDescription { get; set; }
        public string Resale { get; set; }
        public string StatusSKU { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int Network { get; set; }
    }
}
