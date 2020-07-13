using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserPunctuationSourceConfiguration : IEntityTypeConfiguration<UserPunctuationSource>
    {
        public void Configure(EntityTypeBuilder<UserPunctuationSource> modelBuilder)
        {
            modelBuilder.ToTable("ORIGEM_PONTUACAO_USUARIO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property("PunctuationRobotConfigurationId")
                .HasColumnName("ID_CONFIGURACAO_ROBO_PONTOS");
 
        }
    }
}
