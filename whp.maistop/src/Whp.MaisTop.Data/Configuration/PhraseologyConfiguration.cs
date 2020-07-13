using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class PhraseologyConfiguration : IEntityTypeConfiguration<Phraseology>
    {
        public void Configure(EntityTypeBuilder<Phraseology> modelBuilder)
        {
            modelBuilder.ToTable("FRASEOLOGIA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("PhraseologyTypeSubjectId")
                .HasColumnName("ID_TIPO_ASSUNTO_FRASEOLOGIA");

            modelBuilder
                .Property(p => p.Answer)
                .HasColumnName("RESPOSTA");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

        }
    }
}
