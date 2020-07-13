using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> modelBuilder)
        {
            modelBuilder.ToTable("LOJA");

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
                .Property(p => p.Address)
                .HasColumnName("ENDERECO");

            modelBuilder
                .Property(p => p.Cep)
                .HasColumnName("CEP");

            modelBuilder
                .Property(p => p.City)
                .HasColumnName("CIDADE");

            modelBuilder
                .Property(p => p.Cnpj)
                .HasColumnName("CNPJ");

            modelBuilder
                .Property(p => p.Complement)
                .HasColumnName("COMPLEMENTO");

            modelBuilder
                .Property(p => p.Neighborhood)
                .HasColumnName("BAIRRO");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property(p => p.Number)
                .HasColumnName("NUMERO");

            modelBuilder
                .Property(p => p.ShopCode)
                .HasColumnName("CODIGO_LOJA");

        }
    }
}
