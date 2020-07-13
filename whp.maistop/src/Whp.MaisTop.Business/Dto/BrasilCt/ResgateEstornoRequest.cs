using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class ResgateEstornoRequest
    {
        public string accessToken { get; set; }
        public string authorizationCode { get; set; }
        public string orderId { get; set; }
        public string login { get; set; }
        public decimal reversalPointsValue { get; set; }
        public string message { get; set; }

        public ResgateEstornoRequest()
        {
            accessToken = String.Empty;
            authorizationCode = String.Empty;
            orderId = String.Empty;
            login = String.Empty;
            reversalPointsValue = 0;
            message = String.Empty;
        }
    }
}
