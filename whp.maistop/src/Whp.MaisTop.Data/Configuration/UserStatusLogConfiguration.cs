using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class UserStatusLogConfiguration : IEntityTypeConfiguration<UserStatusLog>
    {
        public void Configure(EntityTypeBuilder<UserStatusLog> modelBuilder)
        {
            modelBuilder.ToTable("USUARIO_STATUS_LOG");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("UserId")
                .HasColumnName("ID_USUARIO");

            modelBuilder
                .Property("UserStatusFromId")
                .HasColumnName("DE_ID_USUARIO_STATUS");

            modelBuilder
                .Property("UserStatusToId")
                .HasColumnName("PARA_ID_USUARIO_STATUS");

            modelBuilder
                .Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM");

            modelBuilder
                .Property(p => p.Description)
                .HasColumnName("DESCRICAO");

        }
    }
}
