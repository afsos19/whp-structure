using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Producer Producer { get; set; }
        public CategoryProduct CategoryProduct { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Ean { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
