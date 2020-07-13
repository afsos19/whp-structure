using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class PhraseologyCategoryConfiguration : IEntityTypeConfiguration<PhraseologyCategory>
    {
        public void Configure(EntityTypeBuilder<PhraseologyCategory> modelBuilder)
        {
            modelBuilder.ToTable("FRASEOLOGIA_CATEGORIA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");


        }
    }
}
