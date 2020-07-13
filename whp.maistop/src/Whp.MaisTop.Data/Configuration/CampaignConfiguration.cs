using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> modelBuilder)
        {
            modelBuilder.ToTable("CAMPANHAS");

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
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.Ordernation)
                .HasColumnName("ORDENACAO");

            modelBuilder
                .Property(p => p.Photo)
                .HasColumnName("FOTO");

            modelBuilder
                .Property(p => p.Thumb)
                .HasColumnName("FOTO_THUMB");

            modelBuilder
                .Property(p => p.Title)
                .HasColumnName("TITULO");

            modelBuilder
                .Property(p => p.SubTitle)
                .HasColumnName("SUBTITULO");
        }
    }
}
