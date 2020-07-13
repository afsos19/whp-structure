using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IFocusProductRepository : IRepository<FocusProduct>
    {
        Task<IEnumerable<FocusProduct>> GetCurrentFocusProduct(int networkId);
        Task<IEnumerable<FocusProduct>> GetFocusProduct(int network, int currentMonth, int currentYear, List<int> products);
    }
}
