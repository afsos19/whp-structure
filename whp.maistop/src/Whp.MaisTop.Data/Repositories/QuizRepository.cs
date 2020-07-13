using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public QuizRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetCurrentQuiz(int userId, int network, int office, int shop)
        {
            //var QueryQuestion = _dbContext.QuestionQuiz as IQueryable<QuestionQuiz>;
            //var QueryAnswer = _dbContext.AnswerQuiz as IQueryable<AnswerQuiz>;
            //var QueryAnserUser = _dbContext.AnswerUserQuiz as IQueryable<AnswerUserQuiz>;

            //var quizId = QueryAnserUser.Include(x => x.AnswerQuiz).ThenInclude(x => x.QuestionQuiz).ThenInclude(x => x.Quiz).Where(x => x.User.Id == userId).Select(q => q.AnswerQuiz.QuestionQuiz.Quiz.Id);

            //QueryQuestion = QueryQuestion.Include(x => x.Quiz).Include(x => x.QuestionQuizType).Where(x => x.Quiz.Activated && !quizId.Contains(x.Quiz.Id));
            //QueryAnswer = QueryAnswer.Where(x => QueryQuestion.Select(q => q.Id).Contains(x.QuestionQuiz.Id) && !quizId.Contains(x.QuestionQuiz.Quiz.Id));


            //return (await QueryQuestion.ToListAsync(), await QueryAnswer.ToListAsync());

            var QueryQuestion = _dbContext.QuestionQuiz as IQueryable<QuestionQuiz>;
            var QueryAnswer = _dbContext.AnswerQuiz as IQueryable<AnswerQuiz>;
            var QueryAnserUser = _dbContext.AnswerUserQuiz as IQueryable<AnswerUserQuiz>;
            var QueryQuizRelated = _dbContext.QuizRelated as IQueryable<QuizRelated>;
            var QueryQuizShopRelated = _dbContext.QuizShopRelated as IQueryable<QuizShopRelated>;

            List<int> quizListResult = new List<int>();

            var quizShopRelatedList = await QueryQuizShopRelated
                .Include(x => x.QuizRelated)
                .Include(x => x.QuizRelated.Network)
                .Include(x => x.QuizRelated.Office)
                .Include(x => x.Shop)
                .Include(x => x.QuizRelated.Quiz)
                .Where(x => x.QuizRelated.Quiz.Activated).ToListAsync();

            if (quizShopRelatedList.Any())
            {
                var filtered = quizShopRelatedList.Where(x => x.QuizRelated.Network.Id == network &&
                                                              x.QuizRelated.Office.Id == office &&
                                                              x.QuizRelated.Quiz.Activated &&
                                                              x.Shop.Id == shop).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.QuizRelated.Quiz).Select(x => x.Key.Id).ToList());
            }

            var quizRelated = await QueryQuizRelated
                .Include(x => x.Network)
                .Include(x => x.Office)
                .Include(x => x.Quiz)
                .Where(x => x.Quiz.Activated).ToListAsync();

            if (quizRelated.Any())
            {
                var filtered = quizRelated.Where(x => x.Network.Id == network &&
                                                      x.Office.Id == office &&
                                                     (quizShopRelatedList.Any() ?
                                                     !quizShopRelatedList.GroupBy(g => g.QuizRelated.Quiz.Id).Select(g => g.Key).ToList().Contains(x.Quiz.Id)
                                                     : 1 == 1)).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.Quiz).Select(x => x.Key.Id).ToList());
            }

            var quizAnserUser = QueryAnserUser.Include(x => x.AnswerQuiz)
                                       .ThenInclude(x => x.QuestionQuiz)
                                       .ThenInclude(x => x.Quiz)
                                       .Where(x => x.User.Id == userId)
                                       .Select(q => q.AnswerQuiz.QuestionQuiz.Quiz.Id);

            QueryQuestion = QueryQuestion.Include(x => x.Quiz)
                                         .Include(x => x.QuestionQuizType)
                                         .Where(x => x.Quiz.Activated && quizListResult.Contains(x.Quiz.Id) && !quizAnserUser.Contains(x.Quiz.Id));

            QueryAnswer = QueryAnswer.Where(x => QueryQuestion.Select(q => q.Id).Contains(x.QuestionQuiz.Id) &&
                                                 quizListResult.Contains(x.QuestionQuiz.Quiz.Id) &&
                                                !quizAnserUser.Contains(x.QuestionQuiz.Quiz.Id));

            return (await QueryQuestion.ToListAsync(), await QueryAnswer.ToListAsync());

        }


        public async Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerUserQuiz> AnswerUser)> GetUserAnswers(int userId, int network, int office, int shop)
        {
            //var QueryQuestion = _dbContext.QuestionQuiz as IQueryable<QuestionQuiz>;
            //var QueryAnswer = _dbContext.AnswerQuiz as IQueryable<AnswerQuiz>;
            //var QueryAnserUser = _dbContext.AnswerUserQuiz as IQueryable<AnswerUserQuiz>;

            //var quizId = QueryAnserUser.Include(x => x.AnswerQuiz).ThenInclude(x => x.QuestionQuiz).ThenInclude(x => x.Quiz).Where(x => x.User.Id == userId).Select(q => q.AnswerQuiz.QuestionQuiz.Quiz.Id);

            //QueryQuestion = QueryQuestion.Include(x => x.Quiz).Include(x => x.QuestionQuizType).Where(x => x.Quiz.Activated && !quizId.Contains(x.Quiz.Id));
            //QueryAnswer = QueryAnswer.Where(x => QueryQuestion.Select(q => q.Id).Contains(x.QuestionQuiz.Id) && !quizId.Contains(x.QuestionQuiz.Quiz.Id));


            //return (await QueryQuestion.ToListAsync(), await QueryAnswer.ToListAsync());

            var QueryQuestion = _dbContext.QuestionQuiz as IQueryable<QuestionQuiz>;
            var QueryAnswer = _dbContext.AnswerQuiz as IQueryable<AnswerQuiz>;
            var QueryAnserUser = _dbContext.AnswerUserQuiz as IQueryable<AnswerUserQuiz>;
            var QueryQuizRelated = _dbContext.QuizRelated as IQueryable<QuizRelated>;
            var QueryQuizShopRelated = _dbContext.QuizShopRelated as IQueryable<QuizShopRelated>;

            List<int> quizListResult = new List<int>();

            var quizShopRelatedList = await QueryQuizShopRelated
                .Include(x => x.QuizRelated)
                .Include(x => x.QuizRelated.Network)
                .Include(x => x.QuizRelated.Office)
                .Include(x => x.Shop)
                .Include(x => x.QuizRelated.Quiz)
                .Where(x => x.QuizRelated.Quiz.Activated).ToListAsync();

            if (quizShopRelatedList.Any())
            {
                var filtered = quizShopRelatedList.Where(x => x.QuizRelated.Network.Id == network &&
                                                              x.QuizRelated.Office.Id == office &&
                                                              x.QuizRelated.Quiz.Activated &&
                                                              x.Shop.Id == shop).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.QuizRelated.Quiz).Select(x => x.Key.Id).ToList());
            }

            var quizRelated = await QueryQuizRelated
                .Include(x => x.Network)
                .Include(x => x.Office)
                .Include(x => x.Quiz)
                .Where(x => x.Quiz.Activated).ToListAsync();

            if (quizRelated.Any())
            {
                var filtered = quizRelated.Where(x => x.Network.Id == network &&
                                                      x.Office.Id == office &&
                                                     (quizShopRelatedList.Any() ?
                                                     !quizShopRelatedList.GroupBy(g => g.QuizRelated.Quiz.Id).Select(g => g.Key).ToList().Contains(x.Quiz.Id)
                                                     : 1 == 1)).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.Quiz).Select(x => x.Key.Id).ToList());
            }

            var quizAnserUser = QueryAnserUser
                                       .Include(x => x.AnswerQuiz)
                                       .Where(x => x.User.Id == userId);

            QueryQuestion = QueryQuestion.Include(x => x.Quiz)
                                         .Include(x => x.QuestionQuizType)
                                         .Where(x => x.Quiz.Activated &&  quizListResult.Contains(x.Quiz.Id) );

            QueryAnswer = QueryAnswer.Where(x => QueryQuestion.Select(q => q.Id).Contains(x.QuestionQuiz.Id) &&
                                                 quizListResult.Contains(x.QuestionQuiz.Quiz.Id));

            return (await QueryQuestion.ToListAsync(), await quizAnserUser.ToListAsync());

        }

        public async Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetActivatedQuiz()
        {
            var QueryQuestion = _dbContext.QuestionQuiz as IQueryable<QuestionQuiz>;
            var QueryAnswer = _dbContext.AnswerQuiz as IQueryable<AnswerQuiz>;
            var QueryQuizRelated = _dbContext.QuizRelated as IQueryable<QuizRelated>;
            var QueryQuizShopRelated = _dbContext.QuizShopRelated as IQueryable<QuizShopRelated>;

            List<int> quizListResult = new List<int>();

            var quizShopRelatedList = await QueryQuizShopRelated
                .Include(x => x.QuizRelated)
                .Include(x => x.QuizRelated.Quiz)
                .Where(x => x.QuizRelated.Quiz.Activated).ToListAsync();

            if (quizShopRelatedList.Any())
            {
                var filtered = quizShopRelatedList.Where(x =>
                                                              x.QuizRelated.Quiz.Activated ).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.QuizRelated.Quiz).Select(x => x.Key.Id).ToList());
            }

            var quizRelated = await QueryQuizRelated
                .Include(x => x.Quiz)
                .Where(x => x.Quiz.Activated).ToListAsync();

            if (quizRelated.Any())
            {
                var filtered = quizRelated.Where(x =>
                                                     (quizShopRelatedList.Any() ?
                                                     !quizShopRelatedList.GroupBy(g => g.QuizRelated.Quiz.Id).Select(g => g.Key).ToList().Contains(x.Quiz.Id)
                                                     : 1 == 1)).ToList();
                if (filtered.Any())
                    quizListResult.AddRange(filtered.GroupBy(x => x.Quiz).Select(x => x.Key.Id).ToList());
            }
            

            QueryQuestion = QueryQuestion.Include(x => x.Quiz)
                                         .Include(x => x.QuestionQuizType)
                                         .Where(x => x.Quiz.Activated && quizListResult.Contains(x.Quiz.Id));

            QueryAnswer = QueryAnswer.Where(x =>x.Id != 0);

            return (await QueryQuestion.ToListAsync(), await QueryAnswer.ToListAsync());

        }
    }
}
