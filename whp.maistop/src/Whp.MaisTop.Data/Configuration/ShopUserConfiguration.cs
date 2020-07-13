using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ShopUserConfiguration : IEntityTypeConfiguration<ShopUser>
    {
        public void Configure(EntityTypeBuilder<ShopUser> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_LOJA");

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
                .Property("ShopId")
                .HasColumnName("ID_LOJA");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

        }
    }
}
