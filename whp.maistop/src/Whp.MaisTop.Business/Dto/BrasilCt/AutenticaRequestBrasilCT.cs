using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class AutenticaRequestBrasilCT
    {
        public string projectId { get; set; }
        public int profileId { get; set; }
        public string name { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string login { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string cellphone { get; set; }
        public List<addresses> addresses { get; set; }


       public AutenticaRequestBrasilCT()
       {
           projectId = String.Empty;
           profileId = 0;
           name = String.Empty;
           cpf = String.Empty;
           rg = String.Empty;
           login = String.Empty;
           gender = String.Empty;
           birthDate = String.Empty;
           email = String.Empty;
           phone = String.Empty;
           cellphone = String.Empty;
           addresses = null;

       }
    }
}
