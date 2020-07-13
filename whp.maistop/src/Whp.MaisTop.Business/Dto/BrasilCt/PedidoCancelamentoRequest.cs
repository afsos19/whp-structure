using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class PedidoCancelamentoRequest
    {
        public string accessToken { get; set; }
        public string authorizationCode { get; set; }
        public string orderId { get; set; }
        public string login { get; set; }

        public PedidoCancelamentoRequest()
        {
            accessToken = String.Empty;
            authorizationCode = String.Empty;
            orderId = String.Empty;
            login = String.Empty;
        }
    }
}
