using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class QuestionQuizTypeRepository : Repository<QuestionQuizType>, IQuestionQuizTypeRepository
    {
        public QuestionQuizTypeRepository(WhpDbContext dbContext) : base(dbContext)
        {
        }
    }
}
