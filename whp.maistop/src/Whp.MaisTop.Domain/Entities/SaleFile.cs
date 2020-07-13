using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class SaleFile : BaseEntity
    {
        public User User { get; set; }
        public Network Network { get; set; }
        public FileStatus FileStatus { get; set; }
        public string FileName { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
