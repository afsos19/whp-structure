using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class OccurrenceMessageDto 
    {
        public int Id { get; set; }
        public OccurrenceDto Occurrence { get; set; }
        public UserDto User { get; set; }
        public OccurrenceMessageTypeDto OccurrenceMessageType { get; set; }
        [Required]
        public int OccurrenceMessageTypeId { get; set; }
        [Required]
        public string Message { get; set; }
        public string File { get; set; }
        public bool Internal { get; set; }
        public bool Catalog { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
