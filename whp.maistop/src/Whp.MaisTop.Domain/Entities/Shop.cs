using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Shop : BaseEntity
    {
        public Network Network { get; set; }
        public string ShopCode { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
