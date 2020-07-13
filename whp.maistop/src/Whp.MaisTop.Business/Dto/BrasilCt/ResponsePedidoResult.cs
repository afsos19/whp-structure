using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class ResponsePedidoResult
    {
        public bool success { get; set; }
        public bool authorized { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }


        public ResponsePedidoResult()
        {
            success = false;
            authorized = false;
            authorizationCode = String.Empty;
            returnCode = 0;
            message = String.Empty;
        }
    }
}
