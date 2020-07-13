using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class AnswerQuiz : BaseEntity
    {
        public QuestionQuiz QuestionQuiz { get; set; }
        public decimal Punctuation { get; set; }
        public string Description { get; set; }
        public bool Right { get; set; }
    }
}
