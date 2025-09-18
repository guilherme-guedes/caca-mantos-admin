using backend.Infra.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infra.Data.Mapping.Tabelas
{
    public class LojaTimeModelMapping: IEntityTypeConfiguration<LojaTimeModel>
    {
        public void Configure(EntityTypeBuilder<LojaTimeModel> builder)
        {
            builder.ToTable("times_lojas");

            builder.HasKey(lt => lt.id);
            builder.Property(lt => lt.id)
                .HasColumnName("id");

            builder.Property(l => l.idTime).HasColumnName("id_time");
            builder.HasOne(lt => lt.time)
                .WithMany(t => t.lojas)
                .HasForeignKey(lt => lt.idTime);

            builder.Property(l => l.idLoja).HasColumnName("id_loja");
            builder.HasOne(lt => lt.loja)
                .WithMany(l => l.times)
                .HasForeignKey(lt => lt.idLoja);
        }
    }
}