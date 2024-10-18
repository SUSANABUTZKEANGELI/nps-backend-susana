using nps_backend_susana.Model.Dtos;

namespace nps_backend_susana.Model.Interfaces

{
    public interface INpsLogService
    {
        Task<string> BuscarPergunta();

        Task<bool> SalvarResposta(ScoreDto scoreDto);
    }
}
