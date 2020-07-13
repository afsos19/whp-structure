using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OfficeController : MainController
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IConfiguration configuration, IOfficeService officeService) : base(configuration)
        {
            _officeService = officeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var offices = await _officeService.GetAllOffices();

                if (!offices.Any())
                    return NotFound();

                return Ok(offices);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

