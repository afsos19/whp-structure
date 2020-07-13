using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetUserSales(int userId, int currentMonth, int currentYear);
        Task<MyTeamSaleDto> GetTeamSales(int shop, int currentMonth, int currentYear);
        Task<(bool Approved, string message)> ApproveSaleToProcessing(int currentMonth, int currentYear);
    }
}
