using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Business.Services
{
    public class UserStatusService : IUserStatusService
    {
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserStatusLogRepository _userStatusLogRepository;

        public UserStatusService(IUserStatusRepository userStatusRepository, IUserStatusLogRepository userStatusLogRepository)
        {
            _userStatusRepository = userStatusRepository;
            _userStatusLogRepository = userStatusLogRepository;
        }

        public async Task<IEnumerable<UserStatus>> GetAll() => await _userStatusRepository.GetAll();
        public async Task<UserStatusLog> GetUserLog(int idUser) => (await _userStatusLogRepository.CustomFind(x=> x.User.Id == idUser)).FirstOrDefault();
    }
}
