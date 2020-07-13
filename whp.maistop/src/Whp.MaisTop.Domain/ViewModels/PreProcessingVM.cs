using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.ViewModels
{
    public class PreProcessingVM
    {
        public string Network { get; set; }
        public string Producer { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public string Sku { get; set; }
        public decimal Participant { get; set; }
        public decimal SuperTop { get; set; }
    }
}
