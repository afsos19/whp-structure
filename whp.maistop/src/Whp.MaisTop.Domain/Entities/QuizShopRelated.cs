using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class QuizShopRelated : BaseEntity
    {
        public QuizRelated QuizRelated { get; set; }
        public Shop Shop { get; set; }
    }
}
