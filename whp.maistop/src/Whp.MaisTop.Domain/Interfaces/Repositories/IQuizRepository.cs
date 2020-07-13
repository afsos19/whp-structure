using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<(IEnumerable<QuestionQuiz> Question,  IEnumerable<AnswerUserQuiz> AnswerUser)> GetUserAnswers(int userId, int network, int office, int shop);
        Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetActivatedQuiz();
        Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetCurrentQuiz(int userId, int network, int office, int shop);
    }
}
