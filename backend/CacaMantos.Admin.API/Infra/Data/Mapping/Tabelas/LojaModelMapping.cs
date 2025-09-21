using CacaMantos.Admin.API.Infra.Data.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CacaMantos.Admin.API.Infra.Data.Mapping.Tabelas
{
    public class LojaModelMapping : IEntityTypeConfiguration<LojaModel>
    {
        public void Configure(EntityTypeBuilder<LojaModel> builder)
        {
            builder.ToTable("lojas");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasColumnName("id");

            builder.Property(l => l.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(l => l.Site)
                .HasColumnName("site")
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(l => l.UrlBusca)
                .HasColumnName("url_busca_time")
                .IsRequired()
                .HasMaxLength(130);

            builder.Property(l => l.Parceira)
                .HasColumnName("parceira")
                .HasDefaultValue(false);

            builder.Property(l => l.Ativa)
                .HasColumnName("ativa")
                .HasDefaultValue(true);

            builder.HasMany(l => l.Times)
                .WithOne(t => t.Loja)
                .HasForeignKey(lt => lt.IdTime)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
