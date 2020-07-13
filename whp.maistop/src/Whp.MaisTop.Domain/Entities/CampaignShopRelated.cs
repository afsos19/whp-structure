using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class CampaignShopRelated : BaseEntity
    {
        public CampaignRelated CampaignRelated { get; set; }
        public Shop Shop { get; set; }
        
    }
}
