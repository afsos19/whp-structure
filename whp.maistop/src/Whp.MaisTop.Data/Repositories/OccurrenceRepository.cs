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
    public class OccurrenceRepository : Repository<Occurrence>, IOccurrenceRepository
    {
        public OccurrenceRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Occurrence>> GetOccurrenceFilter(bool Catalog, bool Critical, bool Participant, string Name, int Office, int Network, DateTime? CreatedAt, DateTime? ClosedAt, string Cpf, string Code, int TypeContact, int Subject, int Status, int iteration)
        {
            var occurrenceMessageQuery = _dbContext.OccurrenceMessage as IQueryable<OccurrenceMessage>;

            var occurrenceQuery = _dbContext.Occurrence as IQueryable<Occurrence>;
            var shopUserQuery = _dbContext.ShopUser as IQueryable<ShopUser>;

            if (Catalog != false) {
                var usersId = occurrenceMessageQuery.Where(x => x.Catalog == true).Select(x => x.User.Id);
                occurrenceQuery = occurrenceQuery.Where(x => usersId.Contains(x.User.Id));
            }

            if (Critical != false)
                occurrenceQuery = occurrenceQuery.Where(x => x.Critical == true);

            if (Participant != false)
                occurrenceQuery = occurrenceQuery.Where(x => x.Participant == true);

            if (!string.IsNullOrEmpty(Name))
                occurrenceQuery = occurrenceQuery.Where(x => x.User.Name.ToUpper().Equals(Name.ToUpper()));

            if (!string.IsNullOrEmpty(Code))
                occurrenceQuery = occurrenceQuery.Where(x => x.Code.Equals(Code));

            if (!string.IsNullOrEmpty(Cpf))
                occurrenceQuery = occurrenceQuery.Where(x => x.User.Cpf.Equals(Cpf));

            if (Office > 0)
                occurrenceQuery = occurrenceQuery.Where(x => x.User.Office.Id == Office);

            if (TypeContact > 0)
                occurrenceQuery = occurrenceQuery.Where(x => x.OccurrenceContactType.Id == TypeContact);

            if (Subject > 0)
                occurrenceQuery = occurrenceQuery.Where(x => x.OccurrenceSubject.Id == Subject);

            if (Status > 0)
                occurrenceQuery = occurrenceQuery.Where(x => x.OccurrenceStatus.Id == Status);

            if (Network > 0)
            {
                var usersId = shopUserQuery.Where(x => x.Shop.Network.Id == Network).Select(x => x.User.Id);
                occurrenceQuery = occurrenceQuery.Where(x => usersId.Contains(x.User.Id));
            }

            if (iteration > 0)
            {
                if (iteration == 1)
                    occurrenceQuery = occurrenceQuery.Where(x => x.ReturnedAt.HasValue && x.RedirectedAt.HasValue && x.ReturnedAt > x.RedirectedAt);
                if (iteration == 2)
                    occurrenceQuery = occurrenceQuery.Where(x => x.RedirectedAt.HasValue || (x.ReturnedAt.HasValue && x.RedirectedAt.HasValue && x.RedirectedAt > x.ReturnedAt));
            }

            if (CreatedAt != null)
                occurrenceQuery = occurrenceQuery.Where(x =>
                x.CreatedAt.Date == CreatedAt.Value.Date);

            if (ClosedAt != null)
                occurrenceQuery = occurrenceQuery.Where(x =>
                x.ClosedAt.Value.Date == ClosedAt.Value.Date );

            occurrenceQuery = occurrenceQuery.OrderByDescending(x => x.CreatedAt);
            occurrenceQuery = occurrenceQuery
                .Include(x => x.OccurrenceContactType)
                .Include(x => x.OccurrenceStatus)
                .Include(x => x.OccurrenceSubject)
                .Include(x => x.User);

            return await occurrenceQuery.ToListAsync();
        }

        public async Task<IEnumerable<Occurrence>> GetOccurrenceFilterEai(string Name, DateTime? CreatedAt, DateTime? ClosedAt, string Cpf, string Code, int Subject)
        {
            var occurrenceMessageQuery = _dbContext.OccurrenceMessage as IQueryable<OccurrenceMessage>;

            occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Catalog);

            if (Subject > 0)
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.OccurrenceSubject.Id == Subject);

            if (!string.IsNullOrEmpty(Name))
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.User.Name.ToUpper().Equals(Name.ToUpper()));

            if (!string.IsNullOrEmpty(Code))
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.Code.Equals(Code));

            if (!string.IsNullOrEmpty(Cpf))
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.User.Cpf.Equals(Cpf));

            if (CreatedAt != null)
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.CreatedAt.Date == CreatedAt.Value.Date);

            if (ClosedAt != null)
                occurrenceMessageQuery = occurrenceMessageQuery.Where(x => x.Occurrence.ClosedAt.Value.Date == ClosedAt.Value.Date);

            occurrenceMessageQuery = occurrenceMessageQuery
                .Include(x => x.Occurrence)
                .Include(x => x.Occurrence.OccurrenceContactType)
              .Include(x => x.Occurrence.OccurrenceStatus)
              .Include(x => x.Occurrence.OccurrenceSubject)
              .Include(x => x.Occurrence.User);

            var occurrence = await occurrenceMessageQuery.GroupBy(x => new
            {
                x.Occurrence.Id,
                x.Occurrence.LastIteration,
                x.Occurrence.CreatedAt,
                x.Occurrence.ClosedAt,
                x.Occurrence.RedirectedAt,
                x.Occurrence.ReturnedAt,
                x.Occurrence.OccurrenceContactType,
                x.Occurrence.User,
                x.Occurrence.OccurrenceSubject,
                x.Occurrence.OccurrenceStatus
            })
                .Select(s => new Occurrence
                {
                    Id = s.Key.Id,
                    LastIteration = s.Key.LastIteration,
                    CreatedAt = s.Key.CreatedAt,
                    ClosedAt = s.Key.ClosedAt,
                    RedirectedAt = s.Key.RedirectedAt,
                    ReturnedAt = s.Key.ReturnedAt,
                    OccurrenceContactType = s.Key.OccurrenceContactType,
                    User = s.Key.User,
                    OccurrenceSubject = s.Key.OccurrenceSubject,
                    OccurrenceStatus = s.Key.OccurrenceStatus

                }).ToListAsync();

            return occurrence.OrderByDescending(x => x.RedirectedAt);
        }
    }
}
