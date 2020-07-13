using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Network Network { get; set; }
        public Shop Shop { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public int Amout { get; set; }
        public DateTime SaleDate { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Punctuation { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
        public bool Processed { get; set; }
    }
}
