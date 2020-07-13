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
    public class ShopController : MainController
    {

        private readonly IShopService _shopService;

        public ShopController(IConfiguration configuration, IShopService shopService) : base(configuration)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShop()
        {
            try
            {
                var result = await _shopService.GetShop(UserId);

                return Ok(result);
           
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetShop(int userId)
        {
            try
            {
                var result = await _shopService.GetShop(userId);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}