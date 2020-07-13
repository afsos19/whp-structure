using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class FilterDto
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int Network { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}
