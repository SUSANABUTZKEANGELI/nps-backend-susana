using Microsoft.EntityFrameworkCore;
using nps_backend_susana.Model.Entities;
using nps_backend_susana.Services.Maps;

namespace nps_backend_susana.Services
{
    public class Contexto : DbContext
    {
        public DbSet<NpsLog> NpsLog { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NpsLogMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
