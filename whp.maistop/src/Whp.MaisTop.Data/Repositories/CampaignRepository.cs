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
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(WhpDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Campaign>> GetCampaigns(int network, int office, int shop)
        {

            var campaignRelatedQuery = _dbContext.CampaignRelated as IQueryable<CampaignRelated>;
            var campaignShopRelatedQuery = _dbContext.CampaignShopRelated as IQueryable<CampaignShopRelated>;
            var campaignListResult = new List<Campaign>();

            var campaignShopRelatedList = await campaignShopRelatedQuery
                .Include(x => x.CampaignRelated)
                .Include(x => x.CampaignRelated.Network)
                .Include(x => x.CampaignRelated.Office)
                .Include(x => x.Shop)
                .Include(x => x.CampaignRelated.Campaign)
                .Where(x => x.CampaignRelated.Campaign.Activated).ToListAsync();

            var campaignRelated = await campaignRelatedQuery
                .Include(x => x.Network)
                .Include(x => x.Office)
                .Include(x => x.Campaign)
                .Where(x => x.Campaign.Activated).ToListAsync();

            if (campaignShopRelatedList.Any())
            {
                var filtered = campaignShopRelatedList.Where(x =>
                x.CampaignRelated.Network.Id == network &&
                x.CampaignRelated.Office.Id == office &&
                x.CampaignRelated.Campaign.Activated &&
                x.Shop.Id == shop).ToList();

                if (filtered.Any())
                    campaignListResult.AddRange(filtered.GroupBy(x => x.CampaignRelated.Campaign).Select(x => x.Key).ToList());
            }

            if (campaignRelated.Any())
            {
          
                var filtered = campaignRelated.Where(x =>
                    x.Network.Id == network &&
                    x.Office.Id == office &&
                    (campaignShopRelatedList.Any() ? !campaignShopRelatedList.GroupBy(g => g.CampaignRelated.Campaign.Id).Select(g => g.Key).ToList().Contains(x.Campaign.Id) : 1 == 1) &&
                    x.Campaign.Activated).ToList();

                if (filtered.Any())
                    campaignListResult.AddRange(filtered.GroupBy(x => x.Campaign).Select(x => x.Key).ToList());
            }

            return campaignListResult.Any() ? campaignListResult.OrderBy(x => x.Ordernation).ToList() : campaignListResult;

        }

    }
}
