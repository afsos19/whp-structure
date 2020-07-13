using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class QuestionQuizConfiguration : IEntityTypeConfiguration<QuestionQuiz>
    {
        public void Configure(EntityTypeBuilder<QuestionQuiz> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_PERGUNTA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.Image)
                .HasColumnName("IMAGEM");

            modelBuilder
                .Property("QuizId")
                .HasColumnName("ID_QUESTIONARIO");

            modelBuilder
                .Property("QuestionQuizTypeId")
                .HasColumnName("ID_TIPO_PERGUNTA_QUESTIONARIO");

        }
    }
}
