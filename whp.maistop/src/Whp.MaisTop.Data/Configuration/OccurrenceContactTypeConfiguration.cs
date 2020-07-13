using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OccurrenceContactTypeConfiguration : IEntityTypeConfiguration<OccurrenceContactType>
    {
        public void Configure(EntityTypeBuilder<OccurrenceContactType> modelBuilder)
        {
            modelBuilder.ToTable("SAC_TIPO_CONTATO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
               .Property(p => p.Activated)
               .HasColumnName("ATIVO");

    

            

        }
    }
}
