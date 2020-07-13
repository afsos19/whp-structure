using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class SaleFileDataConfiguration : IEntityTypeConfiguration<SaleFileData>
    {
        public void Configure(EntityTypeBuilder<SaleFileData> modelBuilder)
        {
            modelBuilder.ToTable("ARQUIVO_VENDA_DADOS");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Cnpj)
                .HasColumnName("CNPJ");

            modelBuilder
                .Property(p => p.CpfSalesman)
                .HasColumnName("CPF_VENDEDOR");

            modelBuilder
                .Property(p => p.NameSalesman)
                .HasColumnName("NOME_VENDEDOR");

            modelBuilder
                .Property(p => p.Category)
                .HasColumnName("CATEGORIA");
        
            modelBuilder
               .Property(p => p.Amount)
               .HasColumnName("QUANTIDADE");

            modelBuilder
               .Property(p => p.ProductDescription)
               .HasColumnName("DESCRICAO_PRODUTO");

            modelBuilder
               .Property(p => p.Resale)
               .HasColumnName("REVENDA");

            modelBuilder
               .Property(p => p.ShopCode)
               .HasColumnName("CODIGO_LOJA");

            modelBuilder
               .Property(p => p.EanCode)
               .HasColumnName("CODIGO_EAN");

            modelBuilder
               .Property(p => p.RequestNumber)
               .HasColumnName("NUMERO_PEDIDO");
            
            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
              .Property(p => p.SaleDate)
              .HasColumnName("DATA_VENDA");
            
            modelBuilder
              .Property("SaleFileId")
              .HasColumnName("ID_ARQUIVO_VENDA");

            modelBuilder
              .Property(p => p.Product)
              .HasColumnName("ID_PRODUTO");

            modelBuilder
              .Property("SaleFileSkuStatusId")
              .HasColumnName("ID_ARQUIVO_VENDA_SKU_STATUS");
            

        }
    }
}
