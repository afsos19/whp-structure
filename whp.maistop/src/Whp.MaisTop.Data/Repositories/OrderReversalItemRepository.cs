using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class OrderReversalItemRepository : Repository<OrderReversalItem>, IOrderReversalItemRepository
    {
        public OrderReversalItemRepository(WhpDbContext context) : base(context)
        {
        }

    }
}
