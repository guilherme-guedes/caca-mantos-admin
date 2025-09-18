using backend.Infra.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infra.Data.Mapping.Tabelas
{
    public class LojaModelMapping : IEntityTypeConfiguration<LojaModel>
    {
        public void Configure(EntityTypeBuilder<LojaModel> builder)
        {
            builder.ToTable("lojas");

            builder.HasKey(l => l.id);
            builder.Property(l => l.id)
                .HasColumnName("id");

            builder.Property(l => l.nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(l => l.site)
                .HasColumnName("site")
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(l => l.urlBusca)
                .HasColumnName("url_busca_time")
                .IsRequired()
                .HasMaxLength(130);

            builder.Property(l => l.parceira)
                .HasColumnName("parceira")
                .HasDefaultValue(false);

            builder.Property(l => l.ativa)
                .HasColumnName("ativa")
                .HasDefaultValue(true);
                
            builder.HasMany(l => l.times)
                .WithOne(t => t.loja)
                .HasForeignKey(lt => lt.idTime)
                .OnDelete(DeleteBehavior.Cascade);;
        }
    }
}