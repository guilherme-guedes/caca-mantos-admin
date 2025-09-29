using CacaMantos.Admin.API.Infra.Data.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CacaMantos.Admin.API.Infra.Data.Mapping.Tabelas
{
    public class LojaTimeModelMapping : IEntityTypeConfiguration<LojaTimeModel>
    {
        public void Configure(EntityTypeBuilder<LojaTimeModel> builder)
        {
            builder.ToTable("times_lojas");

            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Id)
                .HasColumnName("id");

            builder.Property(lt => lt.IdTime).HasColumnName("id_time");                
            builder.HasOne(tl => tl.Time)
                .WithMany(t => t.Lojas)
                .HasForeignKey(tl => tl.IdTime)
                .OnDelete(DeleteBehavior.Cascade); 
            
            builder.Property(lt => lt.IdLoja).HasColumnName("id_loja");
            builder.HasOne(tl => tl.Loja)
                .WithMany(l => l.Times)
                .HasForeignKey(tl => tl.IdLoja)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
