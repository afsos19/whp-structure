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
    public class PunctuationController : MainController
    {

        private readonly IPunctuationService _punctuationService;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public PunctuationController(ILogger logger, IConfiguration configuration, IPunctuationService punctuationService, IOrderService orderService) : base(configuration)
        {
            _punctuationService = punctuationService;
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUserCredits([FromBody] FilterDto filterDto)
        {
            try
            {
                var credits = await _punctuationService.GetUserCredits(UserId, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (credits.Any())
                {
                    _logger.Info($"Exibido lista de creditos do Usuario com id {UserId}");
                    return Ok(credits);
                }
                else
                {
                    _logger.Info($"Exibicao lista de creditos do Usuario com id {UserId} - Nenhum crédito encontrado");
                    return NotFound("Nenhum crédito encontrado");
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Exibicao lista de creditos do Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Exibicao lista de creditos do Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserExtract()
        {
            try
            {
                if(UserId > 0)
                {
                    var user = await _punctuationService.GetUserExtract(UserId);

                    _logger.Info($"Exibido Extrado do Usuario com id {UserId}");

                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                return BadRequest($"Exibicao Extrado do Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetUserBalance(string login)
        {
            try
            {

                var result = await _orderService.UserBalance(login);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}