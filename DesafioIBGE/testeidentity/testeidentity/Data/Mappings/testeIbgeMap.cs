using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using testeidentity.Models;

namespace testeidentity.Data.Mappings
{
    public class testeIbgeMap : IEntityTypeConfiguration<testeIbge>
    {
        public void Configure(EntityTypeBuilder<testeIbge> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Estado)
                .IsRequired();

            builder.Property(x => x.Cidade)
                .IsRequired();
        }
    }
}
