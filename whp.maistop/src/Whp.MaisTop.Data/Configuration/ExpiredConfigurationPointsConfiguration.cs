using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ExpiredConfigurationPointsConfiguration : IEntityTypeConfiguration<ExpiredConfigurationPoints>
    {
        public void Configure(EntityTypeBuilder<ExpiredConfigurationPoints> modelBuilder)
        {
            modelBuilder.ToTable("CONFIGURACAO_PONTOS_EXPIRADO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.ExpiresIn)
                .HasColumnName("EXPIRA_EM");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVADO");
        }
    }
}
