using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class OccurrenceDto
    {
        public int Id { get; set; }
        public int OccurrenceStatusId { get; set; }
        public OccurrenceMessageDto OccurrenceMessage { get; set; }
        public OccurrenceStatusDto OccurrenceStatus { get; set; }
        [Required]
        public int OccurrenceSubjectId { get; set; }
        public OccurrenceSubjectDto OccurrenceSubject { get; set; }
        [Required]
        public int OccurrenceContactTypeId { get; set; }
        public OccurrenceContactTypeDto OccurrenceContactType { get; set; }
        public UserDto User { get; set; }
        public string Code { get; set; }
        public string File { get; set; }
        public string Cpf { get; set; }
        public string BrazilCTCall { get; set; }
        public bool Critical { get; set; }
        public bool Participant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? RedirectedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool Activated { get; set; }
        public string LastIteration { get; set; }

    }
}
