using System;


namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class ResponsePedidoCancelamentoResult
    {
        public bool success { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }


        public ResponsePedidoCancelamentoResult()
        {
            success = false;
            authorizationCode = String.Empty;
            returnCode = 0;
            message = String.Empty;
        }
    }
}
