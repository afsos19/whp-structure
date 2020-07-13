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
    public class CampaignController : MainController
    {

        private readonly ICampaignService _campaignService;
        private readonly ILogger _logger;

        public CampaignController(ILogger logger,IConfiguration configuration, ICampaignService campaignService) : base(configuration)
        {
            _campaignService = campaignService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCampaigns()
        {
            try
            {
                var result = await _campaignService.GetCampaigns(Network, Office, Shop);

                if (result.Any())
                {
                    _logger.Info($"Tentativa recuperar a campanha com usuario id :{UserId} - recuperado campanha com sucesso!");
                    return Ok(result);
                }
                else
                {
                    _logger.Warn($"Tentativa recuperar a campanha com usuario id :{UserId} - Não foi encontrado campanhas cadastradas no momento");
                    return NotFound($"Tentativa recuperar a campanha com usuario id :{UserId} - Não foi encontrado campanhas cadastradas no momento");
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar a campanha com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar a campanha com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}