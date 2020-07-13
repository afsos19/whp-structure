using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetNews(int network, int office);
    }
}
