using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class NetworkConfiguration : IEntityTypeConfiguration<Network>
    {
        public void Configure(EntityTypeBuilder<Network> modelBuilder)
        {
            modelBuilder.ToTable("REDE");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");

            modelBuilder
                .Property("RegionalId")
                .HasColumnName("ID_REGIONAL");

            modelBuilder
                .Property(p => p.SiteImage)
                .HasColumnName("SITE_IMAGEM");

            modelBuilder
                .Property(p => p.SiteName)
                .HasColumnName("SITE_NOME");

            modelBuilder
                .Property(p => p.SiteShortName)
                .HasColumnName("SITE_NOME_CURTO");

        }
    }
}
