using AutoMapper;
using NLog;
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
    public class PunctuationService : IPunctuationService
    {

        private readonly IExpiredConfigurationPointsRepository _expiredConfigurationPointsRepository;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;
        private readonly ILogger _logger;
        private readonly IUserPunctuationReservedRepository _userPunctuationReservedRepository;

        public PunctuationService(IUserPunctuationReservedRepository userPunctuationReservedRepository, IExpiredConfigurationPointsRepository expiredConfigurationPointsRepository, IUserPunctuationRepository userPunctuationRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IUserPunctuationSourceRepository userPunctuationSourceRepository, ILogger logger)
        {
            _expiredConfigurationPointsRepository = expiredConfigurationPointsRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
            _logger = logger;
            _userPunctuationReservedRepository = userPunctuationReservedRepository;
        }

        public async Task<IEnumerable<UserPunctuation>> GetUserCredits(int userId, int currentMonth, int currentYear) => await _userPunctuationRepository.CustomFind(x => x.CurrentMonth == currentMonth && x.CurrentYear == currentYear && x.User.Id == userId && x.Punctuation > 0);

        public async Task<ExtractDto> GetUserExtract(int userId)
        {
            var userPunctuation = await _userPunctuationRepository.CustomFind(x => x.User.Id == userId, x => x.User, x => x.UserPunctuationSource);
            var toExpire = await PointsToExpire(userId);

            var balance = userPunctuation.Sum(x => x.Punctuation);
            var userPunctuationReserveds = userPunctuation.Any() ? await _userPunctuationReservedRepository.CustomFind(x => x.User.Id == userPunctuation.First().User.Id) : new List<UserPunctuationReserved>();
            balance = userPunctuationReserveds.Any() ? balance - userPunctuationReserveds.Sum(x => x.Punctuation) : balance;

            return new ExtractDto
            {
                Credit = userPunctuation.Where(x => x.OperationType == 'C').Sum(x => x.Punctuation) > 0 ? userPunctuation.Where(x => x.OperationType == 'C').Sum(x => x.Punctuation) : 0,
                Debit = userPunctuation.Where(x => x.OperationType == 'D').Sum(x => x.Punctuation) != 0 ? userPunctuation.Where(x => x.OperationType == 'D').Sum(x => x.Punctuation) : 0,
                Balance = balance > 0 ? balance : 0,
                NextAmoutToExpire = toExpire > 0 ? toExpire : 0,
                DateLastCredit = userPunctuation.Any() ? userPunctuation.Where(x => x.OperationType == 'C').OrderBy(x => x.Id).Last().CreatedAt : DateTime.MinValue,
                ExpiredPunctuation = userPunctuation.Where(x => x.OperationType == 'D' && x.UserPunctuationSource.Id == 5).Sum(x => x.Punctuation) != 0 ? -1 * userPunctuation.Where(x => x.OperationType == 'D' && x.UserPunctuationSource.Id == 5).Sum(x => x.Punctuation) : 0
            };

        }

        public async Task<decimal> PointsToExpire(int userId)
        {

            var dateConfigured = (await _expiredConfigurationPointsRepository.GetAll()).First().ExpiresIn;

            var month = DateTime.Now.AddHours(1).Month;
            var year = DateTime.Now.AddHours(1).Year;
            var dateCreditLimit = DateTime.Now;
            var dateToExpire = new DateTime(year, month, dateConfigured.Day);

            if (dateToExpire > DateTime.Now.AddHours(1))
                dateCreditLimit = dateToExpire.AddYears(-1);
            else
                dateCreditLimit = dateToExpire.AddYears(-1).AddMonths(1);

            var credit = (await _userPunctuationRepository.CustomFind(x => x.OperationType == 'C' && x.User.Id == userId && x.CreatedAt < dateCreditLimit)).Sum(x => x.Punctuation);

            var debit = (await _userPunctuationRepository.CustomFind(x => x.OperationType == 'D' && x.User.Id == userId)).Sum(x => x.Punctuation);

            _logger.Info($"Expiração de pontos do usuario com id {userId} - total de pontos a expirar {(credit - (-1 * debit))}");

            return credit - (-1 * debit);
        }

        public async Task PointsExpiration()
        {
            var dateConfigured = (await _expiredConfigurationPointsRepository.GetAll()).First().ExpiresIn;

            if (dateConfigured.Day == DateTime.Now.Day)
            {
                var users = await _userRepository.GetAll();

                foreach (var item in users)
                {
                    var balanceToExpire = await PointsToExpire(item.Id);

                    if (balanceToExpire > 0)
                    {
                        _userPunctuationRepository.Save(new UserPunctuation
                        {
                            CreatedAt = DateTime.Now,
                            CurrentMonth = DateTime.Now.Month,
                            CurrentYear = DateTime.Now.Year,
                            Description = "EXPIRACAO DE PONTOS",
                            OperationType = 'D',
                            Punctuation = -1 * balanceToExpire,
                            ReferenceEntityId = 0,
                            User = item,
                            UserPunctuationSource = await _userPunctuationSourceRepository.GetById(5)
                        });

                        await _unitOfWork.CommitAsync();
                    }
                }
            }

        }

        public async Task BirthdayProcessing()
        {
            var users = await _userRepository.CustomFind(x => x.BithDate.Value.Month == DateTime.Now.Month && x.BithDate.Value.Day == DateTime.Now.Day);
            var birthdaySource = await _userPunctuationSourceRepository.GetById(15);
            var punctuationList = new List<UserPunctuation>();

            foreach (var item in users)
            {
                punctuationList.Add(new UserPunctuation
                {
                    CreatedAt = DateTime.Now,
                    CurrentMonth = DateTime.Now.Month,
                    CurrentYear = DateTime.Now.Year,
                    Description = "PONTUAÇÃO POR ANIVERSÁRIO. PARABÉNS!",
                    OperationType = 'C',
                    Punctuation = 5,
                    ReferenceEntityId = 0,
                    User = item,
                    UserPunctuationSource = birthdaySource
                });
            }

            _userPunctuationRepository.SaveMany(punctuationList);

            await _unitOfWork.CommitAsync();

        }
    }
}
