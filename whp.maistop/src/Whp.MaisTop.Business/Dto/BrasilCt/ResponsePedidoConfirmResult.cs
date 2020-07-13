using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class ResponsePedidoConfirmResult
    {
        public bool success { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }

        public ResponsePedidoConfirmResult()
        {
            success = false;
            authorizationCode = String.Empty;
            returnCode = 0;
            message = String.Empty;
        }
    }
}
