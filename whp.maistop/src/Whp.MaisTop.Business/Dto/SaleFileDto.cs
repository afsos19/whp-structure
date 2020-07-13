using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class SaleFileDto
    {
        
        public int Id { get; set; }
        public UserDto User { get; set; }
        public NetworkDto Network { get; set; }
        public FileStatusDto FileStatus { get; set; }
        public string FileName { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
