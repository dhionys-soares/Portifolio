using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace testeidentity.Data.Mappings
{
    public class IdentityUserMap : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasNoKey();
        }
    }
}
