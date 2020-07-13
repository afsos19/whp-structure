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
using NLog;
using Whp.MaisTop.Api.Extensions;

namespace Whp.MaisTop.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : MainController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IShopService _shopService;
        private readonly ILogger _logger;

        public AuthenticationController(
            IConfiguration configuration,
            IShopService shopService,
            IAuthenticationService authenticationService,
            ILogger logger,
            IUserService userService) : base(configuration)
        {
            _userService = userService;
            _logger = logger;
            _authenticationService = authenticationService;
            _shopService = shopService;
        }


        [HttpGet]
        [Authorize(Roles = "VENDEDOR,GERENTE,GERENTE REGIONAL,GESTOR DA INFORMACAO,PREMIO IDEAL")]
        public async Task<IActionResult> ShopAuthenticate()
        {
            try
            {


                var url = "";

                if (UserId > 0)
                    url = await _authenticationService.GetShopAuthenticate(UserId, Network);

                if (string.IsNullOrEmpty(url))
                    _logger.Warn($"Usuario com id {UserId} tentou acessar a loja e ocorreu um problema com a geração do link");
                else
                    _logger.Info($"Usuario com id {UserId} acessou a loja");

                return Ok(url);

            }
            catch
            {

#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} - não conseguiu efetuar o SSO entre em contato com o adm");
#endif
                return BadRequest($"Usuario com id {UserId} - não conseguiu efetuar o SSO entre em contato com o adm");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> TrainingAcademyAuthenticate()
        {
            try
            {
                var url = await _authenticationService.GetTrainingAuthenticate(UserId, Network, "");

                if (string.IsNullOrEmpty(url))
                    _logger.Warn($"Usuario com id {UserId} tentou acessar a plataforma de treinamento e ocorreu um problema com a geração do link");
                else
                    _logger.Info($"Usuario com id {UserId} acessou a plataforma de treinamento");

                return Ok(url);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> ForgotPassword(string cpf)
        {
            try
            {

                var result = await _authenticationService.ForgotPassword(cpf);

                if (result.sent)
                {
                    _logger.Info($"Tentativa de recuperacao de senha com cpf {cpf} - {result.message}");
                    return Ok(new { result.message });
                }
                else
                {
                    _logger.Warn($"Tentativa de recuperacao de senha com cpf {cpf} - {result.message}");
                    return NotFound(result.message);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de recuperacao de senha com cpf {cpf} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de recuperacao de senha com cpf {cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAccessCode([FromBody]UserDto user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.CellPhone))
                {
                    _logger.Warn($"Tentativa de gerar codigo de acesso com telefone {user.CellPhone} - Obrigatório enviar um numero de dispositivo movel");
                    return NotFound("Obrigatório enviar um numero de dispositivo movel");
                }

                var result = await _authenticationService.SendSMSAccessCodeConfirmation(user);

                if (result.sent)
                {
                    _logger.Info($"Tentativa de gerar codigo de acesso com telefone {user.CellPhone} - {result.message}");
                    return Ok(new { result.message, result.code });
                }
                else
                {
                    _logger.Warn($"Tentativa de gerar codigo de acesso com telefone {user.CellPhone} - {result.message}");
                    return BadRequest(result.message);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de gerar codigo de acesso com telefone {user.CellPhone} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de gerar codigo de acesso com telefone {user.CellPhone} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> FirstAccess(string cpf)
        {
            try
            {

                var result = await _authenticationService.FirstAccess(cpf);

                if (result.found)
                {
                    var shops = await _shopService.GetShop(result.user.Id);
                    _logger.Info($"Tentativa de primeiro acesso com cpf {cpf} - {result.message}");
                    return Ok(new { result.message, result.user, shops });
                }
                else
                {
                    _logger.Warn($"Tentativa de primeiro acesso com cpf {cpf} - {result.message}");
                    return NotFound(result.message);
                }


            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de primeiro acesso com cpf {cpf} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Tentativa de primeiro acesso com cpf {cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [Authorize]
        [HttpGet("{cpf}")]
        public async Task<IActionResult> MirrorAccessByCpf(string cpf)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                    {
                        _logger.Warn($"Usuario com id {UserId} tentou acesso espelho - Não foi informado o cpf corretamente cpf: {cpf}");
                        return NotFound("Não foi informado o cpf corretamente.");
                    }

                    var result = await _userService.GetUserAccessByCpf(cpf);

                    if (result.Id == 0) {
                        _logger.Warn($"Usuario com id {UserId} tentou acesso espelho - Usuario não encontrado cpf: {cpf}");
                        return NotFound("Usuario não encontrado");
                    }
                        

                    _logger.Info($"Usuario com id {UserId} entrou em acesso espelho com o cpf {cpf}");
                    return Ok(new { message = "Token de acesso espelho gerado", token = GenerateJwtToken(result, true) });

                }
                else
                {
                    _logger.Warn($"Usuario com id {UserId} tentou entrar em acesso espelho com cpf {cpf} e foi rejeitado pela validação");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Usuario com id {UserId} {ex.ToLogString(Environment.StackTrace)}");
            }

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> MirrorAccessById(int id)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _logger.Warn($"Usuario com id {UserId} tentou acesso espelho - Não foi informado o id corretamente");
                        return NotFound("Não foi informado o cpf corretamente.");
                    }

                    var result = await _userService.GetUserAccessById(id);

                    if (result.Id == 0)
                    {
                        _logger.Warn($"Usuario com id {UserId} tentou acesso espelho - Usuario não encontrado ");
                        return NotFound("Usuario não encontrado");
                    }


                    _logger.Info($"Usuario com id {UserId} entrou em acesso espelho com o id {id}");
                    return Ok(new { message = "Token de acesso espelho gerado", token = GenerateJwtToken(result, true) });

                }
                else
                {
                    _logger.Warn($"Usuario com id {UserId} tentou entrar em acesso espelho com id {id} e foi rejeitado pela validação");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Usuario com id {UserId} {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Usuario com id {UserId} {ex.ToLogString(Environment.StackTrace)}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _authenticationService.DoLogin(model, "");

                    if (result.authenticated)
                    {
                        _logger.Info($"Tentativa de login com cpf {model.Cpf} - {result.messageReturning}");

                        await _authenticationService.DoSaveUserAccessLog(model.Cpf, result.messageReturning, model.Ip, Device);

                        return Ok(new { message = result.messageReturning, token = GenerateJwtToken(result.user) });
                    }
                    else if (!result.authenticated && !string.IsNullOrEmpty(result.link))
                    {
                        await _authenticationService.DoSaveUserAccessLog(model.Cpf, result.messageReturning, model.Ip, Device);

                        _logger.Info($"Tentativa de login com cpf {model.Cpf} - {result.messageReturning}");
                        return Ok(new { message = result.messageReturning, result.link });
                    }
                    else
                    {
                        await _authenticationService.DoSaveUserAccessLog(model.Cpf, result.messageReturning, model.Ip, Device);

                        _logger.Warn($"Tentativa de login com cpf {model.Cpf} - {result.messageReturning}");
                        return NotFound(result.messageReturning);
                    }

                }
                else
                {
                    _logger.Warn($"Tentativa de login com cpf {model.Cpf} rejeitada pela validação");
                    return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {
                return BadRequest($"Tentativa de login com cpf {model.Cpf} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> PreRegistration([FromForm] UserDto user, IFormFile file)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _authenticationService.DoPreRegistration(user, file);

                    if (result.authenticated)
                    {
                        _logger.Info($"Tentativa de pre cadastro com cpf {user.CPF} - {result.messageReturning}");
                        return Ok(new { message = result.messageReturning, token = GenerateJwtToken(result.user) });
                    }
                    else
                    {
                        _logger.Warn($"Tentativa de pre cadastro com cpf {user.CPF} - {result.messageReturning}");
                        return BadRequest(result.messageReturning);
                    }

                }
                else
                {
                    _logger.Warn($"Tentativa de pre cadastro com cpf {user.CPF} e foi barrado pela validação");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa de pre cadastro com cpf {user.CPF} {ex.ToLogString(Environment.StackTrace)}");
#endif

                return BadRequest($"Tentativa de pre cadastro com cpf {user.CPF} {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}