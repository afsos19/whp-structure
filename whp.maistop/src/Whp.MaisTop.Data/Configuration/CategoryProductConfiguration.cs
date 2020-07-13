using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> modelBuilder)
        {
            modelBuilder.ToTable("CATEGORIA_PRODUTO");

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
