using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Entities.Academy;

namespace Whp.MaisTop.Data.Configuration.Academy
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> modelBuilder)
        {
            modelBuilder.ToTable("TB_Treinamento");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("idTreinamento");
            
            modelBuilder
               .Property(p => p.Name)
               .HasColumnName("dsNome");

            modelBuilder
              .Property(p => p.TrainingStartedAt)
              .HasColumnName("dtInicioRespostas");

            modelBuilder
               .Property(p => p.TrainingEndedAt)
               .HasColumnName("dtFimRespostas");

            modelBuilder
               .Property(p => p.hasTrainingMaterial)
               .HasColumnName("btTreinamentoMaterial");

            modelBuilder
               .Property(p => p.CurrentMonth)
               .HasColumnName("inMes");

            modelBuilder
               .Property(p => p.CurrentYear)
               .HasColumnName("inAno");

            modelBuilder
               .Property(p => p.trainingKind)
               .HasColumnName("idTipo");

        }
    }
}
