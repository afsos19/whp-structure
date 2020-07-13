using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface ISaleFileRepository : IRepository<SaleFile>
    {
        Task<bool> ExistFile(int currentMonth, int currentYear, int network);
        Task<IEnumerable<SaleFile>> GetSaleFile(int currentMonth, int currentYear, int network, int fileStatusId);
        Task<(IEnumerable<SaleFile> Rows, int Count)> GetSaleFile(int start, int limit, int currentMonth, int currentYear, int network, int fileStatusId);
    }
}
