using System;
namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class RetornoEstorno
    {
        public int id { get; set; }
        public string dsCodigo { get; set; }
        public string dsAuthorizationCode { get; set; }
        public string dsMensagem { get; set; }


        public RetornoEstorno()
        {
            id = 0;
            dsCodigo = String.Empty;
            dsAuthorizationCode = String.Empty;
            dsMensagem = String.Empty;
        }

    }
}
