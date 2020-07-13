using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<User> AuthenticateUser(string cpf, string password)
        {
            return (await CustomFind(x => x.Cpf.Equals(cpf) && 
                                     x.Password.Equals(password) &&
                                     x.Activated &&
                                     (x.UserStatus.Id == (int)UserStatusEnum.Active || x.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog || x.UserStatus.Id == (int)UserStatusEnum.PasswordExpired ), 
                                     x => x.UserStatus,
                                     x => x.Office)).FirstOrDefault();
        }

        public async Task<bool> CellphoneUsed(string cellphone) => (await CustomFind(x => x.CellPhone == cellphone)).FirstOrDefault() != null ? true : false;

        public async Task<(IEnumerable<User> Users, int Count)> GetUsers(int start, int limit,string cpf = "", string name = "", string email = "", int office = 0)
        {
            var queryUser = _dbContext.Set<User>() as IQueryable<User>;

            if (!string.IsNullOrEmpty(cpf))
                queryUser = queryUser.Where(x => x.Cpf.Equals(cpf));
            if (!string.IsNullOrEmpty(name))
                queryUser = queryUser.Where(x => x.Name.Equals(name));
            if (!string.IsNullOrEmpty(email))
                queryUser = queryUser.Where(x => x.Email.Equals(email));
            if (office > 0)
                queryUser = queryUser.Where(x => x.Office.Id == office);

            queryUser = queryUser.Include(x => x.UserStatus).Include(x => x.Office);

            var count = await queryUser.CountAsync();
            queryUser = queryUser.Skip(start).Take(limit);

            return (await queryUser.ToListAsync(), count);
        }
        public async Task<(IEnumerable<User> Users, int Count)> GetUsers(string cpf = "", string name = "", string email = "", int office = 0)
        {
            var queryUser = _dbContext.Set<User>() as IQueryable<User>;

            if (!string.IsNullOrEmpty(cpf))
                queryUser = queryUser.Where(x => x.Cpf.Equals(cpf));
            if (!string.IsNullOrEmpty(name))
                queryUser = queryUser.Where(x => x.Name.Equals(name));
            if (!string.IsNullOrEmpty(email))
                queryUser = queryUser.Where(x => x.Email.Equals(email));
            if (office > 0)
                queryUser = queryUser.Where(x => x.Office.Id == office);

            queryUser = queryUser.Include(x => x.UserStatus).Include(x => x.Office);

            return (await queryUser.ToListAsync(), await queryUser.CountAsync());
        }
    }
}
