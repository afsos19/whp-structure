using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Entities.Academy;

namespace Whp.MaisTop.Data.Configuration.Academy
{
    public class TrainingResultConfiguration : IEntityTypeConfiguration<TrainingResult>
    {
        public void Configure(EntityTypeBuilder<TrainingResult> modelBuilder)
        {
            modelBuilder.ToTable("TB_TreinamentoResultado");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("idResultado");
            
            modelBuilder
               .Property(p => p.Name)
               .HasColumnName("dsNome");
            
        }
    }
}
