using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IOccurrenceRepository : IRepository<Occurrence>
    {
        Task<IEnumerable<Occurrence>> GetOccurrenceFilter(bool Catalog, bool Critical, bool Participant, string Name, int Office, int Network, DateTime? CreatedAt, DateTime? ClosedAt, string Cpf, string Code, int TypeContact, int Subject, int Status, int iteration);
        Task<IEnumerable<Occurrence>> GetOccurrenceFilterEai(string Name, DateTime? CreatedAt, DateTime? ClosedAt, string Cpf, string Code,  int Subject);
    }
}
