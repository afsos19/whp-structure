using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class GroupProductConfiguration : IEntityTypeConfiguration<GroupProduct>
    {
        public void Configure(EntityTypeBuilder<GroupProduct> modelBuilder)
        {
            modelBuilder.ToTable("PRODUTO_GRUPO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");
            
            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");
            
        }
    }
}
