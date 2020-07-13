using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class SaleFileDataDto 
    {
        
        public int Id { get; set; }
        public SaleFileDto SaleFile { get; set; }
        [Required]
        public int Product { get; set; }
        public SaleFileSkuStatusDto SaleFileSkuStatus { get; set; }
        public string Resale { get; set; }
        public string ShopCode { get; set; }
        public string Cnpj { get; set; }
        public string CpfSalesman { get; set; }
        public string NameSalesman { get; set; }
        public string Category { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public int Amount { get; set; }
        public DateTime SaleDate { get; set; }
        public string RequestNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool NotExisting { get; set; }
    }
}
