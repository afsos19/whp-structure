using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class QuestionQuizTypeConfiguration : IEntityTypeConfiguration<QuestionQuizType>
    {
        public void Configure(EntityTypeBuilder<QuestionQuizType> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_TIPO_PERGUNTA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");
            
        }
    }
}
