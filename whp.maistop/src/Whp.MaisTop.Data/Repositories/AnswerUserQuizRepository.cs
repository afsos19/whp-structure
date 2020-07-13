using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class AnswerUserQuizRepository : Repository<AnswerUserQuiz>, IAnswerUserQuizRepository
    {
        public AnswerUserQuizRepository(WhpDbContext dbContext) : base(dbContext)
        {
        }
    }
}
