
namespace Whp.MaisTop.Business.Dto.BrasilCt
{
   public class ResponseSaldo
    {
       public bool sucesso { get; set; }
       public decimal balance { get; set; }
       public int returnCode { get; set; }
       public string message { get; set; }


       public ResponseSaldo()
       {
           sucesso = false;
           balance = 0;
           returnCode = 0;
           message = string.Empty;
       }

    }
}
