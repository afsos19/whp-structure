using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class QuizRelatedConfiguration : IEntityTypeConfiguration<QuizRelated>
    {
        public void Configure(EntityTypeBuilder<QuizRelated> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_RELACIONADO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property("QuizId")
                .HasColumnName("ID_QUIZ");

            modelBuilder
                .Property("OfficeId")
                .HasColumnName("ID_CARGO");

        }
    }
}
