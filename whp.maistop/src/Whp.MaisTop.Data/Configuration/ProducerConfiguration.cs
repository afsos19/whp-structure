using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> modelBuilder)
        {
            modelBuilder.ToTable("FORNECEDOR");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");
            
        }
    }
}
