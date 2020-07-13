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
    public class OccurrenceController : MainController
    {
        private readonly IOccurrenceService _occurenceService;

        public OccurrenceController(IConfiguration configuration, IOccurrenceService occurenceService) : base(configuration)
        {
            _occurenceService = occurenceService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOccurrenceMessageType()
        {
            try
            {
                var result = await _occurenceService.GetAllOccurrenceMessageType();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOccurrenceStatus()
        {
            try
            {
                var result = await _occurenceService.GetAllOccurrenceStatus();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOccurrenceSubject()
        {
            try
            {
                var result = await _occurenceService.GetAllOccurrenceSubject();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOccurrence(int id)
        {
            try
            {

                var result = await _occurenceService.GetOccurrence(id);

                if (result.Id == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetOccurrenceAdminEai([FromBody] OccurrenceaAdminFilterDto occurrenceaAdminFilterDto)
        {
            try
            {
                var result = await _occurenceService.GetOccurrenceAdminEai(occurrenceaAdminFilterDto);

                if (result.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhuma ocorrencia encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetOccurrenceAdmin([FromBody] OccurrenceaAdminFilterDto occurrenceaAdminFilterDto)
        {
            try
            {
                var result = await _occurenceService.GetOccurrenceAdmin(occurrenceaAdminFilterDto);

                if (result.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhuma ocorrencia encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOccurrenceByUser()
        {
            try
            {

                var result = await _occurenceService.GetOccurrenceByUser(UserId);

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{occurrenceId}")]
        public async Task<IActionResult> GetMessagesOccurenceByUser(int occurrenceId)
        {
            try
            {

                var result = await _occurenceService.GetMessagesOccurenceByUser(occurrenceId);

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{occurrenceId}")]
        public async Task<IActionResult> GetMessagesOccurenceByUserEai(int occurrenceId)
        {
            try
            {

                var result = await _occurenceService.GetMessagesOccurenceByUserEai(occurrenceId);

                if (!result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromForm] OccurrenceMessageDto occurrenceMessageDto, IFormFile formFile)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _occurenceService.SaveMessage(occurrenceMessageDto, formFile, UserId);

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

        [HttpPut]
        public async Task<IActionResult> UpdateOccurence([FromBody] OccurrenceDto occurrenceDto)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (occurrenceDto.Id == 0)
                        return NotFound();

                    var result = await _occurenceService.UpdateOccurrence(occurrenceDto);

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
        public async Task<IActionResult> SaveOccurrence([FromForm] OccurrenceDto occurrenceDto, IFormFile formFile)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _occurenceService.SaveOccurrence(occurrenceDto, formFile, UserId);

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