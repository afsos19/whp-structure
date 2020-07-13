using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{

    public class FriendInviteService : IFriendInviteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessCodeInviteRepository _userAccessCodeInviteRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISMSService _SMSService;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IOfficeRepository _officeRepository;

        public FriendInviteService(IUnitOfWork unitOfWork, IUserAccessCodeInviteRepository userAccessCodeInviteRepository, IUserRepository userRepository, ISMSService SMSService, IUserStatusRepository userStatusRepository, IOfficeRepository officeRepository)
        {
            _unitOfWork = unitOfWork;
            _userAccessCodeInviteRepository = userAccessCodeInviteRepository;
            _userRepository = userRepository;
            _SMSService = SMSService;
            _userStatusRepository = userStatusRepository;
            _officeRepository = officeRepository;
        }

        public async Task<(bool Saved, string Message)> DoCadUserInvited(AccessCodeUserInviteDto accessCodeUserInviteDto)
        {


            var code = await _userAccessCodeInviteRepository.CustomFind(x => x.Code.Equals(accessCodeUserInviteDto.Code));

            if (!code.Any())
                return (false, "Código de convite inválido");

            var user = (await _userRepository.CustomFind(x => x.Cpf.Equals(Regex.Replace(accessCodeUserInviteDto.Cpf, "[^0-9]", "")))).FirstOrDefault();

            if (user != null)
                return (false, "Usuario ja cadastrado no programa!");

            _userRepository.Save(new User
            {
                CellPhone = accessCodeUserInviteDto.Cellphone,
                Cpf = Regex.Replace(accessCodeUserInviteDto.Cpf, "[^0-9]", ""),
                AccessCodeInvite = accessCodeUserInviteDto.Code,
                UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.FriendInvitation),
                Activated = false,
                Office = await _officeRepository.GetById((int)OfficeEnum.Salesman),
                PrivacyPolicy = false,
                CreatedAt = DateTime.Now
            });

            await _unitOfWork.CommitAsync();

            return (true, "Usuario cadastrado com sucesso!");
        }

        public async Task<string> GetAccessCodeInvite(int userId, bool update)
        {
            var code = "";
            var accessCodeObject = await _userAccessCodeInviteRepository.GetAccessCode(userId);

            if (update)
            {
                accessCodeObject.UpdatedAt = DateTime.Now;
                code = accessCodeObject.Code;
            }
            else if (accessCodeObject == null)
            {
                var newAccessCodeObject = new UserAccessCodeInvite
                {
                    Code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 6).Select(s => s[new Random().Next(s.Length)]).ToArray()),
                    CreatedAt = DateTime.Now,
                    User = await _userRepository.GetById(userId),
                    UpdatedAt = null

                };

                _userAccessCodeInviteRepository.Save(newAccessCodeObject);
                code = newAccessCodeObject.Code;
            }
            else
            {
                code = accessCodeObject.Code;
            }

            await _unitOfWork.CommitAsync();

            return code;
        }

        public async Task<IEnumerable<object>> GetInvitedUsers(int UserId)
        {

            var code = await GetAccessCodeInvite(UserId,false);

            var users = await _userRepository.CustomFind(x => x.AccessCodeInvite.Equals(code), x => x.UserStatus);

            if (!users.Any())
                return null;

            return users.Select(async x => new
            {
                x.Name,
                InvitedAt = x.CreatedAt.ToString("dd/MM/yyyy"),
                CreatedAt = (x.UserStatus.Id != (int)UserStatusEnum.FriendInvitation ? (await _userStatusRepository.GetById(x.Id)).CreatedAt.ToString("dd/MM/yyyy") : ""),
                GotPunctuation = (x.UserStatus.Id != (int)UserStatusEnum.FriendInvitation ? true : false)
            }).ToList();

        }

        public async Task<(bool Sent, string Message)> SendAccessCodeInvite(string cellphone, int userId)
        {

            var objetCode = await _userAccessCodeInviteRepository.GetAccessCode(userId);

            if (objetCode != null && objetCode.UpdatedAt != null && (DateTime.Now - (DateTime)objetCode.UpdatedAt).Minutes < 5)
                return (false, "Você já solicitou o envio do código de indicação do convide amigos para este número");

            var code = await GetAccessCodeInvite(userId,true);

            if (string.IsNullOrEmpty(code))
                return (false, "Não foi possivel identificar e gerar o código de acesso do atual usuario!");

            try
            {
                _SMSService.SendAccessInvite(cellphone, code);

                return (true, $"Código enviado com sucesso para o numero: {cellphone}");

            }
            catch
            {
                return (false, $"Falha ao tentar enviar código de convite de amigo por sms para o numero: {cellphone}");
            }

        }
    }

}
