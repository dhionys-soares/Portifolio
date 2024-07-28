using DesafioIbge.Data.Mappings;
using DesafioIbge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DesafioIbge.Data
{
    public class DataContext(DbContextOptions options) : IdentityDbContext(options)
    {       

        public DbSet<Ibge> Ibges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IbgeMap());
        }
    }
}
