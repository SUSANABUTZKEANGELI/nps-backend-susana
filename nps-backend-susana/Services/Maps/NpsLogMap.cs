using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nps_backend_susana.Model.Entities;

namespace nps_backend_susana.Services.Maps
{
    internal class NpsLogMap : IEntityTypeConfiguration<NpsLog>
    {
        public void Configure(EntityTypeBuilder<NpsLog> builder)
        {
            builder.ToTable("NpsLog");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DateScore)
                .IsRequired();

            builder.Property(x => x.IdProduct)
                .IsRequired();

            builder.Property(x => x.Score)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
