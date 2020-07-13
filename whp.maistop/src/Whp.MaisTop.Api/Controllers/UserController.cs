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
    public class UserController : MainController
    {

        private readonly IUserService _userService;
        private readonly IUserStatusService _userStatusService;
        private readonly IHierarchyFileService _hierarchyFileService;
        private readonly INetworkService _networkService;
        private readonly IShopService _shopService;
        private readonly ILogger _logger;

        public UserController(ILogger logger, IHierarchyFileService hierarchyFileService, IUserStatusService userStatusService, IConfiguration configuration, IUserService userService, INetworkService networkService, IShopService shopService) : base(configuration)
        {
            _logger = logger;
            _hierarchyFileService = hierarchyFileService;
            _userStatusService = userStatusService;
            _userService = userService;
            _networkService = networkService;
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<IActionResult> SendAccessCodeExpiration()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.SendAccessCodeExpiration(UserId);

                    if (result.Sent)
                    {
                        _logger.Info($"Tentativa de envio de codigo expiracao com usuario id :{UserId} - {result.Message}");
                        return Ok(result.Message);
                    }
                    else
                    {
                        _logger.Warn($"Tentativa de envio de codigo expiracao com usuario id :{UserId} - {result.Message}");
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
                    _logger.Fatal($"Tentativa de envio de codigo expiracao com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de envio de codigo expiracao com usuario id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserExpiredPassword([FromBody] ExpiredPasswordDto expiredPasswordDto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _userService.UpdateUserExpiredPassword(UserId, expiredPasswordDto.Password, expiredPasswordDto.Token);

                    if (result.updated)
                        return Ok(new { message = result.returnMessage, result.user });
                    else
                        return NotFound(result.returnMessage);
                }
                else
                    return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                return BadRequest($"Tentativa de tualizacao de senha expirada de usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _userService.UpdateUser(user, null);

                    if (result.updated)
                        return Ok(new { message = result.returnMessage, result.user });
                    else
                        return BadRequest(result.returnMessage);
                }
                else
                    return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de tualizacao de usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de tualizacao de usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm] UserDto user, IFormFile file)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _userService.UpdateUser(user, file);

                    if (result.updated)
                        return Ok(new { message = result.returnMessage, result.user });
                    else
                        return BadRequest(result.returnMessage);
                }
                else
                    return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de tualizacao de usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de tualizacao de usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{PrivacityPolicy}")]
        public async Task<IActionResult> UpdatePrivacityPolicy(bool PrivacityPolicy)
        {
            try
            {

                var result = await _userService.UpdatePrivacityPolicy(UserId, PrivacityPolicy);

                if (!result)
                {
                    _logger.Warn($"Tentativa de tualizacao da politica de privacidade com id :{UserId} - Não foi possivel atualizar as politicas de privacidade");
                    return BadRequest("Não foi possivel atualizar as politicas de privacidade");
                }

                _logger.Info($"Tentativa de tualizacao da politica de privacidade com id :{UserId} - Politica de privacidade atualizada com sucesso");
                return Ok("Politica de privacidade atualizada com sucesso");

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de tualizacao da politica de privacidade com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de tualizacao da politica de privacidade com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "GERENTE,GERENTE REGIONAL")]
        public async Task<IActionResult> GetTeamUsers([FromBody] FilterMyTeamDto filterDto)
        {
            try
            {
                var result = await _userService.GetTeamUsers(filterDto.Shop, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (result.UserList.Any())
                {
                    _logger.Info($"Tentativa recuperar time de usuarios atravez do usuario com id :{UserId} - encontrado com sucesso!");
                    return Ok(result);
                }
                else
                {
                    _logger.Warn($"Tentativa recuperar time de usuarios atravez do usuario com id :{UserId} - Nenhum usuario encontrado!");
                    return NotFound("Nenhum usuario encontrado!");
                }


            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar time de usuarios atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar time de usuarios atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {

                if (userId == 0)
                {
                    _logger.Warn($"Tentativa recuperar usuario atravez do usuario com id :{UserId} - id: {userId} inválido");
                    return NotFound();
                }

                var user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    _logger.Warn($"Tentativa recuperar usuario atravez do usuario com id :{UserId} - usuario não encontrado id: {userId}");
                    return NotFound();
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar usuario atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar usuario atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> ExportUsersToExcel()
        {
            try
            {
                var file = await _userService.ExportUsersToExcel();

                return File(file,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","report_geral_usuarios.xlsx");

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de exportar excel geral de todos os usuarios - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de exportar excel geral de todos os usuarios - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserStatusLog(int userId)
        {
            try
            {

                if (userId == 0)
                {
                    _logger.Warn($"Tentativa recuperar lista de status log  atravez do usuario com id :{UserId} - id: {userId} inválido");
                    return NotFound();
                }

                var userStatusList = await _userService.GetUserStatusLogList(userId);

                if (userStatusList == null)
                {
                    _logger.Warn($"Tentativa recuperar lista de status log  atravez do usuario com id :{UserId} - nenhum historico encontrado com usuario id: {userId}");
                    return NotFound();
                }

                return Ok(userStatusList);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar lista de status log usuario atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar lista de status log  atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserListAdmin(UserFilterDto userFilterDto)
        {
            try
            {
              
                var user = await _userService.GetUsersToAdmin(userFilterDto);

                return Ok(user);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar lista de usuario no admin atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar lista de  usuario no admin atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetUserAdmin(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                {
                    _logger.Warn($"Tentativa recuperar usuario no admin atravez do usuario com id :{UserId} - Não foi informado o cpf corretamente cpf: {cpf}");
                    return NotFound("Não foi informado o cpf corretamente.");
                }

                var user = await _userService.GetUserAccessByCpf(cpf);
                var userLog = await _userStatusService.GetUserLog(user.Id);

                if (user.Id == 0)
                {
                    _logger.Warn($"Tentativa recuperar usuario no admin atravez do usuario com id :{UserId} - usuario não encontrado cpf: {cpf}");
                    return NotFound();
                }


                var network = await _networkService.GetById(user.Network);
                var shop = await _shopService.GetById(user.Shop);
                var hierarchy = await _hierarchyFileService.GetFileDataByCnpj(shop.Cnpj);

                _logger.Info($"Tentativa recuperar usuario no admin atravez do usuario com id :{UserId} - usuario encontrado com sucesso cpf: {cpf}");

                return Ok(new
                {
                    user,
                    userLog,
                    hierarchy,
                    network,
                    shop
                });

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa recuperar usuario no admin atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa recuperar usuario no admin atravez do usuario com id :{UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}