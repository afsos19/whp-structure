using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class RetornoConfirmaPedido
    {
        public int id { get; set; }
        public string dsCodigoLTM { get; set; }
        public string dsAuthorizationCode { get; set; }
        public string dsMensagem { get; set; }
    }
}
