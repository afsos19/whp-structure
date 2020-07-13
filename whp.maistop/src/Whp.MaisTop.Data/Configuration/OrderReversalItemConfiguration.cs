using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OrderReversalItemConfiguration : IEntityTypeConfiguration<OrderReversalItem>
    {
        public void Configure(EntityTypeBuilder<OrderReversalItem> modelBuilder)
        {
            modelBuilder.ToTable("PEDIDO_EXTORNO_ITEM");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.ExternalOrderId)
                .HasColumnName("ID_PEDIDO_EXTERNO");

            modelBuilder
                .Property(p => p.Ammout)
                .HasColumnName("QUANTIDADE");

            modelBuilder
                .Property(p => p.CodeItem)
                .HasColumnName("ITEM_CODIGO");

            modelBuilder
                .Property(p => p.Reason)
                .HasColumnName("MOTIVO");

            modelBuilder
                .Property("OrderItemId")
                .HasColumnName("ID_PEDIDO_ITEM");

            modelBuilder
                .Property(p => p.TotalValue)
                .HasColumnName("VALOR_TOTAL");

            modelBuilder
                .Property(p => p.UnitValue)
                .HasColumnName("VALOR_UNITARIO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

        }
    }
}
