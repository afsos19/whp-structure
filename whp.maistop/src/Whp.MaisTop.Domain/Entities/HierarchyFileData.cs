using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class HierarchyFileData : BaseEntity
    {
        public HierarchyFile HierarchyFile { get; set; }
        public string Resale { get; set; }
        public string ShopCode { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Office { get; set; }
        public string Off { get; set; }
        public string Spreedsheet { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
