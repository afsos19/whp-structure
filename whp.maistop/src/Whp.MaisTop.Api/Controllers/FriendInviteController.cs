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
    public class FriendInviteController : MainController
    {

        private readonly IFriendInviteService _friendInviteService;
        private readonly ILogger _logger;

        public FriendInviteController(
            ILogger logger,
            IConfiguration configuration,
            IFriendInviteService friendInviteService) : base(configuration)
        {
            _friendInviteService = friendInviteService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> DoCadUserInvited(AccessCodeUserInviteDto accessCodeUserInviteDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _friendInviteService.DoCadUserInvited(accessCodeUserInviteDto);

                    if (result.Saved)
                    {
                        _logger.Info($"Tentativa cadastrar usuario do convite de amigos :{accessCodeUserInviteDto.Cpf} - {result.Message}");
                        return Ok(result.Message);
                    }
                    else
                    {
                        _logger.Warn($"Tentativa cadastrar usuario do convite de amigos :{accessCodeUserInviteDto.Cpf} - {result.Message}");
                        return BadRequest(result.Message);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa cadastrar usuario do convite de amigos :{accessCodeUserInviteDto.Cpf} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa cadastrar usuario do convite de amigos :{accessCodeUserInviteDto.Cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetInvitedUsers()
        {
            try
            {
                var result = await _friendInviteService.GetInvitedUsers(UserId);

                if (result == null)
                    return NotFound();

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar a lista de usuario convidado com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar a lista de usuario convidado com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserAccessCode()
        {
            try
            {
                var result = await _friendInviteService.GetAccessCodeInvite(UserId,false);

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar o código do convite de amigos com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar o código do convite de amigos com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendAccessCodeInvite(AccessCodeInviteDto accessCodeInviteDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _friendInviteService.SendAccessCodeInvite(accessCodeInviteDto.Cellphone, UserId);

                    if (result.Sent)
                    {
                        _logger.Info($"Tentativa enviar codigo de convite com usuario id :{UserId} - {result.Message}");
                        return Ok(result.Message);
                    }
                    else
                    {
                        _logger.Warn($"Tentativa enviar codigo de convite com usuario id :{UserId} - {result.Message}");
                        return NotFound(result.Message);
                    }
                }
                else
                {
                    return BadRequest("Informe o celular do usuario!");
                }
                

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar o código do convite de amigos com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar o código do convite de amigos com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}