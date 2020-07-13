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
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(Roles = "GESTOR DA INFORMACAO,ACESSO ESPELHO")]
    public class ImportationController : MainController
    {

        private readonly INetworkService _networkService;
        private readonly ILogger _logger;
        private readonly IHierarchyFileService _hierarchyFileService;
        private readonly ISaleFileService _saleFileService;

        public ImportationController(ILogger logger, IConfiguration configuration, INetworkService networkService, IHierarchyFileService hierarchyFileService, ISaleFileService saleFileService) : base(configuration)
        {
            _logger = logger;
            _networkService = networkService;
            _hierarchyFileService = hierarchyFileService;
            _saleFileService = saleFileService;
        }

        [HttpPost]
        public async Task<IActionResult> SendHierarchyFile([FromForm]FileStatusParamDto fileStatusParamDto, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.Warn($"Importação de arquivo de hierarquia - Arquivo não encontrado");
                    return NotFound("Arquivo não encontrado");
                }

                var messageReturn = await _hierarchyFileService.SendHierarchyFile(fileStatusParamDto, file, Network, UserId);

                _logger.Info($"Importação de arquivo de hierarquia - {messageReturn}");

                return Ok(messageReturn);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Importação de arquivo de hierarquia - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Importação de arquivo de hierarquia - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendSaleFile([FromForm]FileStatusParamDto fileStatusParamDto, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.Warn($"Importação de arquivo de venda - Arquivo não encontrado");
                    return NotFound("Arquivo não encontrado");
                }


                var messageReturn = await _saleFileService.SendSaleFile(fileStatusParamDto, file, Network, UserId);

                _logger.Info($"Importação de arquivo de venda - {messageReturn}");

                return Ok(messageReturn);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Importação de arquivo de venda - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"Importação de arquivo de venda - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetSaleFileStatus([FromBody] FileStatusParamDto fileStatusParamDto)
        {
            try
            {

                var fileStatus = await _saleFileService.GetFileStatus(fileStatusParamDto, Network);

                if (fileStatus == null)
                {
                    _logger.Warn($"status do arquivo de venda - Não encontramos nenhum arquivo importado no mes {fileStatusParamDto.CurrentMonth} e ano {fileStatusParamDto.CurrentYear}");
                    return NotFound($"Não encontramos nenhum arquivo importado no mes {fileStatusParamDto.CurrentMonth} e ano {fileStatusParamDto.CurrentYear}");
                }


                return Ok(fileStatus);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"status do arquivo de venda - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"status do arquivo de venda - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetHierarchyFileStatus([FromBody] FileStatusParamDto fileStatusParamDto)
        {
            try
            {
                var fileStatus = await _hierarchyFileService.GetFileStatus(fileStatusParamDto, Network);

                if (fileStatus == null)
                {
                    _logger.Warn($"status do arquivo de venda - Não encontramos nenhum arquivo importado no mes {fileStatusParamDto.CurrentMonth} e ano {fileStatusParamDto.CurrentYear}");
                    return NotFound($"Não encontramos nenhum arquivo importado no mes {fileStatusParamDto.CurrentMonth} e ano {fileStatusParamDto.CurrentYear}");
                }

                return Ok(fileStatus);
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"status do arquivo de hierarquia - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"status do arquivo de hierarquia - {ex.ToLogString(Environment.StackTrace)}");
            }
        }

    }
}