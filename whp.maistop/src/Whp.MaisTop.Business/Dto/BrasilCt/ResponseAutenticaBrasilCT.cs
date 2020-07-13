using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class ResponseAutenticaBrasilCT
    {
        public bool success { get; set; }
        public string login { get; set; }
        public string sessionId { get; set; }

        public ResponseAutenticaBrasilCT()
        {
            success = false;
            login = String.Empty;
            sessionId = String.Empty;
        }
    }
}
