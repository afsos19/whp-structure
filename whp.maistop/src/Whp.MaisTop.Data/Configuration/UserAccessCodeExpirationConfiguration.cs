using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserAccessCodeExpirationConfiguration : IEntityTypeConfiguration<UserAccessCodeExpiration>
    {
        public void Configure(EntityTypeBuilder<UserAccessCodeExpiration> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_CODIGO_EXPIRACAO_ACESSO");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Code)
                .HasColumnName("CODIGO");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

        }
    }
}
