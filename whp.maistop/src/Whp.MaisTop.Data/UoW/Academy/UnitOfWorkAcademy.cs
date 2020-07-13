using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Context.Academy;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Whp.MaisTop.Domain.Interfaces.UoW.Academy;

namespace Whp.MaisTop.Data.UoW.Academy
{
    public class UnitOfWorkAcademy : IUnitOfWorkAcademy
    {
        private readonly WhpAcademyDbContext _context;

        public UnitOfWorkAcademy(WhpAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
