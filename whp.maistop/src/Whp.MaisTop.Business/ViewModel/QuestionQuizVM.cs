using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.ViewModel
{
    public class QuestionQuizVM
    {
        public IEnumerable<Quiz> Quiz { get; set; }
        public IEnumerable<QuestionQuizType> QuestionQuizType { get; set; }
        public IEnumerable<AnswerUserQuiz> AnswerUserQuiz { get; set; }
        public IEnumerable<AnswerQuiz> AnswerQuiz { get; set; }
    }
}
