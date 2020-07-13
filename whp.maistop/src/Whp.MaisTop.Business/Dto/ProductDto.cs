using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public ProducerDto Producer { get; set; }
        public CategoryProductDto CategoryProduct { get; set; }
        [Required]
        public int ProducerId { get; set; }
        [Required]
        public int CategoryProductId { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public string Name { get; set; }
        public string Ean { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
