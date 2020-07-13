using System;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class RetornoAutorizaResgate
    {
        public int id { get; set; }
        public string dsCodigo { get; set; }
        public string dsMensagem { get; set; }

       public RetornoAutorizaResgate()
       {
           id = 0;
           dsCodigo = String.Empty;
           dsMensagem = String.Empty;
       }
    }
}
