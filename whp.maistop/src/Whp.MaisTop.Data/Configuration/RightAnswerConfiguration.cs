using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class RightAnswerConfiguration : IEntityTypeConfiguration<RightAnswer>
    {
        public void Configure(EntityTypeBuilder<RightAnswer> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_RESPOSTA_CERTA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.AnswerDescription)
                .HasColumnName("RESPOSTA_DESCRITIVA");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property("AnswerQuizId")
                .HasColumnName("ID_QUESTIONARIO_RESPOSTA");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

        }
    }
}
