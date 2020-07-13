using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IShopUserRepository : IRepository<ShopUser>
    {
        Task<(IEnumerable<ShopUser> Users, int Count)> GetUsers(int start, int limit, string cpf = "", string name = "", string email = "", int office = 0, int userStatus = 0, string cnpj = "", int network = 0);
        Task<(IEnumerable<ShopUser> Users, int Count)> GetUsers(string cpf = "", string name = "", string email = "", int office = 0, int userStatus = 0, string cnpj = "", int network = 0);
        Task<bool> ClearShops(IEnumerable<string> cpfs);
        Task<bool> ClearShopsByNetwork(int network);
        Task ClearRegionManagerShops(IEnumerable<string> cpfs);
        Task<IEnumerable<ShopUser>> GetShopUsersTrainingManagers(string cpf, string cnpj, int network);

    }
}
