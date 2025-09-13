using backend.Infra.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data
{
    public class ContextoBanco : DbContext
    {
        public DbSet<TimeModel> Times {get;set;}
        public DbSet<LojaModel> Lojas {get;set;}
        public DbSet<LojaTimeModel> LojasTimes {get;set;}

        public ContextoBanco(DbContextOptions<ContextoBanco> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("admin");

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoBanco).Assembly);
        }
    }
}