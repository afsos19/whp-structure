using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class OrderReversalConfiguration : IEntityTypeConfiguration<OrderReversal>
    {
        public void Configure(EntityTypeBuilder<OrderReversal> modelBuilder)
        {
            modelBuilder.ToTable("PEDIDO_EXTORNO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Activated)
                .HasColumnName("ATIVO");

            modelBuilder
                .Property(p => p.Value)
                .HasColumnName("VALOR");

            modelBuilder
                .Property(p => p.Message)
                .HasColumnName("MENSAGEM");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.AccessToken)
                .HasColumnName("TOKEN_ACESSO");

            modelBuilder
                .Property(p => p.AuthorizationCode)
                .HasColumnName("CODIGO_AUTORIZACAO");
            
            modelBuilder
                .Property(p => p.ExternalOrderId)
                .HasColumnName("ID_PEDIDO_EXTERNO");
            
            modelBuilder
                .Property(p => p.Login)
                .HasColumnName("LOGIN");

            modelBuilder
                .Property("OrderId")
                .HasColumnName("ID_PEDIDO");

        }
    }
}
