using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IUserStatusService
    {
        Task<IEnumerable<UserStatus>> GetAll();
        Task<UserStatusLog> GetUserLog(int idUser);
    }
}
