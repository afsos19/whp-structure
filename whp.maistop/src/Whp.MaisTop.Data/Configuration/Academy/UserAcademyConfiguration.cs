using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Entities.Academy;

namespace Whp.MaisTop.Data.Configuration.Academy
{
    public class UserAcademyConfiguration : IEntityTypeConfiguration<UserAcademy>
    {
        public void Configure(EntityTypeBuilder<UserAcademy> modelBuilder)
        {
            modelBuilder.ToTable("TB_Usuario");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("idUsuario");
            
            modelBuilder
               .Property(p => p.Login)
               .HasColumnName("dsLogin");

            modelBuilder
               .Property(p => p.Activated)
               .HasColumnName("btAtivo");
            

        }
    }
}
