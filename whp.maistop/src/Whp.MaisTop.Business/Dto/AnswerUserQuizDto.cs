using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class AnswerUserQuizDto 
    {
        public int AnswerQuizId { get; set; }
        public string AnswerDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
