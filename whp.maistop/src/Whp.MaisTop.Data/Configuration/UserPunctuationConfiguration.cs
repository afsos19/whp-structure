using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserPunctuationConfiguration : IEntityTypeConfiguration<UserPunctuation>
    {
        public void Configure(EntityTypeBuilder<UserPunctuation> modelBuilder)
        {
            modelBuilder.ToTable("PONTUACAO_USUARIO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.CurrentYear)
                .HasColumnName("ANO_VIGENTE");

            modelBuilder
                .Property(p => p.CurrentMonth)
                .HasColumnName("MES_VIGENTE");

            modelBuilder
                .Property(p => p.OperationType)
                .HasColumnName("TIPO_OPERACAO");

            modelBuilder
                .Property(p => p.Punctuation)
                .HasColumnName("PONTUACAO");

            modelBuilder
                .Property(p => p.ReferenceEntityId)
                .HasColumnName("ID_ENTIDADE_REFERENTE");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property("UserPunctuationSourceId")
                .HasColumnName("ID_ORIGEM_PONTUACAO");
            
            
        }
    }
}
