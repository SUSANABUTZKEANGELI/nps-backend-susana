using Microsoft.EntityFrameworkCore;
using nps_backend_susana.Model.Entities;
using nps_backend_susana.Services.Maps;
using System.Reflection;

namespace nps_backend_susana.Services
{
    public class Contexto : DbContext
    {
        public DbSet<NpsLog> NpsLog { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {

        }

        public Contexto() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NpsLogMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
