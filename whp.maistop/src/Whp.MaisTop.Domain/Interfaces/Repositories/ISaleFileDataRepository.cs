using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.ViewModels;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface ISaleFileDataRepository : IRepository<SaleFileData>
    {
        Task<(IEnumerable<SKUClassificationVM> Data, int Count)> GetPendingClassification(int start, int limit, int network = 0);
        Task<(IEnumerable<string[]> Data, int Count)> GetPreProcessingSale(int currentYear, int currentMonth, int start, int limit,int network = 0);
    }
}
