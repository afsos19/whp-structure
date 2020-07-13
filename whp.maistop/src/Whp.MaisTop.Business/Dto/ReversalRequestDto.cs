using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ReversalRequestDto
    {
        [Required]
        public string Cpf { get; set; }
        [Required]
        public long ExternalOrderId { get; set; }
        [Required]
        public IEnumerable<ReversalRequestItemsDto> ReversalRequestItems { get; set; }
    }
}
