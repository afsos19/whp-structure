using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Entities.Academy;

namespace Whp.MaisTop.Data.Configuration.Academy
{
    public class TrainingUserConfiguration : IEntityTypeConfiguration<TrainingUser>
    {
        public void Configure(EntityTypeBuilder<TrainingUser> modelBuilder)
        {
            modelBuilder.ToTable("TB_TreinamentoUsuario");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("idUsuarioTreinamento");

            modelBuilder
                .Property("UserAcademyId")
                .HasColumnName("idUsuario");

            modelBuilder
                .Property("TrainingId")
                .HasColumnName("idTreinamento");

            modelBuilder
                .Property("TrainingResultId")
                .HasColumnName("idResultado");

            modelBuilder
               .Property(p => p.Percentage)
               .HasColumnName("vlPorcentagem");

            modelBuilder
               .Property(p => p.Punctuation)
               .HasColumnName("vlPontuacao");
           
            modelBuilder
               .Property(p => p.TrainingDoneAt)
               .HasColumnName("dtOperacao");
        }
    }
}
