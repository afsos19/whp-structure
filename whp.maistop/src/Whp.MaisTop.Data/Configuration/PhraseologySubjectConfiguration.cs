using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class PhraseologySubjectConfiguration : IEntityTypeConfiguration<PhraseologySubject>
    {
        public void Configure(EntityTypeBuilder<PhraseologySubject> modelBuilder)
        {
            modelBuilder.ToTable("FRASEOLOGIA_ASSUNTO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("PhraseologyCategoryId")
                .HasColumnName("ID_CATEGORIA_FRASEOLOGIA");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

        }
    }
}
