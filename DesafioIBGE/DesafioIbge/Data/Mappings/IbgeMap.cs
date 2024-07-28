using DesafioIbge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioIbge.Data.Mappings
{
    public class IbgeMap : IEntityTypeConfiguration<Ibge>
    {
        public void Configure(EntityTypeBuilder<Ibge> builder)
        {
            builder.ToTable("Ibge");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
            .IsFixedLength(true)
            .HasMaxLength(7);

            builder.Property(x => x.State)
                .IsRequired(false)
                .HasColumnType("VARCHAR")
                .IsFixedLength()
                .HasMaxLength(2);

            builder.Property(x => x.City)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.HasIndex(x => x.Id, "IX_IBGE_Id");
            builder.HasIndex(x => x.State, "IX_IBGE_State");
            builder.HasIndex(x => x.City, "IX_IBGE_City");
        }
    }
}
