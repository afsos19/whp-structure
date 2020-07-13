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
    public class PhraseologyTypeSubjectController : MainController
    {
        private readonly IPhraseologyTypeSubjectService _phraseologyService;

        public PhraseologyTypeSubjectController(IConfiguration configuration, IPhraseologyTypeSubjectService phraseologyService) : base(configuration)
        {
            _phraseologyService = phraseologyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhraseologyTypeSubject(int id)
        {
            try
            {

                var result = await _phraseologyService.GetPhraseologyTypeSubject(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhraseologyTypeSubjectBySubjectId(int id)
        {
            try
            {

                var result = await _phraseologyService.GetPhraseologyTypeSubjectsBySubjectId(id);

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhraseologyTypeSubject()
        {
            try
            {
                var result = await _phraseologyService.GetAllPhraseologyTypeSubjects();

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
        public async Task<IActionResult> UpdatePhraseologyTypeSubject([FromBody] PhraseologyTypeSubjectDto PhraseologyDto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (PhraseologyDto.Id == 0)
                        return NotFound();

                    var result = await _phraseologyService.UpdatePhraseologyTypeSubject(PhraseologyDto);

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
        public async Task<IActionResult> SavePhraseologyTypeSubject([FromBody] PhraseologyTypeSubjectDto PhraseologyDto)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _phraseologyService.SavePhraseologyTypeSubject(PhraseologyDto);

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