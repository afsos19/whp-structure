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
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<News>> GetNews(int network, int office)
        {
            var newsQuery = _dbContext.News as IQueryable<News>;
            var newsRelatedQuery = _dbContext.NewsRelated as IQueryable<NewsRelated>;

            newsRelatedQuery = newsRelatedQuery.Include(x => x.News).Where(x => x.Network.Id == network && x.Office.Id == office && x.News.Activated);

            return await newsQuery.Where(x => newsRelatedQuery.Select(n => n.News.Id).Contains(x.Id)).OrderBy(x => x.Ordernation).ToListAsync();

        }

    }
}
