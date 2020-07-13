using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ExtractDto
    {
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal ExpiredPunctuation { get; set; }
        public decimal Balance { get; set; }
        public decimal NextAmoutToExpire { get; set; }
        public DateTime DateLastCredit { get; set; }
    }
}
