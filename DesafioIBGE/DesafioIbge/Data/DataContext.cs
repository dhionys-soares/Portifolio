using DesafioIbge.Data.Mappings;
using DesafioIbge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNet.Identity;

namespace DesafioIbge.Data
{
    public class DataContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:dhionysserver.database.windows.net,1433;Initial Catalog=Desafiov2;Persist Security Info=False;User ID=dhionys;Password=*$8)uoh5xZ)6HZ-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<Ibge> Ibges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IbgeMap());
        }
    }
}
