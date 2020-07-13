using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.ToTable("PEDIDO");

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
                .Property(p => p.ReversedAt)
                .HasColumnName("ESTORNADO_EM");

            modelBuilder
                .Property(p => p.AccessToken)
                .HasColumnName("TOKEN_ACESSO");

            modelBuilder
                .Property(p => p.AuthorizationCode)
                .HasColumnName("CODIGO_AUTORIZACAO");

            modelBuilder
                .Property(p => p.ConversionRate)
                .HasColumnName("TAXA_CONVERSAO");

            modelBuilder
                .Property(p => p.ExternalOrderId)
                .HasColumnName("ID_PEDIDO_EXTERNO");

            modelBuilder
                .Property(p => p.ForecastDate)
                .HasColumnName("DATA_PREVISAO");

            modelBuilder
                .Property(p => p.Freight)
                .HasColumnName("FRETE");

            modelBuilder
                .Property(p => p.Login)
                .HasColumnName("LOGIN");

            modelBuilder
                .Property("OrderStatusId")
                .HasColumnName("ID_PEDIDO_STATUS");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property(p => p.OrderValue)
                .HasColumnName("VALOR_PEDIDO");

            modelBuilder
                .Property(p => p.Total)
                .HasColumnName("TOTAL_PEDIDO");

        }
    }
}
