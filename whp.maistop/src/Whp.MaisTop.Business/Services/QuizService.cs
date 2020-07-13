using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IRightAnswerRepository _rightAnswerRepository;
        private readonly IAnswerQuizRepository _answerQuizRepository;
        private readonly IAnswerUserQuizRepository _answerUserQuizRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IRightAnswerRepository rightAnswerRepository,
            IQuizRepository quizRepository, 
            IAnswerQuizRepository answerQuizRepository, 
            IAnswerUserQuizRepository answerUserQuizRepository, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork)
        {
            _rightAnswerRepository = rightAnswerRepository;
            _quizRepository = quizRepository;
            _answerQuizRepository = answerQuizRepository;
            _answerUserQuizRepository = answerUserQuizRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetCurrentQuiz(int userId, int network, int office, int shop)
        {
            return await _quizRepository.GetCurrentQuiz(userId, network, office, shop);
        }

        public async Task<(IEnumerable<QuestionQuiz> Question, IEnumerable<AnswerQuiz> Answer)> GetActivatedQuiz()
        {
            return await _quizRepository.GetActivatedQuiz();
        }

        public async Task<bool> SaveQuiz(IEnumerable<AnswerUserQuizDto> answerUserQuizzes, bool RightAnswers, int userId)
        {
            var AnswerUserQuizDomain = new List<AnswerUserQuiz>();
            var user = await _userRepository.GetById(userId);

            foreach (var item in answerUserQuizzes)
            {
                AnswerUserQuizDomain.Add(new AnswerUserQuiz
                {
                    AnswerDescription = !string.IsNullOrEmpty(item.AnswerDescription) ? item.AnswerDescription : "",
                    AnswerQuiz = await _answerQuizRepository.GetById(item.AnswerQuizId),
                    CreatedAt = DateTime.Now,
                    User = user
                });
            }

            _answerUserQuizRepository.SaveMany(AnswerUserQuizDomain);
            bool result = await _unitOfWork.CommitAsync();
            if (RightAnswers == true)
            {
                await SaveRightAnswer(answerUserQuizzes, user);
            }
            return result;

        }

        public async Task<bool> SaveRightAnswer(IEnumerable<AnswerUserQuizDto> answerUserQuizzes, User user)
        {
            var RightAnswerDomain = new List<RightAnswer>();

            foreach (var item in answerUserQuizzes)
            {
                RightAnswerDomain.Add(new RightAnswer
                {
                    AnswerDescription = !string.IsNullOrEmpty(item.AnswerDescription) ? item.AnswerDescription : "",
                    AnswerQuiz = (await _answerQuizRepository.CustomFind(x => x.Id == item.AnswerQuizId)).FirstOrDefault(),
                    CreatedAt = DateTime.Now,
                    User = user
                });
            }

            var result = await _rightAnswerRepository.SaveAnswer(RightAnswerDomain);
            return (result.sucesso);

        }
    }
}
