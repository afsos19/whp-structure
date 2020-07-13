using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<Campaign>> GetCampaigns(int network, int office, int shop);
    }
}
