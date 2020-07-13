using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OccurrenceConfiguration : IEntityTypeConfiguration<Occurrence>
    {
        public void Configure(EntityTypeBuilder<Occurrence> modelBuilder)
        {
            modelBuilder.ToTable("SAC");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Participant)
                .HasColumnName("PARTICIPANTE");

            modelBuilder
                .Property(p => p.File)
                .HasColumnName("ARQUIVO");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property(p => p.BrazilCTCall)
                .HasColumnName("BRASILCT_CHAMADO");
            
            modelBuilder
                .Property(p => p.Code)
                .HasColumnName("CODIGO");
            
            modelBuilder
                .Property(p => p.Critical)
                .HasColumnName("CRITICO");
            
            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property("OccurrenceStatusId")
                .HasColumnName("ID_SAC_STATUS");
            
            modelBuilder
                .Property("OccurrenceSubjectId")
                .HasColumnName("ID_SAC_ASSUNTO");

            modelBuilder
                .Property("OccurrenceContactTypeId")
                .HasColumnName("ID_SAC_TIPO_CONTATO");

            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
               .Property(p => p.ClosedAt)
               .HasColumnName("FECHADO_EM");

            modelBuilder
               .Property(p => p.Activated)
               .HasColumnName("ATIVO");

            modelBuilder
               .Property(p => p.RedirectedAt)
               .HasColumnName("REDIRECIONADO_EM");

            modelBuilder
               .Property(p => p.ReturnedAt)
               .HasColumnName("RETORNADO_EM");

            modelBuilder
               .Property(p => p.LastIteration)
               .HasColumnName("ULTIMA_ITERACAO");

        }
    }
}
