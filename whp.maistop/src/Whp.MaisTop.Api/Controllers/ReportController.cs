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
    public class ReportController : MainController
    {

        private readonly IReportService _reportService;
        private readonly ISaleFileService _saleFileService;
        private readonly ILogger _logger;

        public ReportController(ISaleFileService saleFileService, ILogger logger,IConfiguration configuration, IReportService reportService) : base(configuration)
        {
            _reportService = reportService;
            _saleFileService = saleFileService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PreProcessingSalesPunctuation(FilterDto filterDto)
        {
            try
            {
                var result = await _reportService.PreProcessingSalesPunctuation(filterDto);

                if (result.hasReport)
                {
                    _logger.Info($"Tentativa gerar relatório de pre processamento de pontuacao de venda com usuario id :{UserId} - relatorio gerado com sucesso!");
                    return Ok(result.report);
                }
                else
                {
                    _logger.Warn($"Tentativa gerar relatório de pre processamento de pontuacao de venda com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} ");
                    return NotFound(result.message);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa gerar relatório de pre processamento de pontuacao  de venda com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} ");
#endif
                return BadRequest($"Tentativa gerar relatório de pre processamento de pontuacao  de venda com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PreProcessingSales(FilterDto filterDto)
        {
            try
            {
                var result = await _reportService.PreProcessingSales(filterDto);

                if (result.hasReport)
                {
                    _logger.Info($"Tentativa gerar relatório de pre processamento de venda com usuario id :{UserId} - relatorio gerado com sucesso!");
                    return Ok(result.report);
                }
                else
                {
                    _logger.Warn($"Tentativa gerar relatório de pre processamento de venda com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network}");
                    return NotFound(result.message);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa gerar relatório de pre processamento de venda com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network}");
#endif
                return BadRequest($"Tentativa gerar relatório de pre processamento de venda com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BaseTrackingHierarchy(FilterBaseTrackingDto filterDto)
        {
            try
            {
                var result = await _reportService.GetHierarchyFile(filterDto);

                if (result.hasReport)
                {
                    _logger.Info($"Tentativa gerar relatório acompanhamento de bases com usuario id :{UserId} - relatorio gerado com sucesso!");
                    return Ok(result.report);
                }
                else
                {
                    _logger.Warn($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
                    return NotFound($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
#endif
                return BadRequest($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BaseTrackingSale(FilterBaseTrackingDto filterDto)
        {
            try
            {
                var result = await _reportService.GetSaleFile(filterDto);

                if (result.hasReport)
                {
                    _logger.Info($"Tentativa gerar relatório acompanhamento de bases com usuario id :{UserId} - relatorio gerado com sucesso!");
                    return Ok(result.report);
                }
                else
                {
                    _logger.Warn($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
                    return NotFound($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - Não foi encontrado registro no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
#endif
                return BadRequest($"Tentativa gerar relatório acompanhamento de bases com usuario id:{UserId} - {ex.ToLogString(Environment.StackTrace)} - no mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {filterDto.Network} e status {filterDto.FileStatusId}");
            }
        }

    }
}