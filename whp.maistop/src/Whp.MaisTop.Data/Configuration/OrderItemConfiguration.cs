using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> modelBuilder)
        {
            modelBuilder.ToTable("PEDIDO_ITEM");

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
                .Property(p => p.Ammout)
                .HasColumnName("QUANTIDADE");

            modelBuilder
                .Property(p => p.Category)
                .HasColumnName("CATEGORIA");

            modelBuilder
                .Property(p => p.CodeItem)
                .HasColumnName("CODIGO_ITEM");

            modelBuilder
                .Property(p => p.Department)
                .HasColumnName("DEPARTAMENTO");

            modelBuilder
                .Property(p => p.ExternalOrderId)
                .HasColumnName("ID_PEDIDO_EXTERNO");

            modelBuilder
                .Property(p => p.ForecastDate)
                .HasColumnName("DATA_PREVISAO");

            modelBuilder
                .Property("OrderId")
                .HasColumnName("ID_PEDIDO");
            
            modelBuilder
                .Property(p => p.Partner)
                .HasColumnName("PARCEIRO");

            modelBuilder
                .Property(p => p.ProductName)
                .HasColumnName("NOME_PRODUTO");

            modelBuilder
                .Property(p => p.TotalValue)
                .HasColumnName("VALOR_TOTAL");

            modelBuilder
                .Property(p => p.UnitValue)
                .HasColumnName("VALOR_UNITARIO");

        }
    }
}
