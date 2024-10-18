using nps_backend_susana.Model.Entities;

namespace nps_backend_susana.Model.Interfaces

{
    public interface INpsLogRepository 
    {
        Task <NpsLog> IncluirAsync(NpsLog entity);
    }
}
