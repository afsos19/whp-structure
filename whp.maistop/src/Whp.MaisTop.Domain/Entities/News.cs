using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Thumb { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
        public int Ordernation { get; set; }
    }
}
