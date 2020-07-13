using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class CampaignRelatedConfiguration : IEntityTypeConfiguration<CampaignRelated>
    {
        public void Configure(EntityTypeBuilder<CampaignRelated> modelBuilder)
        {
            modelBuilder.ToTable("CAMPANHAS_RELACIONADA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("NetworkId")
                .HasColumnName("ID_REDE");

            modelBuilder
                .Property("CampaignId")
                .HasColumnName("ID_CAMPANHA");

            modelBuilder
                .Property("OfficeId")
                .HasColumnName("ID_CARGO");
            
        }
    }
}
