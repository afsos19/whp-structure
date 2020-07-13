using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IHierarchyFileRepository : IRepository<HierarchyFile>
    {
        Task<bool> ExistFile(int currentMonth, int currentYear, int network);
        Task<IEnumerable<HierarchyFile>> GetHierarchyFile(int currentMonth, int currentYear, int network, int fileStatusId);
        Task<(IEnumerable<HierarchyFile> Rows, int Count)> GetHierarchyFile(int start, int limit, int currentMonth, int currentYear, int network, int fileStatusId);
    }
}
