using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

     
        // get all the movies belonging to that cast

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCast(int id)
        {
            var cast = await _castService.GetAllCast(id);

            if (cast == null)
            {
                return NotFound($"NO Cast Found for {id}");
            }

            return Ok(cast);
        }
    }
}
