using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public  class ResponseResgateEstornoResult
    {
        public bool success { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }

        public ResponseResgateEstornoResult()
        {
            success = false;
            authorizationCode = String.Empty;
            returnCode = 0;
            message = String.Empty;
        }
    }
}
