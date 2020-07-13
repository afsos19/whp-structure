using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class Network : BaseEntity
    {
        public Regional Regional { get; set; }
        public string Name { get; set; }
        public string SiteName { get; set; }
        public string SiteImage { get; set; }
        public string SiteShortName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }

    }
}
