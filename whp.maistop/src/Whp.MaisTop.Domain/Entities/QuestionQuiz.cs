using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class QuestionQuiz : BaseEntity
    {
        public Quiz Quiz { get; set; }
        public QuestionQuizType QuestionQuizType { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
