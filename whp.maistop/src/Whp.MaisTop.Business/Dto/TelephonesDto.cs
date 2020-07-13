using System;
using System.Collections.Generic;
using System.Text;

namespace Ssg.MaisSamsung.Business.Dto
{
    public class TelephonesDto
    {
        public int PhoneTypeId { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public int ExtensionNumber { get; set; }
        public bool Active { get; set; }
    }
}
