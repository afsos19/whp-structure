using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Whp.MaisTop.Api.Extensions;
using NLog;

namespace Whp.MaisTop.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserStatusController : MainController
    {

        private readonly IUserStatusService _userStatusService;
        private readonly ILogger _logger;

        public UserStatusController(ILogger logger, IConfiguration configuration, IUserStatusService userStatusService) : base(configuration)
        {
            _userStatusService = userStatusService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userStatusService.GetAll();

                if (!result.Any())
                {
                    _logger.Info($"tentativa recuperar lista de status dos usuarios - nenhum item encontrado");
                    return NotFound();
                }
                    

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"tentativa recuperar lista de status dos usuarios - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"tentativa recuperar lista de status dos usuarios {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }


    }
}