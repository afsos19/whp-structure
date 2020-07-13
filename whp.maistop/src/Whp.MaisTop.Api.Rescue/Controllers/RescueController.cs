using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using Swashbuckle.AspNetCore.Annotations;
using Whp.MaisTop.Api.Rescue.Extensions;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Api.Rescue.Controllers
{
    [Route("api/rescue/v1")]
    [ApiController]
    [Authorize(Roles = "BRASILCT")]
    public class RescueController : MainController 
    {
        
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;

        public RescueController(IConfiguration configuration, IOrderService orderService, ILogger logger, IAuthenticationService authenticationService) : base(configuration)
        {
            _orderService = orderService;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet("balance/{cpf}")]
        [SwaggerResponse(200,"Retorna o saldo", typeof(RescueResponseDto<decimal>))]
        public async Task<IActionResult> Balance(string cpf)
        {
            try
            {
                
                var result = await _orderService.UserBalance(cpf);

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar o saldo do cpf :{cpf} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar o saldo do cpf :{cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost("order")]
        [SwaggerResponse(200, "Retorna o pedido criado", typeof(RescueResponseDto<Order>))]
        public async Task<IActionResult> Order([FromBody] RescueRequestDto rescueRequestDto)
        {
            try
            {
                var result = await _orderService.DoRescue(rescueRequestDto);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de gerar pedido do cpf :{rescueRequestDto.Cpf} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de gerar pedido do cpf :{rescueRequestDto.Cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerResponse(200, "Retorna o token", typeof(object))]
        public async Task<IActionResult> Login([FromBody] LoginAdminDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _authenticationService.DoLogin(new LoginDto { Cpf = model.Login, Password = model.Password}, "");

                    if (result.authenticated)
                    {
                        _logger.Info($"Tentativa de login na api de resgate com cpf {model.Login} - {result.messageReturning}");

                        return Ok(new { message = result.messageReturning, token = GenerateJwtToken(result.user) });
                    }
                    else
                    {
                        _logger.Fatal($"Tentativa de login na api de resgate com cpf {model.Login} - {result.messageReturning}");
                        return NotFound(result.messageReturning);
                    }

                }
                else
                {
                    _logger.Fatal($"Tentativa de login na api de resgate com cpf {model.Login} rejeitada pela validação");
                    return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de login na api de resgate com cpf {model.Login} - {ex.ToLogString(Environment.StackTrace)}");
#endif

                return BadRequest($"Tentativa de login na api de resgate com cpf {model.Login} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        [HttpPost("reversal")]
        [SwaggerResponse(200, "Retorna o pedido estornado", typeof(RescueResponseDto<Order>))]
        public async Task<IActionResult> Reversal([FromBody] ReversalRequestDto reversalRequestDto)
        {
            try
            {
                var result = await _orderService.DoReversal(reversalRequestDto);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de gerar estorno do cpf :{reversalRequestDto.Cpf} e pedido extorno {reversalRequestDto.ExternalOrderId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de gerar estorno do cpf :{reversalRequestDto.Cpf} e pedido extorno {reversalRequestDto.ExternalOrderId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}
