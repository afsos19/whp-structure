using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class PedidoResult
    {
        public bool success { get; set; }
        public bool authorized { get; set; }
        public string authorizationCode { get; set; }
        public int returnCode { get; set; }
        public string message { get; set; }
        public List<originApportionment> originApportionment { get; set; }
    }
}
