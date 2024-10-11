using Microsoft.AspNetCore.Mvc;
using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Services;

namespace nps_backend_susana.Controllers
{
    [Route("api/nps")]
    [ApiController]
    public class NpsLogController : ControllerBase
    {
        private readonly NpsLogService _logService;

        public NpsLogController(NpsLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPergunta()
        {
            var result = await _logService.BuscarPergunta();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarResposta([FromBody] ScoreDto scoreDto)
        {
            if (scoreDto == null)
            {
                return BadRequest("Nota inválida. Tente de novo");
            }

            if (scoreDto.Score <= 6 && 
               (scoreDto.Category == null || scoreDto.Category == ""))
            {
                return BadRequest("Categoria obrigatória");
            }

            try
            {
                var result = await _logService.SalvarResposta(scoreDto);
                if (result)
                {
                    return Ok("Nota e log salvos com sucesso.");
                }

                return StatusCode(500, "Erro ao salvar a nota.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}