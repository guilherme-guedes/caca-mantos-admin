using backend.Domain.Model;
using backend.Infra.Data.Postgre.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data.Postgre
{
    public class ContextoBancoPostgres : DbContext
    {
        public DbSet<TimeModel> Times {get;set;}
        public DbSet<LojaModel> Lojas {get;set;}
        public DbSet<LojaTimeModel> LojasTimes {get;set;}

        public ContextoBancoPostgres(DbContextOptions<ContextoBancoPostgres> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoBancoPostgres).Assembly);
        }
    }
}