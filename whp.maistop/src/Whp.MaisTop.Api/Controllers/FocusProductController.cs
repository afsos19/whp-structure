﻿using System;
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
    public class FocusProductController : MainController
    {
        private readonly ILogger _logger;
        private readonly IFocusProductService _productService;

        public FocusProductController(
            ILogger logger,
            IConfiguration configuration,
            IFocusProductService productService) : base(configuration)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFocusProduct(int id)
        {

            try
            {

                var result = await _productService.DoDeleteFocusProduct(id);

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
        public async Task<IActionResult> GetFocusProducts(FilterDto filterDto)
        {

            try
            {

                var result = await _productService.GetFocusProducts(filterDto.Network, filterDto.CurrentMonth, filterDto.CurrentYear);

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
        public async Task<IActionResult> SaveFocusProduct([FromBody]FocusProductDto focusProductDto )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.DoSaveFocusProduct(focusProductDto);

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
        public async Task<IActionResult> UpdateProduct([FromBody]FocusProductDto focusProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.DoUpdateFocusProduct(focusProductDto);

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
        public async Task<IActionResult> GetFocusProductById(int id)
        {

            try
            {

                var result = await _productService.GetFocusProductById(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
#if (!DEBUG)
                    _logger.Fatal($"get de produto super top por id com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
#endif
                return BadRequest($"get de produto super top por id com id {UserId} - {ex.ToLogString(Environment.StackTrace)}");
            }

        }

    }
}