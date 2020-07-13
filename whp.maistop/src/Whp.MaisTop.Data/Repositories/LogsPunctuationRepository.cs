using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class LogsPunctuationRepository : Repository<LogsPunctuation>, ILogsPunctuationRepository
    {
        public LogsPunctuationRepository(WhpDbContext dbContext) : base(dbContext)
        {
        }
    }
}
