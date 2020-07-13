using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LogsPunctuationController : MainController
    {
        private readonly ILogger _logger;
        private readonly ILogsPunctuationService _logsPunctuationService;
        public LogsPunctuationController(IConfiguration configuration, ILogger logger, ILogsPunctuationService logsPunctuationService) : base(configuration)
        {
            _logger = logger;
            _logsPunctuationService = logsPunctuationService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult HourPunctuationLogs()
        {
            try
            {
                var logs = _logsPunctuationService.GetLastHourLogsPunctuation();
                _logger.Info(logs.Message + " - Créditos: " + logs.Credits.ToString() + " - Débitos: " + logs.Debits);
                return Ok(logs);
            }
            catch (Exception e)
            {
                return BadRequest("Falha na chamada dos logs de pontuação: " + e.Message);
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult DailyPunctuationLogs()
        {
            try
            {
                var logs = _logsPunctuationService.GetDailyLogsPunctuation();
                _logger.Info(logs.Message + " - Créditos: " + logs.Credits.ToString() + " - Débitos: " + logs.Debits);
                return Ok(logs);
            }
            catch (Exception e)
            {
                return BadRequest("Falha na chamada dos logs de pontuação: " + e.Message);
            }

        }
    }
}