using nps_backend_susana.Model.Entities;

namespace nps_backend_susana.Model.Interfaces

{
    public interface INpsLogRepository 
    {
        Task IncluirAsync(NpsLog entity);
    }
}
