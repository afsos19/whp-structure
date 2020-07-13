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
    [Authorize]
    public class ProductController : MainController
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public ProductController(
            ILogger logger,
            IConfiguration configuration,
            IProductService productService) : base(configuration)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipantProduct()
        {
            try
            {
                var result = await _productService.GetParticipantProducts(Network);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFocusProduct()
        {
            try
            {
                var result = await _productService.GetFocusProducts(Network);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {

            try
            {

                var result = await _productService.GetAllCategories();

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"lista de categoria  Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"lista de categoria  Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {

            try
            {

                var result = await _productService.GetAllProducers();

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"lista de fabricante  Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"lista de fabricante  Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {

                var result = await _productService.GetAllProducts();

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"lista de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"lista de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromForm]ProductDto productDto, IFormFile formFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.DoSaveProduct(productDto, formFile);

                    if (!result)
                    {
                        _logger.Warn($"cadastro de produto Usuario com id {UserId} - Ocorreu um erro ao tenta cadastrar o produto");
                        return BadRequest("Ocorreu um erro ao tenta cadastrar o produto");
                    }
                    
                    return Ok("Produto cadastrado com sucesso!");
                }
                else
                {
                    _logger.Warn($"cadastro de produto Usuario com id {UserId} - campos obrigatórios não foram preenchidos");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"cadastro de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"cadastro de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromForm]ProductDto productDto, IFormFile formFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.DoUpdateProduct(productDto, formFile);

                    if (!result)
                    {
                        _logger.Warn($"edição de produto Usuario com id {UserId} - Ocorreu um erro ao tenta editar o produto");
                        return BadRequest("Ocorreu um erro ao tenta cadastrar o produto");
                    }

                    return Ok("Produto cadastrado com sucesso!");
                }
                else
                {
                    _logger.Warn($"edição de produto Usuario com id {UserId} - campos obrigatórios não foram preenchidos");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"edição de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"edição de produto Usuario com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            try
            {

                var result = await _productService.GetProductById(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"get de produto por id com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"get de produto por id com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

    }
}