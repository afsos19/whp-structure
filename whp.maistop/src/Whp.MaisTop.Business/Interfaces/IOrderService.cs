using Ssg.MaisSamsung.Business.Dto.Ltm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IOrderService
    {
        Task<RescueResponseDto<Order>> DoRescue(RescueRequestDto rescueRequestDto);
        Task<RescueResponseDto<decimal>> UserBalance(string cpf);
        Task<RescueResponseDto<Order>> DoReversal(ReversalRequestDto orderRequestDto);
        Task<IEnumerable<OrderReturnDto>> GetUserOrder(int userId, int currentMonth, int currentYear);

    }
}
