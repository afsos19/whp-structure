using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserAccessLogConfiguration : IEntityTypeConfiguration<UserAccessLog>
    {
        public void Configure(EntityTypeBuilder<UserAccessLog> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_ACESSO_LOG");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property(p => p.Device)
                .HasColumnName("DISPOSITIVO");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property(p => p.Ip)
                .HasColumnName("IP");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

        }
    }
}
