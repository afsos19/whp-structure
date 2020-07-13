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

namespace Whp.MaisTop.Data.Repositories
{
    public class HierarchyFileRepository : Repository<HierarchyFile>, IHierarchyFileRepository
    {
        public HierarchyFileRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistFile(int currentMonth, int currentYear, int network)
        {
            var file = (await CustomFind(
                 x => x.CurrentMonth == currentMonth &&
                 x.FileStatus.Id != (int)FileStatusEnum.EndedError &&
                 x.Network.Id == network &&
                 x.CurrentYear == currentYear,
                 x => x.FileStatus)).FirstOrDefault();

            return file != null;
        }

        public async Task<(IEnumerable<HierarchyFile> Rows, int Count)> GetHierarchyFile(int start, int limit, int currentMonth, int currentYear, int network, int fileStatusId)
        {
            var query = _dbContext.Set<HierarchyFile>() as IQueryable<HierarchyFile>;

            if (currentMonth > 0)
                query = query.Where(x => x.CurrentMonth == currentMonth);
            if (currentYear > 0)
                query = query.Where(x => x.CurrentYear == currentYear);
            if (network > 0)
                query = query.Where(x => x.Network.Id == network);
            if (fileStatusId > 0)
                query = query.Where(x => x.FileStatus.Id == fileStatusId);

            query = query.Include(x => x.Network).Include(x => x.User).Include(x => x.FileStatus);

            var count = await query.CountAsync();
            query = query.Skip(start).Take(limit);

            return (await query.OrderBy(x => x.Id).ToListAsync(), count);

        }
        public async Task<IEnumerable<HierarchyFile>> GetHierarchyFile(int currentMonth, int currentYear, int network, int fileStatusId)
        {
            var query = _dbContext.Set<HierarchyFile>() as IQueryable<HierarchyFile>;

            if (currentMonth > 0)
                query = query.Where(x => x.CurrentMonth == currentMonth);
            if (currentYear > 0)
                query = query.Where(x => x.CurrentYear == currentYear);
            if (network > 0)
                query = query.Where(x => x.Network.Id == network);
            if (fileStatusId > 0)
                query = query.Where(x => x.FileStatus.Id == fileStatusId);

            query = query.Include(x => x.Network).Include(x => x.User).Include(x => x.FileStatus);

            return await query.OrderBy(x => x.Id).ToListAsync();

        }

    }
}
