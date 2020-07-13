using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class BaseOrder
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string Login { get; set; }
        public long ExternalOrderId { get; set; }
        public string AuthorizationCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
