using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class PhraseologyTypeSubjectConfiguration : IEntityTypeConfiguration<PhraseologyTypeSubject>
    {
        public void Configure(EntityTypeBuilder<PhraseologyTypeSubject> modelBuilder)
        {
            modelBuilder.ToTable("FRASEOLOGIA_TIPO_ASSUNTO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("PhraseologySubjectId")
                .HasColumnName("ID_ASSUNTO_FRASEOLOGIA");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

        }
    }
}
