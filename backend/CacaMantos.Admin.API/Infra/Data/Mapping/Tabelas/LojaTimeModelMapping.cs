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

            builder.Property(l => l.IdTime).HasColumnName("id_time");
            builder.HasOne(lt => lt.Time)
                .WithMany(t => t.Lojas)
                .HasForeignKey(lt => lt.IdTime);

            builder.Property(l => l.IdLoja).HasColumnName("id_loja");
            builder.HasOne(lt => lt.Loja)
                .WithMany(l => l.Times)
                .HasForeignKey(lt => lt.IdLoja);
        }
    }
}
