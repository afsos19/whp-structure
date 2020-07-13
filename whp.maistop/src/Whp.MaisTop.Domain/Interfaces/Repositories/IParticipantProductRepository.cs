using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IParticipantProductRepository : IRepository<ParticipantProduct>
    {
        Task<IEnumerable<ParticipantProduct>> GetCurrentParticipantProduct(int networkId);
        Task<IEnumerable<ParticipantProduct>> GetParticipantProduct(int network, int currentMonth, int currentYear, List<int> products);

    }
}
