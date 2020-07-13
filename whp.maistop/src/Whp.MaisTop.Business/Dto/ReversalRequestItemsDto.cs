using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ReversalRequestItemsDto
    {
        [Required]
        public long ExternalOrderId { get; set; }
        [Required]
        public int CodeItem { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public decimal UnitValue { get; set; }
        [Required]
        public decimal TotalValue { get; set; }
        [Required]
        public int Ammout { get; set; }
    }
}
