using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
  public  class RetornoConfirmaPedido
    {
        public int id { get; set; }
        public string dsCodigo { get; set; }
        public string dsAuthorizationCode { get; set; }
        public string dsMensagem { get; set; }

        public RetornoConfirmaPedido()
        {
            id = 0;
            dsCodigo = String.Empty;
            dsAuthorizationCode = String.Empty;
            dsMensagem = String.Empty;

        }
    }
}
