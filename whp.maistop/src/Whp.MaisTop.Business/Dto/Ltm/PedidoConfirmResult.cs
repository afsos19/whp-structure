using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class PedidoConfirmResult
    {
        public bool success { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }

    }
}
