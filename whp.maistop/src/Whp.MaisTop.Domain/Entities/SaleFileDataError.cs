using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class SaleFileDataError : BaseEntity
    {
        public SaleFile SaleFile { get; set; }
        public int Line { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
