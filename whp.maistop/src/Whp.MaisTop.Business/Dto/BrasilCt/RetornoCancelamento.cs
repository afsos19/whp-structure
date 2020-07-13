using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class RetornoCancelamento
    {
        public int id { get; set; }
        public string dsCodigo { get; set; }
        public string dsAuthorizationCode { get; set; }
        public string dsMensagem { get; set; }


        public RetornoCancelamento()
        {
            id = 0;
            dsCodigo = String.Empty;
            dsAuthorizationCode = String.Empty;
            dsMensagem = String.Empty;

        }
    }
}
