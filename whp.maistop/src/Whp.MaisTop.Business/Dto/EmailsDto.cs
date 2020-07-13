using System;
using System.Collections.Generic;
using System.Text;

namespace Ssg.MaisSamsung.Business.Dto
{
    public class EmailsDto
    {
        public int EmailTypeId { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
    }
}
