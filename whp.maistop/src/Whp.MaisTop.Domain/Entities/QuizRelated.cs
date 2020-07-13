using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class QuizRelated : BaseEntity
    {
        public Quiz Quiz { get; set; }
        public Office Office { get; set; }
        public Network Network { get; set; }
    }
}
