using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ParticipantProductConfiguration : IEntityTypeConfiguration<ParticipantProduct>
    {
        public void Configure(EntityTypeBuilder<ParticipantProduct> modelBuilder)
        {
            modelBuilder.ToTable("PRODUTO_PARTICIPANTE");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.CurrentMonth)
                .HasColumnName("MES_VIGENTE");

            modelBuilder
                .Property(p => p.CurrentYear)
                .HasColumnName("ANO_VIGENTE");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property("ProductId")
                .HasColumnName("ID_PRODUTO");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

        }
    }
}
