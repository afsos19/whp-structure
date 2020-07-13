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
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Data.Repositories
{
    public class ShopUserRepository : Repository<ShopUser>, IShopUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShopUserRepository(WhpDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ClearShops(IEnumerable<string> cpfs)
        {
            DeleteMany(await CustomFind(x => cpfs.Contains(x.User.Cpf)));

            return await _unitOfWork.CommitAsync();
        }
        public async Task<bool> ClearShopsByNetwork(int network)
        {
            DeleteMany(await CustomFind(x => x.Shop.Network.Id == network));

            return await _unitOfWork.CommitAsync();
        }

        public async Task ClearRegionManagerShops(IEnumerable<string> cpfs)
        {
            DeleteMany(await CustomFind(x => x.User.Office.Id == (int)OfficeEnum.RegionManager && cpfs.Contains(x.User.Cpf)));

            await _unitOfWork.CommitAsync();
        }

        public async Task<(IEnumerable<ShopUser> Users, int Count)> GetUsers(int start, int limit, string cpf = "", string name = "", string email = "", int office = 0, int userStatus = 0, string cnpj = "", int network = 0)
        {
            var queryUser = _dbContext.Set<ShopUser>() as IQueryable<ShopUser>;

            if (!string.IsNullOrEmpty(cpf))
                queryUser = queryUser.Where(x => x.User.Cpf.Equals(cpf));
            if (!string.IsNullOrEmpty(name))
                queryUser = queryUser.Where(x => x.User.Name.Equals(name));
            if (!string.IsNullOrEmpty(email))
                queryUser = queryUser.Where(x => x.User.Email.Equals(email));
            if (office > 0)
                queryUser = queryUser.Where(x => x.User.Office.Id == office);
            if(network > 0)
                queryUser = queryUser.Where(x => x.Shop.Network.Id == network);
            if (userStatus > 0)
                queryUser = queryUser.Where(x => x.User.UserStatus.Id == userStatus);
            if (!string.IsNullOrEmpty(cnpj))
                queryUser = queryUser.Where(x => x.Shop.Cnpj.Equals(cnpj));

            queryUser = queryUser.Include(x => x.User.UserStatus).Include(x => x.User.Office).Include(x => x.Shop).ThenInclude(x => x.Network);

            var count = await queryUser.CountAsync();
            queryUser = queryUser.Skip(start).Take(limit);

            return (await queryUser.ToListAsync(), count);
        }
        public async Task<(IEnumerable<ShopUser> Users, int Count)> GetUsers(string cpf = "", string name = "", string email = "", int office = 0, int userStatus = 0, string cnpj = "", int network = 0)
        {
            var queryUser = _dbContext.Set<ShopUser>() as IQueryable<ShopUser>;

            if (!string.IsNullOrEmpty(cpf))
                queryUser = queryUser.Where(x => x.User.Cpf.Equals(cpf));
            if (!string.IsNullOrEmpty(name))
                queryUser = queryUser.Where(x => x.User.Name.Equals(name));
            if (!string.IsNullOrEmpty(email))
                queryUser = queryUser.Where(x => x.User.Email.Equals(email));
            if (office > 0)
                queryUser = queryUser.Where(x => x.User.Office.Id == office);
            if (network > 0)
                queryUser = queryUser.Where(x => x.Shop.Network.Id == network);
            if (userStatus > 0)
                queryUser = queryUser.Where(x => x.User.UserStatus.Id == userStatus);
            if (!string.IsNullOrEmpty(cnpj))
                queryUser = queryUser.Where(x => x.Shop.Cnpj.Equals(cnpj));

            queryUser = queryUser.Include(x => x.User.UserStatus).Include(x => x.User.Office).Include(x => x.Shop).ThenInclude(x => x.Network);

            return (await queryUser.ToListAsync(), await queryUser.CountAsync());
        }

        public async Task<IEnumerable<ShopUser>> GetShopUsersTrainingManagers(string cpf, string cnpj, int network)
        {
            var queryUser = _dbContext.Set<ShopUser>() as IQueryable<ShopUser>;

            queryUser = queryUser.Where(x =>
                x.User.Office.Id == (int)OfficeEnum.Manager &&
                (x.User.UserStatus.Id == (int)UserStatusEnum.Active || x.User.UserStatus.Id == (int)UserStatusEnum.PasswordExpired));

            if (!string.IsNullOrEmpty(cpf))
                queryUser = queryUser.Where(x => x.User.Cpf.Equals(cpf));

            if (!string.IsNullOrEmpty(cnpj))
                queryUser = queryUser.Where(x => x.Shop.Cnpj.Equals(cnpj));

            if (network > 0)
                queryUser = queryUser.Where(x => x.Shop.Network.Id == network);

            queryUser = queryUser.Include(x => x.User).Include(x => x.Shop).ThenInclude(x => x.Network);

            return await queryUser.ToListAsync();
        }
    }
}
