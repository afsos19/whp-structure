using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Configurations
{
    public class EmailConfiguration
    {
        public string HostSMTP { get; set; }
        public string HostPORT { get; set; }
        public string SmtpUser{ get; set; }
        public string SmtpPassword { get; set; }
        public string HostSender { get; set; }
    }
}
