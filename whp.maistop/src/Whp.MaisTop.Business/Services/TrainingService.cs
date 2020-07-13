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
using Whp.MaisTop.Domain.Entities.Academy;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.Repositories.Academy;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingUserPointsRepository _trainingUserPointsRepository;
        private readonly ITrainingUserRepository _trainingUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(ITrainingRepository trainingRepository, ILogger logger, IUnitOfWork unitOfWork, ITrainingUserPointsRepository trainingUserPointsRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, IShopUserRepository shopUserRepository, IUserPunctuationRepository userPunctuationRepository, IUserPunctuationSourceRepository userPunctuationSourceRepository)
        {
            _trainingRepository = trainingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _trainingUserPointsRepository = trainingUserPointsRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _shopUserRepository = shopUserRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
        }

        public TrainingService(ITrainingRepository trainingRepository, ILogger logger, IUnitOfWork unitOfWork, ITrainingUserPointsRepository trainingUserPointsRepository, ITrainingUserRepository trainingUserRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, IShopUserRepository shopUserRepository, IUserPunctuationRepository userPunctuationRepository, IUserPunctuationSourceRepository userPunctuationSourceRepository)
        {
            _trainingRepository = trainingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _trainingUserPointsRepository = trainingUserPointsRepository;
            _trainingUserRepository = trainingUserRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _shopUserRepository = shopUserRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
        }

        public async Task<MyTeamTrainingDto> GetTrainingTeamSales(int shop, int currentMonth, int currentYear)
        {
            var usersId = (await _shopUserRepository.CustomFind(x => x.Shop.Id == shop, x => x.User)).Select(x => x.User.Id).ToList();

            var trainingCurrent = (await _trainingUserPointsRepository.CustomFind(
            x => usersId.Contains(x.User.Id) &&
            x.TrainingDoneAt.Month == currentMonth &&
            x.TrainingDoneAt.Year == currentYear,
            x => x.User)).OrderBy(x => x.User.Id).ToList();

            if (usersId.Count() == 0 || trainingCurrent.Count() == 0)
                return new MyTeamTrainingDto();

            var trainingCount = trainingCurrent
             .Where(x => x.ResultId == 4)
             .GroupBy(x => new { x.TrainingId, x.User.Id })
             .Select(g =>
                 new
                 {
                     User = g.Key.Id,
                 }).ToList().GroupBy(x => x.User)
                .Select(x =>
                new
                {
                    CountUser = x.Select(s => s.User).Count()
                }).ToList();

            var trainingListGroup = trainingCurrent
             .GroupBy(x => new { x.TrainingStatus, x.User.Id, x.User })
             .Select(g =>
                 new
                 {
                     g.Key.User,
                     g.Key.TrainingStatus,
                 }).ToList();

            var trainingDone = trainingCurrent.Select(x => x.User.Id).GroupBy(x => x).ToList();
            var trainingNotDont = usersId.Count() - trainingDone.Count();
            var trainingOneDone = trainingCount.Where(x => x.CountUser == 1).Count();
            var trainingTwoDone = trainingCount.Where(x => x.CountUser > 1).Count();

            return new MyTeamTrainingDto
            {
                TrainingList = trainingListGroup,
                TrainingNotDone = (trainingNotDont * 100) / usersId.Count(),
                TrainingOneDone = (trainingOneDone * 100) / usersId.Count(),
                TrainingTwoDone = (trainingTwoDone * 100) / usersId.Count()
            };
        }

        public async Task TrainingPunctuationProcesses(User user, string trainingName, int trainingId)
        {

            var lastUserTrainingCompleted = await _trainingUserPointsRepository.CustomFind(x => x.User.Id == user.Id &&
            x.ResultId == (int)TrainingResultEnum.Approved &&
            x.TrainingDoneAt.Month == DateTime.Now.AddMonths(-1).Month &&
            x.TrainingDoneAt.Year == DateTime.Now.AddMonths(-1).Year);

            var lastTraining = await _trainingRepository.CustomFind(x => x.CurrentMonth == DateTime.Now.AddMonths(-1).Month &&
            x.CurrentYear == DateTime.Now.AddMonths(-1).Year && !x.hasTrainingMaterial && x.trainingKind == 2);

            var trainingValidation = lastTraining.Count() >= 2 ? 2 : 1;

            decimal point = 10;

            var lastPoint = (await _userPunctuationRepository.CustomFind(x => x.User.Id == user.Id &&
            x.UserPunctuationSource.Id == (int)UserPunctuationSourceEnum.TrainingSaleman)).OrderBy(x => x.Id);

            if (lastUserTrainingCompleted.Count() >= trainingValidation)
            {

                var lastMonthToApply = lastPoint.Where(x => x.CurrentMonth == DateTime.Now.AddMonths(-1).Month && x.CurrentYear == DateTime.Now.AddMonths(-1).Year).LastOrDefault();
                var currentMonthToApply = lastPoint.Where(x => x.CurrentMonth == DateTime.Now.Month && x.CurrentYear == DateTime.Now.Year).LastOrDefault();

                if (currentMonthToApply != null)
                    point = currentMonthToApply.Punctuation;
                else if (lastMonthToApply != null && lastMonthToApply.Punctuation < 50)
                    point = lastMonthToApply.Punctuation + 10;
                else if (lastMonthToApply != null && lastMonthToApply.Punctuation >= 50)
                    point = 50;

            }

            _logger.Info($" Nova pontuacao inserida no mes {DateTime.Now.Month} de {DateTime.Now.Year} no valor {point} para o usuario com id {user.Id} ");

            _userPunctuationRepository.Save(new UserPunctuation
            {
                CurrentMonth = DateTime.Now.Month,
                CurrentYear = DateTime.Now.Year,
                CreatedAt = DateTime.Now,
                Description = trainingName,
                OperationType = 'C',
                Punctuation = point,
                ReferenceEntityId = trainingId,
                User = user,
                UserPunctuationSource = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.TrainingSaleman)
            });

            await _unitOfWork.CommitAsync();



        }

        public async Task ProcessesAcademyTraining()
        {

            var academyTraining = (await _trainingUserRepository.CustomFind(x =>
            (x.TrainingResult.Id == (int)TrainingResultEnum.Approved || x.TrainingResult.Id == (int)TrainingResultEnum.Disapproved) &&
            x.TrainingDoneAt.Month == DateTime.Now.Month && x.TrainingDoneAt.Year == DateTime.Now.Year,
            x => x.TrainingResult,
            x => x.Training,
            x => x.UserAcademy));

            foreach (var item in academyTraining)
            {

                if (item.UserAcademy != null && item.TrainingResult != null && item.Training != null)
                {
                    var TrainingUserPoints = await _trainingUserPointsRepository.CustomFind(x =>
                    x.User.Cpf.Equals(item.UserAcademy.Login) &&
                    x.ResultId == item.TrainingResult.Id &&
                    x.TrainingId == item.Training.Id);

                    var user = (await _userRepository.CustomFind(x => x.Cpf.Equals(item.UserAcademy.Login))).FirstOrDefault();

                    if (!TrainingUserPoints.Any() && user != null)
                    {
                        _logger.Info($" Novo treinamento inserido chamado {item.Training.Name} para o usuario com id {user.Id} com o resultado {item.TrainingResult.Name} ");

                        var obj = new TrainingUserPoints
                        {
                            Activated = true,
                            CreatedAt = DateTime.Now,
                            Percentage = item.Percentage,
                            Punctuation = item.Punctuation,
                            ResultId = item.TrainingResult.Id,
                            TrainingDescription = item.Training.Name,
                            TrainingDoneAt = item.TrainingDoneAt,
                            TrainingEndedAt = item.Training.TrainingEndedAt,
                            TrainingStartedAt = item.Training.TrainingStartedAt,
                            TrainingId = item.Training.Id,
                            TrainingStatus = item.TrainingResult.Name,
                            User = user
                        };

                        _trainingUserPointsRepository.Save(obj);

                        await _unitOfWork.CommitAsync();

                        if (obj.ResultId == (int)TrainingResultEnum.Approved)
                            await TrainingPunctuationProcesses(obj.User, item.Training.Name, obj.Id);
                    }
                }
                else
                {
                    _logger.Warn($"encontrado inconsistencia no registro {item.Id} de treinamento da academia");
                }

            }

        }

        public async Task ProcessesAcademyTrainingManagers()
        {

            if (DateTime.Now.Day == 25)
            {

                var userPunctuationList = new List<UserPunctuation>();
                var source = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.TrainingManagers);

                var managers = await _shopUserRepository.CustomFind(x =>
                x.User.Office.Id == (int)OfficeEnum.Manager &&
                (x.User.UserStatus.Id == (int)UserStatusEnum.Active || x.User.UserStatus.Id == (int)UserStatusEnum.PasswordExpired),
                x => x.User,
                x => x.Shop);

                var lastTraining = await _trainingRepository.CustomFind(x => x.CurrentMonth == DateTime.Now.AddMonths(-1).Month &&
                x.CurrentYear == DateTime.Now.AddMonths(-1).Year && !x.hasTrainingMaterial && x.trainingKind == 2);

                var trainingValidation = lastTraining.Count() >= 2 ? 1 : 0;

                var trainingUser = await _trainingUserPointsRepository.CustomFind(x =>
                    x.ResultId == (int)TrainingResultEnum.Approved &&
                    x.TrainingEndedAt.Month == DateTime.Now.AddMonths(-1).Month &&
                    x.TrainingEndedAt.Year == DateTime.Now.AddMonths(-1).Year,
                    x => x.User);

                _logger.Info($"processamento pontuacao gerente treinamento - iniciado processamento");

                foreach (var item in managers)
                {

                    var punctuation = await _userPunctuationRepository.CustomFind(x =>
                    x.User.Id == item.User.Id &&
                    x.CurrentMonth == DateTime.Now.AddMonths(-1).Month &&
                    x.CurrentYear == DateTime.Now.AddMonths(-1).Year &&
                    x.UserPunctuationSource.Id == (int)UserPunctuationSourceEnum.TrainingManagers);

                    if (!punctuation.Any())
                    {
                        var shopUsers = await _shopUserRepository.CustomFind(x =>
                                            x.Shop.Id == item.Shop.Id &&
                                            x.User.Office.Id == (int)OfficeEnum.Salesman &&
                                            (x.User.UserStatus.Id == (int)UserStatusEnum.Active || x.User.UserStatus.Id == (int)UserStatusEnum.PasswordExpired || (x.User.UserStatus.Id == (int)UserStatusEnum.PreRegistration && (DateTime.Now - x.User.CreatedAt).TotalDays >= 30)),
                                            x => x.User);

                        var totalUsers = shopUsers.Select(x => x.User).Count();

                        var trainingUserFiltered = trainingUser.Where(x =>
                        shopUsers.Select(s => s.User.Id).Contains(x.User.Id)).ToList();

                        if (trainingUserFiltered.Any())
                        {
                            var totalUsersTrainingComplete = trainingUserFiltered.
                                GroupBy(x => new { x.Id, x.User }).
                                Select(x => new
                                {
                                    userId = x.Key.User.Id,
                                    total = x.Select(s => s.Id).Count()
                                }).
                                GroupBy(x => new { x.userId, x.total }).
                                Select(x => new
                                {
                                    user = x.Key.userId,
                                    total = x.Sum(y => y.total)
                                }).ToList().
                                Count(x => x.total > trainingValidation);

                            if (totalUsers > 0)
                            {
                                var porcentageTrainingCompleted = (totalUsersTrainingComplete * 100) / totalUsers;

                                if (porcentageTrainingCompleted >= 80)
                                {
                                    userPunctuationList.Add(new UserPunctuation
                                    {
                                        CreatedAt = DateTime.Now,
                                        CurrentMonth = DateTime.Now.AddMonths(-1).Month,
                                        CurrentYear = DateTime.Now.AddMonths(-1).Year,
                                        Description = "PONTUAÇÃO TREINAMENTO GERENTE",
                                        OperationType = 'C',
                                        Punctuation = 100,
                                        ReferenceEntityId = 0,
                                        User = item.User,
                                        UserPunctuationSource = source
                                    });

                                    _logger.Info($"processamento pontuacao gerente treinamento - adicionado na fila 100 pontos para usuario {item.User.Id}");

                                }

                            }
                        }
                    }

                }

                if (userPunctuationList.Any())
                {
                    _userPunctuationRepository.SaveMany(userPunctuationList);
                    await _unitOfWork.CommitAsync();

                    _logger.Info($"processamento pontuacao gerente treinamento - concluido processamento");

                }
                else
                    _logger.Info($"processamento pontuacao gerente treinamento - nenhum gerente foi pontuado");
            }

        }

        public async Task<IEnumerable<TrainingManagerDto>> GetTrainingManagersReport(TrainingManagerFilterDto trainingManagerFilterDto)
        {
            int filterMonth = trainingManagerFilterDto.Month;
            var today = DateTime.Now;
            int currentMonth = Convert.ToInt32(today.Month);
            
            int monthValue = filterMonth - currentMonth;


            var report = new List<TrainingManagerDto>();
            var shopUsers = await _shopUserRepository.GetShopUsersTrainingManagers(trainingManagerFilterDto.Cpf, trainingManagerFilterDto.Cnpj, trainingManagerFilterDto.Network);


            var lastTraining = await _trainingRepository.CustomFind(x => x.CurrentMonth == DateTime.Now.AddMonths(-monthValue).Month &&
                    x.CurrentYear == DateTime.Now.AddMonths(-monthValue).Year && !x.hasTrainingMaterial && x.trainingKind == 2);

            var trainingUserList = await _trainingUserPointsRepository.CustomFind(x =>
                        x.ResultId == (int)TrainingResultEnum.Approved &&
                        x.TrainingEndedAt.Month == DateTime.Now.AddMonths(-monthValue).Month &&
                        x.TrainingEndedAt.Year == DateTime.Now.AddMonths(-monthValue).Year,
                        x => x.User);
           



            var trainingValidation = lastTraining.Count() >= 2 ? 1 : 0;
            var salesmanList = await _shopUserRepository.CustomFind(x =>
                                            x.User.Office.Id == (int)OfficeEnum.Salesman &&
                                            (x.User.UserStatus.Id == (int)UserStatusEnum.Active || x.User.UserStatus.Id == (int)UserStatusEnum.PasswordExpired || (x.User.UserStatus.Id == (int)UserStatusEnum.PreRegistration && (DateTime.Now - x.User.CreatedAt).TotalDays >= 30)),
                                            x => x.User,
                                            x => x.Shop);
            foreach (var item in shopUsers)
            {
                var salesman = salesmanList.Where(x => x.Shop != null && x.Shop.Id == item.Shop.Id ).Select(x => x.User.Id).ToList();

                var trainingUser = trainingUserList.Where(x => salesman.Contains(x.User.Id));

                var totalUsersTrainingComplete = trainingUser.
                                GroupBy(x => new { x.Id, x.User }).
                                Select(x => new
                                {
                                    userId = x.Key.User.Id,
                                    total = x.Select(s => s.Id).Count()
                                }).
                                GroupBy(x => new { x.userId, x.total }).
                                Select(x => new
                                {
                                    user = x.Key.userId,
                                    total = x.Sum(y => y.total)
                                }).ToList().
                                Count(x => x.total > trainingValidation);

                var porcentageTrainingCompleted = salesman.Count() > 0 ? (totalUsersTrainingComplete * 100) / salesman.Count() : 0;

                report.Add(new TrainingManagerDto
                {
                    Cnpj = item.Shop.Cnpj,
                    Network = item.Shop.Network.Name,
                    Name = item.User.Name,
                    PorcentageCompleted = porcentageTrainingCompleted,
                    TotalSalesman = salesman.Count(),
                    TrainingCompleted = totalUsersTrainingComplete
                });
            }

            return report;

        }
    }
}
