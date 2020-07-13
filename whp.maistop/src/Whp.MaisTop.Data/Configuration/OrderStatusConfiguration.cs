using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> modelBuilder)
        {
            modelBuilder.ToTable("PEDIDO_STATUS");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Code)
                .HasColumnName("CODIGO");

            modelBuilder
                .Property(p => p.Method)
                .HasColumnName("METODO");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");
            
        }
    }
}
