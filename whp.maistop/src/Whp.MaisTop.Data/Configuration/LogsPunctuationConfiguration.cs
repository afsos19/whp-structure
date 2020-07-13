using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class LogsPunctuationConfiguration : IEntityTypeConfiguration<LogsPunctuation>
    {
        public void Configure(EntityTypeBuilder<LogsPunctuation> modelBuilder)
        {
            modelBuilder.ToTable("PONTUACAO_USUARIO_LOGALTERACAO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.DESCRICAO)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.CRIADO_EM)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.ANO_VIGENTE)
                .HasColumnName("ANO_VIGENTE");

            modelBuilder
                .Property(p => p.MES_VIGENTE)
                .HasColumnName("MES_VIGENTE");

            modelBuilder
                .Property(p => p.TIPO_OPERACAO)
                .HasColumnName("TIPO_OPERACAO");

            modelBuilder
                .Property(p => p.PONTUACAO)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property(p => p.ID_ENTIDADE_REFERENTE)
                .HasColumnName("ID_ENTIDADE_REFERENTE");

            modelBuilder
                .Property(p => p.ID_USUARIO)
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property(p => p.ID_ORIGEM_PONTUACAO)
                .HasColumnName("ID_ORIGEM_PONTUACAO");

            modelBuilder
                .Property(p => p.USUARIO_BANCO)
                .HasColumnName("USUARIO_BANCO");

            modelBuilder
                .Property(p => p.OPERACAO)
                .HasColumnName("OPERACAO");
        }
    }
}
