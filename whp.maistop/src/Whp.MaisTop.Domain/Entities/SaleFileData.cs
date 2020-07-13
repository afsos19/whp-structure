using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class SaleFileData : BaseEntity
    {
        public SaleFile SaleFile { get; set; }
        public int Product { get; set; }
        public SaleFileSkuStatus SaleFileSkuStatus { get; set; }
        public string Resale { get; set; }
        public string ShopCode { get; set; }
        public string EanCode { get; set; }
        public string Cnpj { get; set; }
        public string CpfSalesman { get; set; }
        public string NameSalesman { get; set; }
        public string Category { get; set; }
        public string ProductDescription { get; set; }
        public int Amount { get; set; }
        public DateTime SaleDate { get; set; }
        public string RequestNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
