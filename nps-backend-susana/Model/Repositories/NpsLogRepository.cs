using nps_backend_susana.Model.Entities;
using nps_backend_susana.Model.Interfaces;
using nps_backend_susana.Services;

namespace nps_backend_susana.Model.Repositories
{
    public class NpsLogRepository : INpsLogRepository
    {
        private readonly Contexto _contexto;
         public NpsLogRepository(Contexto contexto) 
        {
            _contexto = contexto;
        }

        public async Task IncluirAsync(NpsLog entity)
        {
            await _contexto.NpsLog.AddAsync(entity);
            await _contexto.SaveChangesAsync();
        }
    }
}
