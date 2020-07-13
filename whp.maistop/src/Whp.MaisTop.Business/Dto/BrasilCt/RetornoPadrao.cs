using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class RetornoPadrao
    {
       public int id { get; set; }
       public string dsMensagem { get; set; }

       public RetornoPadrao()
       {
           id = 0;
           dsMensagem = String.Empty;
       }

    }
}
