using nps_backend_susana.Model.Dtos;
using nps_backend_susana.Model.Responses;

namespace nps_backend_susana.Model.Interfaces

{
    public interface INpsLogService
    {
        Task<NpsResponse> BuscarPergunta(string login);

        Task<NpsResponse> SalvarResposta(ScoreDto scoreDto);
    }
}
