﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class HierarchyFileDataErrorConfiguration : IEntityTypeConfiguration<HierarchyFileDataError>
    {
        public void Configure(EntityTypeBuilder<HierarchyFileDataError> modelBuilder)
        {
            modelBuilder.ToTable("ARQUIVO_HIERARQUIA_DADOS_ERRO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.Line)
                .HasColumnName("LINHA");
            
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
