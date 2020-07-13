using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : MainController
    {

        private readonly IOrderService _orderService;

        public OrderController(IConfiguration configuration, IOrderService orderService) : base(configuration)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUserOrder(FilterDto filterDto)
        {
            try
            {
                var result = await _orderService.GetUserOrder(UserId, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (result.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhum registro encontrado");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}