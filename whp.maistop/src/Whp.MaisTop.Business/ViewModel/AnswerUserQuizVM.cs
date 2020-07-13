using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Dto;

namespace Whp.MaisTop.Business.ViewModel
{
    public class AnswerUserQuizVM
    {
        public IEnumerable<AnswerUserQuizDto> AnswerUserQuizDto { get; set; }
        public bool RightAnswers { get; set; }
    }
}
