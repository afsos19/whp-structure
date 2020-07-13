using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class PunctuationRobotConfigurationConfiguration : IEntityTypeConfiguration<PunctuationRobotConfiguration>
    {
        public void Configure(EntityTypeBuilder<PunctuationRobotConfiguration> modelBuilder)
        {
            modelBuilder.ToTable("CONFIGURACAO_ROBO_PONTOS");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.IdMechanicsRobotPunctuation)
                .HasColumnName("ID_MECANISMO_ROBO_PONTOS");

            modelBuilder
                .Property(p => p.IdRobotPunctuation)
                .HasColumnName("ID_ROBO_PONTOS");
            
        }
    }
}
