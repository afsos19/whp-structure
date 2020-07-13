using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Configuration;
using Whp.MaisTop.Data.Configuration.Academy;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Entities.Academy;

namespace Whp.MaisTop.Data.Context.Academy
{
    public class WhpAcademyDbContext : DbContext
    {
        public WhpAcademyDbContext()
        {
        }

        public WhpAcademyDbContext(DbContextOptions<WhpAcademyDbContext> options) : base(options)
        {
        }

        #region DbSet

        public DbSet<TrainingUser> TrainingUser { get; set; }
        public DbSet<TrainingResult> TrainingResult { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<UserAcademy> UserAcademy { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new TrainingConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingResultConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserAcademyConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
