using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Dto;

namespace Whp.MaisTop.Business.Dto
{
    public class HierarchyFileDto 
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
