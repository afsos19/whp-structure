﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Dto.BrasilCt
{
    public class PedidoConfirmaRequest
    {
        public string accessToken { get; set; }
        public string orderId { get; set; }
        public string login { get; set; }
        public string authorizationCode { get; set; }



        public PedidoConfirmaRequest()
        {
            accessToken = String.Empty;
            orderId = String.Empty;
            login = String.Empty;
            authorizationCode = String.Empty;

        }
    }
}
