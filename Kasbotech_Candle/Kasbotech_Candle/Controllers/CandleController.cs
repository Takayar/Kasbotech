using Kasbotech_Candle.Model.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kasbotech_Candle.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CandleController : Controller
    {
        private readonly ICandleService _candleService;

      

        public CandleController(ICandleService candleService)
        {
            _candleService = candleService;
        }
     
        [HttpPost]
        public IActionResult Post([FromBody] CandleDto candleDto)
        {
            _candleService.AddNewCandle(candleDto);
            return Ok();
        }
    }
}
