using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class HierarchyFileDataConfiguration : IEntityTypeConfiguration<HierarchyFileData>
    {
        public void Configure(EntityTypeBuilder<HierarchyFileData> modelBuilder)
        {
            modelBuilder.ToTable("ARQUIVO_HIERARQUIA_DADOS");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Cnpj)
                .HasColumnName("CNPJ");

            modelBuilder
                .Property(p => p.Cpf)
                .HasColumnName("CPF");

            modelBuilder
                .Property(p => p.Name)
                .HasColumnName("NOME");
        
            modelBuilder
               .Property(p => p.Off)
               .HasColumnName("DESLIGADO");

            modelBuilder
               .Property(p => p.Office)
               .HasColumnName("CARGO");

            modelBuilder
               .Property(p => p.Resale)
               .HasColumnName("REVENDA");

            modelBuilder
               .Property(p => p.ShopCode)
               .HasColumnName("CODIGO_LOJA");

            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

            modelBuilder
              .Property(p => p.Spreedsheet)
              .HasColumnName("PLANILHA");

            modelBuilder
              .Property("HierarchyFileId")
              .HasColumnName("ID_ARQUIVO_HIERARQUIA");

        }
    }
}
