using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class TrainingUserPointsConfiguration : IEntityTypeConfiguration<TrainingUserPoints>
    {
        public void Configure(EntityTypeBuilder<TrainingUserPoints> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_PONTOS_TREINAMENTO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Percentage)
                .HasColumnName("PERCENTUAL");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTO");

            modelBuilder
                .Property(p => p.ResultId)
                .HasColumnName("ID_RESULTADO");

            modelBuilder
                .Property(p => p.TrainingDescription)
                .HasColumnName("TREINAMENTO_DESCRICAO");

            modelBuilder
               .Property(p => p.TrainingStatus)
               .HasColumnName("TREINAMENTO_STATUS");

            modelBuilder
               .Property(p => p.TrainingDoneAt)
               .HasColumnName("CONCLUIDO_EM");

            modelBuilder
               .Property(p => p.TrainingEndedAt)
               .HasColumnName("DATA_LIMITE");

            modelBuilder
               .Property(p => p.TrainingStartedAt)
               .HasColumnName("DATA_INICIO");

            modelBuilder
               .Property(p => p.TrainingId)
               .HasColumnName("ID_TREINAMENTO");
            
            modelBuilder
               .Property(p => p.Activated)
               .HasColumnName("ATIVO");

            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
               .Property("UserId")
               .HasColumnName("ID_USUARIO");
            
        }
    }
}
