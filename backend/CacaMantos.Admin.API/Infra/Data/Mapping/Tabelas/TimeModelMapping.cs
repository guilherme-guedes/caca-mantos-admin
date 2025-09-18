using backend.Infra.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infra.Data.Mapping.Tabelas
{
    public class TimeModelMapping : IEntityTypeConfiguration<TimeModel>
    {
        public void Configure(EntityTypeBuilder<TimeModel> builder)
        {
            builder.ToTable("times");

            builder.HasKey(t => t.id);
            builder.Property(t => t.id)
                .HasColumnName("id");

            builder.Property(t => t.nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.identificador)
                .HasColumnName("identificador")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.nomeBusca)
                .HasColumnName("nome_busca")
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.destaque)
                .HasColumnName("destaque")
                .HasDefaultValue(false);

            builder.Property(t => t.ativo)
                .HasColumnName("ativo")
                .HasDefaultValue(true);

            builder.Property(t => t.principal)
                .HasColumnName("principal")
                .HasDefaultValue(true);

            builder.Property(t => t.termos)
                .HasColumnName("termos");

            builder.HasMany(t => t.lojas)
                .WithOne(lt => lt.time)
                .HasForeignKey(lt => lt.idLoja)
                .OnDelete(DeleteBehavior.Cascade); ;

            builder.Property(t => t.timePrincipalId).HasColumnName("id_time_principal");
            builder.HasOne(t => t.timePrincipal)
                .WithMany(t => t.homonimos)
                .HasForeignKey(t => t.timePrincipalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.timePrincipalId);
        }        
    }
}