using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> AuthenticateUser(string cpf, string password);
        Task<bool> CellphoneUsed(string cellphone);
        Task<(IEnumerable<User> Users, int Count)> GetUsers(int start, int limit, string cpf = "", string name = "", string email = "", int office = 0);
        Task<(IEnumerable<User> Users, int Count)> GetUsers(string cpf = "", string name = "", string email = "", int office = 0);
    }
}
