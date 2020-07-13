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
    public class ShopService : IShopService
    {
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopUserRepository shopUserRepository, IShopRepository shopRepository)
        {
            _shopUserRepository = shopUserRepository;
            _shopRepository = shopRepository;
        }

        public async Task<Shop> GetById(int id) => await _shopRepository.GetById(id);

        public async Task<IEnumerable<Shop>> GetShop(int userId)
        {
            var shopUser = await _shopUserRepository.CustomFind(x => x.User.Id == userId, x => x.Shop, x => x.Shop.Network);

            return shopUser.Select(x => x.Shop).ToList();
        }
    }
}
