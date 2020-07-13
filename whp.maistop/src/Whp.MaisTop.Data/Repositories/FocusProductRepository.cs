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
    public class FocusProductRepository : Repository<FocusProduct>, IFocusProductRepository
    {
        public FocusProductRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FocusProduct>> GetCurrentFocusProduct(int networkId)
        {
            return await CustomFind(
                x => x.Activated &&
                x.CurrentMonth == DateTime.Now.Month &&
                x.CurrentYear == DateTime.Now.Year &&
                x.Network.Id == networkId,
                x => x.Product,
                x => x.GroupProduct);
        }


        public async Task<IEnumerable<FocusProduct>> GetFocusProduct(int network, int currentMonth, int currentYear, List<int> products)
        {

            var query = _dbContext.FocusProduct as IQueryable<FocusProduct>;

            query = query
                .Include(x => x.Network)
                .Include(x => x.Product)
                .Include(x => x.GroupProduct);

            if(products != null)
                query = query.Where(x => products.Contains(x.Product.Id));
            if (network > 0)
                query = query.Where(x => x.Network.Id == network);
            if (currentMonth > 0)
                query = query.Where(x => x.CurrentMonth == currentMonth);
            if (currentYear > 0)
                query = query.Where(x => x.CurrentYear == currentYear);

            return await query.ToListAsync();
        }

    }
}
