using CacaMantos.Admin.API.Infra.Data.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CacaMantos.Admin.API.Infra.Data.Mapping.Tabelas
{
    public class TimeModelMapping : IEntityTypeConfiguration<TimeModel>
    {
        public void Configure(EntityTypeBuilder<TimeModel> builder)
        {
            builder.ToTable("times");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.Identificador)
                .HasColumnName("identificador")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.NomeBusca)
                .HasColumnName("nome_busca")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.Destaque)
                .HasColumnName("destaque")
                .HasDefaultValue(false);

            builder.Property(t => t.Ativo)
                .HasColumnName("ativo")
                .HasDefaultValue(true);

            builder.Property(t => t.Principal)
                .HasColumnName("principal")
                .HasDefaultValue(true);

            builder.Property(t => t.Termos)
                .HasColumnName("termos");

            builder.HasMany(t => t.Lojas)
                .WithOne(lt => lt.Time)
                .HasForeignKey(lt => lt.IdLoja)
                .OnDelete(DeleteBehavior.Cascade); ;

            builder.Property(t => t.TimePrincipalId).HasColumnName("id_time_principal");
            builder.HasOne(t => t.TimePrincipal)
                .WithMany(t => t.Homonimos)
                .HasForeignKey(t => t.TimePrincipalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.TimePrincipalId);
        }
    }
}
