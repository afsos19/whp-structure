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
    public class AdminValidationController : MainController
    {
     
        private readonly IProductService _productService;
        private readonly ISaleFileService _saleFileService;
        private readonly ILogger _logger;

        public AdminValidationController(ILogger logger ,IConfiguration configuration, IProductService productService, ISaleFileService saleFileService) : base (configuration)
        {
            _productService = productService;
            _saleFileService = saleFileService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSaleFileDataSku([FromBody] SaleFileDataDto saleFileDataDtos)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _saleFileService.UpdateSaleFileDataSku(saleFileDataDtos);

                    _logger.Info(result);

                    return Ok(result);
                }
                else
                {
                    _logger.Warn($"tentativa atualizacao arquivo venda data sku Usuario com id {UserId}  - Objeto inválido informe o id do arquivo de venda data e o produto");
                    return BadRequest(ModelState);
                }
                

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"tentativa atualizacao arquivo venda data sku Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"tentativa atualizacao arquivo venda data sku Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetPendingClassification([FromBody] FilterDto filterDto)
        {

            try
            {

                var result = await _saleFileService.GetPendingClassification(filterDto);
                
                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"tentativa atualizacao arquivo venda data sku Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"tentativa atualizacao arquivo venda data sku Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ApproveFiles(FilterDto filterDto)
        {

            try
            {

                var result = await _saleFileService.DoApprove(filterDto);

                if (result)
                {
                    _logger.Info($"tentativa aprovação arquivo de venda com o usuario {UserId} com os parametros de mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {Network} - files aprovado com sucesso, aguardando processamento");
                    return Ok($"tentativa aprovação arquivo de venda com o usuario {UserId} com os parametros de mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {Network} - files aprovado com sucesso, aguardando processamento");
                }
                else
                {
                    _logger.Warn($"tentativa aprovação arquivo de venda com o usuario {UserId} com os parametros de mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {Network} - não foi possivel localizar arquivos nos parametros informado");
                    return NotFound("Não foi possivel localizar arquivos nos parametros informado");
                }
                    

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"tentativa aprovação arquivo de venda com o usuario {UserId} com os parametros de mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {Network} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"tentativa aprovação arquivo de venda com o usuario {UserId} com os parametros de mes {filterDto.CurrentMonth} ano {filterDto.CurrentYear} e rede {Network} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> GetProducts(GetProductDto getProductDto)
        {

            try
            {
                var result = await _productService.GetProductByCategoryAndProducer(
                    getProductDto.CategoryProductId, 
                    getProductDto.ProducerId);

                if(!result.Any())
                    return NotFound("Nenhum produto encontrado");

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"lista de produtos por categoria e fabricante no admin Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"lista de produtos por categoria e fabricante no admin Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

        
    }
}