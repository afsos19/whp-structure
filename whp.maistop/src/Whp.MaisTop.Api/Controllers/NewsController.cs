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
    public class NewsController : MainController
    {

        private readonly INewsService _newsService;
        private readonly ILogger _logger;

        public NewsController(ILogger logger,IConfiguration configuration, INewsService newsService) : base(configuration)
        {
            _newsService = newsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var result = await _newsService.GetNews(Network, Office);

                if (!result.Any())
                {
                    _logger.Warn($"Tentativa recuperar as novidades com usuario id :{UserId} - novidades não encontrada");
                    return NotFound();
                }

                return Ok(result);
           
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar as novidades com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar as novidades com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}