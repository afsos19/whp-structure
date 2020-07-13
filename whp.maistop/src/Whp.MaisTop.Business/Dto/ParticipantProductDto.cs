using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ParticipantProductDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
        public NetworkDto Network { get; set; }
        public int NetworkId { get; set; }
        public decimal Punctuation { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
