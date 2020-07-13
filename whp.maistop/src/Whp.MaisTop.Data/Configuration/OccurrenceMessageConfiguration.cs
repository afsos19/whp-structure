using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OccurrenceMessageConfiguration : IEntityTypeConfiguration<OccurrenceMessage>
    {
        public void Configure(EntityTypeBuilder<OccurrenceMessage> modelBuilder)
        {
            modelBuilder.ToTable("SAC_MENSAGEM");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Internal)
                .HasColumnName("MENSAGEM_INTERNA");

            modelBuilder
                .Property(p => p.Catalog)
                .HasColumnName("MENSAGEM_CATALOGO");

            modelBuilder
                .Property(p => p.Message)
                .HasColumnName("MENSAGEM");

            modelBuilder
                .Property(p => p.File)
                .HasColumnName("ARQUIVO");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property("OccurrenceId")
                .HasColumnName("ID_SAC");

            modelBuilder
                .Property("OccurrenceMessageTypeId")
                .HasColumnName("ID_SAC_TIPO_MENSAGEM");

            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
               .Property(p => p.Activated)
               .HasColumnName("ATIVO");

        }
    }
}
