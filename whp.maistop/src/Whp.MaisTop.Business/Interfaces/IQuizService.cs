using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IQuizService
    {
        Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetCurrentQuiz(int userId, int network, int office, int shop);
        Task<bool> SaveQuiz(IEnumerable<AnswerUserQuizDto> answerUserQuizzes, bool RightAnswers, int userId);
        Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetActivatedQuiz();
        Task<bool> SaveRightAnswer(IEnumerable<AnswerUserQuizDto> answerUserQuizzes, User user);
    }
}
