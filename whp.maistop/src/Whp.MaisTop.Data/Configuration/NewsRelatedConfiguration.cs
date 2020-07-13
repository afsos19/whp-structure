using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class NewsRelatedConfiguration : IEntityTypeConfiguration<NewsRelated>
    {
        public void Configure(EntityTypeBuilder<NewsRelated> modelBuilder)
        {
            modelBuilder.ToTable("NOVIDADES_RELACIONADA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property("NewsId")
                .HasColumnName("ID_NOVIDADE");

            modelBuilder
                .Property("OfficeId")
                .HasColumnName("ID_CARGO");
            
        }
    }
}
