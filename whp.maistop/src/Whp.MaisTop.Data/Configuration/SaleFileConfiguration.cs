using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class SaleFileConfiguration : IEntityTypeConfiguration<SaleFile>
    {
        public void Configure(EntityTypeBuilder<SaleFile> modelBuilder)
        {
            modelBuilder.ToTable("ARQUIVO_VENDA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.CurrentMonth)
                .HasColumnName("MES_VIGENTE");

            modelBuilder
                .Property(p => p.CurrentYear)
                .HasColumnName("ANO_VIGENTE");

            modelBuilder
                .Property(p => p.FileName)
                .HasColumnName("NOME_ARQUIVO");

            modelBuilder
                .Property("FileStatusId")
                .HasColumnName("ID_STATUS_ARQUIVO");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");
            
            modelBuilder
               .Property(p => p.CreatedAt)
               .HasColumnName("CRIADO_EM");

        }
    }
}
