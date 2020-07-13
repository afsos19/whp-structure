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

namespace Whp.MaisTop.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class PhraseologyCategoryController : MainController
    {
        private readonly IPhraseologyCategoryService _phraseologyCategoryService;

        public PhraseologyCategoryController(IConfiguration configuration, IPhraseologyCategoryService phraseologyCategoryService) : base(configuration)
        {
            _phraseologyCategoryService = phraseologyCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhraseologyCategory(int id)
        {
            try
            {

                var result = await _phraseologyCategoryService.GetPhraseologyCategory(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhraseologyCategory()
        {
            try
            {
                var result = await _phraseologyCategoryService.GetAllPhraseologyCategories();

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePhraseologyCategory([FromBody] PhraseologyCategoryDto PhraseologyCategoryDto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (PhraseologyCategoryDto.Id == 0)
                        return NotFound();

                    var result = await _phraseologyCategoryService.UpdatePhraseologyCategory(PhraseologyCategoryDto);

                    if(!result.Updated)
                        return BadRequest($"Ocorreu um erro ao tentar atualizar a resposta com id {PhraseologyCategoryDto.Id}");

                    return Ok(result);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePhraseologyCategory([FromBody] PhraseologyCategoryDto PhraseologyCategoryDto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _phraseologyCategoryService.SavePhraseologyCategory(PhraseologyCategoryDto);

                    if (!result.Saved)
                        return BadRequest($"Ocorreu um erro ao tentar cadastrar um resposta");

                    return Ok(result);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}