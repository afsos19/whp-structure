using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Data.Configuration
{
    public class CampaignShopRelatedConfiguration : IEntityTypeConfiguration<CampaignShopRelated>
    {
        public void Configure(EntityTypeBuilder<CampaignShopRelated> modelBuilder)
        {
            modelBuilder.ToTable("CAMPANHAS_LOJAS_RELACIONADA");

            modelBuilder
                .Property(p => p.Id)
                .HasColumnName("ID");

            modelBuilder
                .Property("CampaignRelatedId")
                .HasColumnName("ID_CAMPANHA_RELACIONADA");

            modelBuilder
                .Property("ShopId")
                .HasColumnName("ID_LOJA");

        }
    }
}
