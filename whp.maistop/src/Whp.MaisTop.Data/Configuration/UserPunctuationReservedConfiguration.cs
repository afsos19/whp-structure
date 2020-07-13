using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserPunctuationReservedConfiguration : IEntityTypeConfiguration<UserPunctuationReserved>
    {
        public void Configure(EntityTypeBuilder<UserPunctuationReserved> modelBuilder)
        {
            modelBuilder.ToTable("PONTUACAO_USUARIO_RESERVADA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property(p => p.ExternalOrderId)
                .HasColumnName("ID_PEDIDO_EXTERNO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property("OrderId")
                .HasColumnName("ID_PEDIDO");
            
            
        }
    }
}
