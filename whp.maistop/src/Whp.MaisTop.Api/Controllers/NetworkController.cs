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
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NetworkController : MainController
    {
        private readonly INetworkService _networkService;
        private readonly ILogger _logger;

        public NetworkController(ILogger logger,IConfiguration configuration, INetworkService officeService) : base(configuration)
        {
            _networkService = officeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var network = await _networkService.GetAllNetworks();

                if (!network.Any())
                {
                    _logger.Warn($"Tentativa recuperar as redes com usuario id :{UserId} - redes não encontrada");
                    return NotFound();
                }
                
                return Ok(network);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar as redes com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar as redes com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}

