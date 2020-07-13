using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class RegionalConfiguration : IEntityTypeConfiguration<Regional>
    {
        public void Configure(EntityTypeBuilder<Regional> modelBuilder)
        {
            modelBuilder.ToTable("REGIONAL");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("NOME");
        
        }
    }
}
