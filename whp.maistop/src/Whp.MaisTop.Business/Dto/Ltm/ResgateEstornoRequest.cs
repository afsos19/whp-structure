
namespace Ssg.MaisSamsung.Business.Dto.Ltm
{
    public class ResgateEstornoRequest
    {
        public string accessToken { get; set; }
        public string authorizationCode { get; set; }
        public string orderId { get; set; }
        public string login { get; set; }
        public decimal reversalPointsValue { get; set; }
        public string message { get; set; }
        public Apportionment apportionment { get; set; }
    }

    public class Apportionment
    {
        public int inCodigo { get; set; }
        public decimal vlValue { get; set; }
    }
}
