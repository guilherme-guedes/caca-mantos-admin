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

            builder.Property(t => t.IdTimePrincipal).HasColumnName("id_time_principal");
            builder.HasOne(t => t.TimePrincipal)
                .WithMany(t => t.Homonimos)
                .HasForeignKey(t => t.IdTimePrincipal)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasIndex(t => t.IdTimePrincipal);
            builder.HasIndex(t => t.Identificador);
            builder.HasIndex(t => t.Nome);
        }
    }
}
