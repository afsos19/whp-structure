using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
  public  class PedidoCancelamentoRequest
    {
        public string accessToken { get; set; }
        public string authorizationCode { get; set; }
        public string orderId { get; set; }
        public string login { get; set; }
  }
}
