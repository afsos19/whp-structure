using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using OfficeOpenXml;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Dto.BrasilCt;
using Whp.MaisTop.Business.Extensions;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserStatusLogRepository _userStatusLogRepository;
        private readonly IUserAccessCodeExpirationRepository _userAccessCodeExpirationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;
        private readonly ISMSService _SMSService;
        private readonly IEmailService _emailService;

        public UserService(IMapper mapper, IOfficeRepository officeRepository, IUserRepository userRepository, IShopUserRepository shopUserRepository, IUserStatusRepository userStatusRepository, IUserStatusLogRepository userStatusLogRepository, IUserAccessCodeExpirationRepository userAccessCodeExpirationRepository, IUnitOfWork unitOfWork, ILogger logger, IHostingEnvironment env, ISMSService SMSService, IEmailService emailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _shopUserRepository = shopUserRepository;
            _userStatusRepository = userStatusRepository;
            _userStatusLogRepository = userStatusLogRepository;
            _userAccessCodeExpirationRepository = userAccessCodeExpirationRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _env = env;
            _SMSService = SMSService;
            _emailService = emailService;
            _officeRepository = officeRepository;
        }

        public async Task<(bool Sent, string Message)> SendAccessCodeExpiration(int userId)
        {


            var user = await _userRepository.GetById(userId);
            var msgReturn = "";

            if (string.IsNullOrEmpty(user.CellPhone))
                return (false, "Celular não cadastrado");

            var objetCode = await _userAccessCodeExpirationRepository.GetAccessCode(userId);

            if (objetCode != null && (DateTime.Now - objetCode.CreatedAt).Minutes < 5)
                return (false, "Você já solicitou o envio do token para cadastrar sua nova senha. Aguarde o recebimento ou tente novamente dentro de alguns minutos.");

            var code = await GetAccessCodeExpiration(userId);

            if (string.IsNullOrEmpty(code))
                return (false, "Não foi possivel identificar e gerar o código de acesso do atual usuario!");

            if (string.IsNullOrEmpty(user.Name))
                return (false, "Nome do usuario não cadastrado!");

            if (string.IsNullOrEmpty(user.Email))
                return (false, "Email do usuario não cadastrado!");

            try
            {
                var sent = _emailService.SendAccessCodeExpiration(code, user.Name, user.Email);

                if (!sent)
                    msgReturn = $"Falha ao tentar enviar o código de expiração por email para o email: {user.Email}";
            }
            catch
            {
                msgReturn = $"Falha ao tentar enviar o código de expiração por email para o email: {user.Email}";
            }

            try
            {
                _SMSService.SendAccessConfirmation(user.CellPhone, code);

                msgReturn += $" - Código enviado com sucesso para o numero: {user.CellPhone}";

                return (true, msgReturn);

            }
            catch
            {
                msgReturn += $" - Falha ao tentar enviar o código de expiração por sms para o numero: {user.CellPhone}";
                return (false, msgReturn);
            }

        }

        public async Task<string> GetAccessCodeExpiration(int userId)
        {

            var accessCodeObject = await _userAccessCodeExpirationRepository.GetAccessCode(userId);

            if (accessCodeObject != null)
                _userAccessCodeExpirationRepository.Delete(accessCodeObject);

            var newAccessCodeObject = new UserAccessCodeExpiration
            {
                Code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 6).Select(s => s[new Random().Next(s.Length)]).ToArray()),
                CreatedAt = DateTime.Now,
                User = await _userRepository.GetById(userId)

            };

            _userAccessCodeExpirationRepository.Save(newAccessCodeObject);

            await _unitOfWork.CommitAsync();

            return newAccessCodeObject.Code;
        }

        public async Task<MyTeamUserDto> GetTeamUsers(int shop, int currentMonth, int currentYear)
        {
            var users = (await _shopUserRepository.CustomFind(x => x.Shop.Id == shop,
                x => x.User.Office,
                x => x.User.UserStatus)).Select(x => x.User).Where(x => x.CreatedAt.Month == currentMonth && x.CreatedAt.Year == currentYear).ToList();

            var total = users.Count();

            if (total == 0)
                return new MyTeamUserDto();

            var Activated = (users.Where(x => x.UserStatus.Id == (int)UserStatusEnum.Active).Count() * 100) / total;
            var Inactivated = (users.Where(x => x.UserStatus.Id == (int)UserStatusEnum.Inactive).Count() * 100) / total;
            var PreRegistered = (users.Where(x => x.UserStatus.Id == (int)UserStatusEnum.PreRegistration).Count() * 100) / total;

            return new MyTeamUserDto
            {
                UserList = users,
                Activated = Activated,
                Inactivated = Inactivated,
                PreRegistered = PreRegistered
            };
        }

        public async Task<UserDto> GetUserAccessByCpf(string cpf)
        {
            var unnmapedUser = (await _userRepository.CustomFind(x => x.Cpf.Equals(cpf), x => x.Office, x => x.UserStatus)).FirstOrDefault();
            var user = new UserDto();

            if (unnmapedUser == null)
                return user;

            _mapper.Map(unnmapedUser, user);

            var shops = (await _shopUserRepository.CustomFind(x => x.User.Id == user.Id, x => x.Shop, x => x.Shop.Network)).FirstOrDefault();

            if (shops == null)
                return user;

            user.Network = shops.Shop.Network.Id;
            user.Shop = shops.Shop.Id;

            return user;
        }

        public async Task<UserDto> GetUserAccessById(int id)
        {
            var unnmapedUser = (await _userRepository.CustomFind(x => x.Id == id, x => x.Office, x => x.UserStatus)).FirstOrDefault();
            var user = new UserDto();

            if (unnmapedUser == null)
                return user;

            _mapper.Map(unnmapedUser, user);

            var shops = (await _shopUserRepository.CustomFind(x => x.User.Id == user.Id, x => x.Shop, x => x.Shop.Network)).FirstOrDefault();

            if (shops == null)
                return user;

            user.Network = shops.Shop.Network.Id;
            user.Shop = shops.Shop.Id;

            return user;
        }

        public async Task<User> GetUserById(int id) => (await _userRepository.CustomFind(x => x.Id == id, x => x.UserStatus, x => x.Office)).FirstOrDefault();

        public async Task PasswordExpiration()
        {

            var ActiveUsers = await _userRepository.CustomFind(x => x.UserStatus.Id == (int)UserStatusEnum.Active && x.Office.Id != (int)OfficeEnum.SAC && x.Office.Id != (int)OfficeEnum.BrasilCT, x => x.UserStatus);
            var userStatusLogList = new List<UserStatusLog>();
            var expiredStatus = await _userStatusRepository.GetById((int)UserStatusEnum.PasswordExpired);

            foreach (var item in ActiveUsers)
            {
                var oldStatus = item.UserStatus;
                item.UserStatus = expiredStatus;

                userStatusLogList.Add(new UserStatusLog
                {
                    CreatedAt = DateTime.Now,
                    Description = "Expiração de senha",
                    User = item,
                    UserStatusFrom = oldStatus,
                    UserStatusTo = item.UserStatus
                });

            }

            _userStatusLogRepository.SaveMany(userStatusLogList);

            await _unitOfWork.CommitAsync();

        }

        public async Task<bool> UpdatePrivacityPolicy(int UserId, bool PrivacityPolicy)
        {
            var user = await _userRepository.GetById(UserId);

            user.PrivacyPolicy = PrivacityPolicy;

            var commited = await _unitOfWork.CommitAsync();

            return commited;
        }

        public async Task<(User user, bool updated, string returnMessage)> UpdateUserExpiredPassword(int id, string password, string token)
        {

            var entity = (await _userRepository.CustomFind(x => x.Id == id, x => x.Office, x => x.UserStatus)).FirstOrDefault();

            if (entity == null)
                return (entity, false, "Usuario não encontrado");

            if (string.IsNullOrEmpty(password) || entity.Password.Equals(Crypto.Encrypt(password, Crypto.Key256, 256)))
                return (entity, false, "Senha inválida");

            var accessCode = await _userAccessCodeExpirationRepository.GetAccessCode(id);

            if (accessCode != null && !accessCode.Code.Equals(token))
                return (entity, false, "Token inválido");

            var oldStatus = entity.UserStatus;

            entity.Password = Crypto.Encrypt(password, Crypto.Key256, 256);
            entity.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Active);

            _userStatusLogRepository.Save(new UserStatusLog
            {
                CreatedAt = DateTime.Now,
                Description = "Reativação de usuario com senha expirada",
                User = entity,
                UserStatusFrom = oldStatus,
                UserStatusTo = entity.UserStatus
            });

            await _unitOfWork.CommitAsync();

            return (entity, true, "Usuario Alterado com sucesso");

        }
        public async Task<(UserDto user, bool updated, string returnMessage)> UpdateUser(UserDto userDto, IFormFile file)
        {

            var entity = (await _userRepository.CustomFind(x => x.Id == userDto.Id, x => x.Office, x => x.UserStatus)).First();

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                if (string.IsNullOrEmpty(userDto.Password) || !entity.Password.Equals(Crypto.Encrypt(userDto.OldPassword, Crypto.Key256, 256)) || userDto.Password.Equals(userDto.OldPassword))
                    return (userDto, false, "Senha inválida");

                entity.Password = Crypto.Encrypt(userDto.Password, Crypto.Key256, 256);
            }

            if (file != null)
            {
                if (file.Length > 0 && file.Length <= 2000000)
                {
                    var extensions = Path.GetFileName(file.FileName).Split('.').Last();

                    if (extensions.ToUpper() != "JPG" && extensions.ToUpper() != "JPEG" && extensions.ToUpper() != "PNG")
                    {

                        return (userDto, false, "Foto perfil formato inválido");
                    }

                    var imageName = $"{DateTime.Now.Year}" +
                        $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

                    var path = Path.Combine(_env.WebRootPath, $"Content/PhotoPerfil/{Path.GetFileName(imageName)}");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    userDto.Photo = imageName;

                }
                else
                {
                    return (userDto, false, "Foto perfil tem limite de tamanho 2MB");
                }
            }


            _mapper.Map(userDto, entity);

            if (userDto.OfficeId > 0)
                entity.Office = await _officeRepository.GetById(userDto.OfficeId);

            if (userDto.UserStatusId > 0)
            {
                var newUserStatus = await _userStatusRepository.GetById(userDto.UserStatusId);

                _userStatusLogRepository.Save(new UserStatusLog
                {
                    CreatedAt = DateTime.Now,
                    Description = "Alteração de usuario ADMIN",
                    User = entity,
                    UserStatusFrom = entity.UserStatus,
                    UserStatusTo = newUserStatus
                });

                entity.UserStatus = newUserStatus;
            }

            await _unitOfWork.CommitAsync();

            _mapper.Map(entity, userDto);

            return (userDto, true, "Usuario Alterado com sucesso");

        }

        public async Task<IEnumerable<UserStatusLog>> GetUserStatusLogList(int userId) => await _userStatusLogRepository.CustomFind(x => x.User.Id == userId, x => x.UserStatusFrom, x => x.UserStatusTo, x => x.User);

        public async Task<object> GetUsersToAdmin(UserFilterDto userFilterDto)
        {
            var shopUsers = await _shopUserRepository.GetUsers(userFilterDto.Start, userFilterDto.Length, userFilterDto.Cpf, userFilterDto.Name, userFilterDto.Email, userFilterDto.Office, userFilterDto.UserStatus, userFilterDto.cnpj, userFilterDto.network);

            var userWithNetwork = shopUsers.Users.GroupBy(x => new
            {
                x.User.Cpf,
                x.User.Name,
                x.User.Email,
                UserStatus = x.User.UserStatus.Description,
                Office = x.User.Office.Description,
                Network = x.Shop.Network.Name,
                x.User.Id
            }).Select(x => new[] {
                x.Key.Cpf,
                x.Key.Name,
                x.Key.Email,
                x.Key.UserStatus,
                x.Key.Office,
                x.Key.Network,
                x.Key.Id.ToString()
            }).ToList();

            return new
            {
                data = userWithNetwork,
                recordsTotal = shopUsers.Count,
                recordsFiltered = shopUsers.Count
            };
        }

        public async Task<byte[]> ExportUsersToExcel()
        {
            var userExcel = new List<UserExcelDto>();

            var users = await _shopUserRepository.GetAll(x => x.Shop, x => x.Shop.Network, x => x.User, x => x.User.UserStatus, x => x.User.Office);

            foreach (var item in users)
            {
                if (!userExcel.Where(x => x.Cpf.Equals(item.User.Cpf)).Any() && item.User != null && item.User.Office != null && item.User.UserStatus != null && item.Shop != null && item.Shop.Network != null)
                    userExcel.Add(new UserExcelDto
                    {
                        AccessCodeInvite = item.User.AccessCodeInvite,
                        Network = item.Shop.Network.Name,
                        Name = item.User.Name,
                        Activated = item.User.Activated ? "SIM" : "NÃO",
                        Address = item.User.Address,
                        BithDate = item.User.BithDate != null ? item.User.BithDate.Value.ToString("dd/mm/yyyy") : "",
                        CellPhone = item.User.CellPhone,
                        CEP = item.User.CEP,
                        City = item.User.City,
                        CivilStatus = item.User.CivilStatus.ToString(),
                        Cnpj = item.Shop.Cnpj,
                        CommercialPhone = item.User.CommercialPhone,
                        Complement = item.User.Complement,
                        Cpf = item.User.Cpf,
                        CreatedAt = item.User.CreatedAt != DateTime.MinValue ? item.User.CreatedAt.ToString("dd/mm/yyyy") : "",
                        Email = item.User.Email,
                        Gender = item.User.Gender.ToString(),
                        HeartTeam = item.User.HeartTeam,
                        Neighborhood = item.User.Neighborhood,
                        Number = item.User.Number,
                        Office = item.User.Office.Description,
                        OffIn = item.User.OffIn != null ? item.User.OffIn.Value.ToString("dd/mm/yyyy") : "",
                        Password = item.User.Password,
                        PasswordRecoveredAt = item.User.PasswordRecoveredAt != null ? item.User.PasswordRecoveredAt.Value.ToString("dd/mm/yyyy") : "",
                        Phone = item.User.Phone,
                        Photo = item.User.Photo,
                        PrivacyPolicy = item.User.PrivacyPolicy ? "SIM" : "NÃO",
                        ReferencePoint = item.User.ReferencePoint,
                        SonAmout = item.User.SonAmout,
                        Uf = item.User.Uf,
                        UserStatus = item.User.UserStatus.Description
                    });
            }

            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excel.Workbook.Worksheets.Add("REPORT GERAL USUARIOS");
                excelWorksheet.Cells["A1"].LoadFromDataTable(userExcel.ToDataTable(), true);
                return excel.GetAsByteArray();
            }

        }
    }
}
