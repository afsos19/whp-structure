using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class RescueResponseDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
