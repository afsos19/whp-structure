using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class AnswerQuizConfiguration : IEntityTypeConfiguration<AnswerQuiz>
    {
        public void Configure(EntityTypeBuilder<AnswerQuiz> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_RESPOSTA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property("QuestionQuizId")
                .HasColumnName("ID_QUESTIONARIO_PERGUNTA");

            modelBuilder
                .Property(p => p.Right)
                .HasColumnName("PERGUNTA_CORRETA");

        }
    }
}
