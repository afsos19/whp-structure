using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class GetProductDto
    {
        public int CategoryProductId { get; set; }
        public int ProducerId { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int NetworkId { get; set; }
    }
}
