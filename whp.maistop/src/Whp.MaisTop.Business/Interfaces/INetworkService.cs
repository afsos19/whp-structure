using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface INetworkService
    {
        Task<IEnumerable<Network>> GetAllNetworks();
        Task<Network> GetById(int id);
    }
}
