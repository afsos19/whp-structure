using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WhpDbContext _context;

        public UnitOfWork(WhpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
