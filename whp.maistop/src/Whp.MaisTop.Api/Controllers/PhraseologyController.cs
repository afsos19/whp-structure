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
    public class PhraseologyController : MainController
    {
        private readonly IPhraseologyService _phraseologyService;

        public PhraseologyController(IConfiguration configuration, IPhraseologyService phraseologyService) : base(configuration)
        {
            _phraseologyService = phraseologyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhraseology(int id)
        {
            try
            {

                var result = await _phraseologyService.GetPhraseology(id);

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
        public async Task<IActionResult> GetAllPhraseology()
        {
            try
            {
                var result = await _phraseologyService.GetAllPhraseologies();

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
        public async Task<IActionResult> UpdatePhraseology([FromBody] PhraseologyDto PhraseologyDto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (PhraseologyDto.Id == 0)
                        return NotFound();

                    var result = await _phraseologyService.UpdatePhraseology(PhraseologyDto);

                    if(!result.Updated)
                        return BadRequest($"Ocorreu um erro ao tentar atualizar a resposta com id {PhraseologyDto.Id}");

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
        public async Task<IActionResult> SavePhraseology([FromBody] PhraseologyDto PhraseologyDto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _phraseologyService.SavePhraseology(PhraseologyDto);

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