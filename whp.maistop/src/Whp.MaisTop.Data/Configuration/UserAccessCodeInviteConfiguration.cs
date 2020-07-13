using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserAccessCodeInviteConfiguration : IEntityTypeConfiguration<UserAccessCodeInvite>
    {
        public void Configure(EntityTypeBuilder<UserAccessCodeInvite> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_CODIGO_CONVITE_ACESSO");

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

            modelBuilder
                .Property(p => p.UpdatedAt)
                .HasColumnName("ATUALIZADO_EM");
        }
    }
}
