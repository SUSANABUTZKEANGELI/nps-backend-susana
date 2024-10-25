using Microsoft.AspNetCore.Mvc;
using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Model.Interfaces;

namespace nps_backend_susana.Controllers
{
    [Route("api/nps")]
    [ApiController]
    public class NpsLogController : ControllerBase
    {
        private readonly INpsLogService _logService;

        public NpsLogController(INpsLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPergunta([FromQuery] string login)
        {
            var result = await _logService.BuscarPergunta(login);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarResposta([FromBody] ScoreDto scoreDto)
        {
            var result = await _logService.SalvarResposta(scoreDto);
            return Ok(result);
        }
    }
}