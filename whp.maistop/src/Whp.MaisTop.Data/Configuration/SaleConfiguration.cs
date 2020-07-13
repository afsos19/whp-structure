using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> modelBuilder)
        {
            modelBuilder.ToTable("VENDA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.Processed)
                .HasColumnName("PROCESSADO");

            modelBuilder
                .Property(p => p.Amout)
                .HasColumnName("QUANTIDADE");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.CurrentYear)
                .HasColumnName("ANO_VIGENTE");

            modelBuilder
                .Property(p => p.CurrentMonth)
                .HasColumnName("MES_VIGENTE");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property("ProductId")
                .HasColumnName("ID_PRODUTO");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property(p => p.SaleDate)
                .HasColumnName("DATA_VENDA");

            modelBuilder
                .Property("ShopId")
                .HasColumnName("ID_LOJA");

            modelBuilder
                .Property(p => p.TotalValue)
                .HasColumnName("VALOR_TOTAL");

            modelBuilder
                .Property(p => p.UnitValue)
                .HasColumnName("VALOR_UNIDADE");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");
            
        }
    }
}
