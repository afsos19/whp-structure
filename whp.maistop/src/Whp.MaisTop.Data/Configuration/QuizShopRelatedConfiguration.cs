using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class QuizShopRelatedConfiguration : IEntityTypeConfiguration<QuizShopRelated>
    {
        public void Configure(EntityTypeBuilder<QuizShopRelated> modelBuilder)
        {
            modelBuilder.ToTable("QUESTIONARIO_LOJAS_RELACIONADA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("QuizRelatedId")
                .HasColumnName("ID_QUIZ_RELACIONADO");

            modelBuilder
                .Property("ShopId")
                .HasColumnName("ID_LOJA");

        }
    }
}
