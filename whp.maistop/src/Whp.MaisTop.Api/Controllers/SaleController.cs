using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using Whp.MaisTop.Api.Extensions;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SaleController : MainController
    {

        private readonly ISaleService _saleService;
        private readonly ILogger _logger;

        public SaleController(ILogger logger,IConfiguration configuration, ISaleService saleService) : base(configuration)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetUserSales([FromBody] FilterDto filterDto)
        {
            try
            {
                var result = await _saleService.GetUserSales(UserId, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (result.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhum venda encontrada!");
           
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "GERENTE,GERENTE REGIONAL")]
        public async Task<IActionResult> GetTeamSales([FromBody] FilterMyTeamDto filterDto)
        {
            try
            {
                var result = await _saleService.GetTeamSales(filterDto.Shop, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (result.ListSale.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhum venda encontrada!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveSaleToProcessing(FilterDto filterDto)
        {
            try
            {
                var result = await _saleService.ApproveSaleToProcessing(filterDto.CurrentMonth, filterDto.CurrentYear);

                if (!result.Approved)
                {
                    _logger.Warn($"tentativa de aprovação de vendas com usuario {UserId} - {result.message}");
                    return NotFound(result.message);
                }
                
                _logger.Info($"tentativa de aprovação de vendas com usuario {UserId} - {result.message}");

                return Ok(result.message);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"tentativa de aprovação de vendas com usuario {UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear}");
#endif
                return BadRequest($"tentativa de aprovação de vendas com usuario {UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear}");
            }
        }
    }
}