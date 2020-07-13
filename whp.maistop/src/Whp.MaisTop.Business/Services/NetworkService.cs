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
    public class NetworkService : INetworkService
    {
        private readonly INetworkRepository _networkRepository;

        public NetworkService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }

        public async Task<IEnumerable<Network>> GetAllNetworks() => (await _networkRepository.CustomFind(x => x.Activated)).OrderBy(x => x.Name).ToList();

        public async Task<Network> GetById(int id) => await _networkRepository.GetById(id);
    }
}
