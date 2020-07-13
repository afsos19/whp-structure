using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TrainingController : MainController
    {

        private readonly ITrainingService _trainingService;

        public TrainingController(IConfiguration configuration, ITrainingService trainingService) : base(configuration)
        {
            _trainingService = trainingService;
        }

      
        [HttpPost]
        [Authorize(Roles = "GERENTE,GERENTE REGIONAL")]
        public async Task<IActionResult> GetTeamSales([FromBody] FilterMyTeamDto filterDto)
        {
            try
            {
                var result = await _trainingService.GetTrainingTeamSales(filterDto.Shop, filterDto.CurrentMonth, filterDto.CurrentYear);

                if (result.TrainingList.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhum treinamento encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> GetTrainingManagersReport([FromBody] TrainingManagerFilterDto filterDto)
        {
            try
            {
                var result = await _trainingService.GetTrainingManagersReport(filterDto);

                if (result.Any())
                    return Ok(result);
                else
                    return NotFound("Nenhum registro encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}