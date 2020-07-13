using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.ToTable("PRODUTO");

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

            modelBuilder
                .Property(p => p.Ean)
                .HasColumnName("EAN");

            modelBuilder
                .Property("CategoryProductId")
                .HasColumnName("ID_PRODUTO_CATEGORIA");
            
            modelBuilder
                .Property(p => p.Photo)
                .HasColumnName("FOTO");

            modelBuilder
                .Property("ProducerId")
                .HasColumnName("ID_FORNECEDOR");

            modelBuilder
                .Property(p => p.Sku)
                .HasColumnName("SKU");

        }
    }
}
