using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class RescueRequestDto
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public long ExternalOrderId { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public decimal OrderValue { get; set; }
        public decimal Freight { get; set; }
        public decimal ConversionRate { get; set; }
        public DateTime? ForecastDate { get; set; }
        [Required]
        public IEnumerable<RescueRequestItemsDto> RescueRequestItems { get; set; }
    }
}
