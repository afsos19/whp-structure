using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class AnswerUserQuiz : BaseEntity
    {
        public User User { get; set; }
        public AnswerQuiz AnswerQuiz { get; set; }
        public string AnswerDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
