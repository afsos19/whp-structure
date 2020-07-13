using System;
using System.Collections.Generic;
using System.Text;

namespace Ssg.MaisSamsung.Business.Dto
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
        public int AddressType { get; set; }
        public StateDto State { get; set; }
        public int AddressTypeId { get; set; }
    }
}
