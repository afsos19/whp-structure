using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class AnswerUserQuizConfiguration : IEntityTypeConfiguration<AnswerUserQuiz>
    {
        public void Configure(EntityTypeBuilder<AnswerUserQuiz> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_USUARIO_RESPOSTA");

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
