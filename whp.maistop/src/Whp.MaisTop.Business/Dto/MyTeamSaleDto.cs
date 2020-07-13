using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class MyTeamSaleDto
    {
        public MyTeamSaleDto()
        {
            Participant = 0;
            SuperTop = 0;
            ListSale = new List<MyTeamSaleReturnListDto>();
        }

        public int Participant { get; set; }
        public int SuperTop { get; set; }
        public IEnumerable<MyTeamSaleReturnListDto> ListSale { get; set; }
    }
}
