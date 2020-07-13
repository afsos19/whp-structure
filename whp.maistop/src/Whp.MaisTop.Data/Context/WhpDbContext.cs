using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Configuration;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.ViewModels;

namespace Whp.MaisTop.Data.Context
{
    public class WhpDbContext : DbContext
    {
        public WhpDbContext()
        {
        }

        public WhpDbContext(DbContextOptions<WhpDbContext> options) : base(options)
        {
        }

        #region DbSet

        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuestionQuizType> QuestionQuizType { get; set; }
        public DbSet<AnswerQuiz> AnswerQuiz { get; set; }
        public DbSet<RightAnswer> RightAnswer { get; set; }
        public DbSet<AnswerUserQuiz> AnswerUserQuiz { get; set; }
        public DbSet<QuestionQuiz> QuestionQuiz { get; set; }
        public DbSet<QuizRelated> QuizRelated { get; set; }
        public DbSet<QuizShopRelated> QuizShopRelated { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserPunctuation> UserPunctuation { get; set; }
        public DbSet<UserPunctuationReserved> UserPunctuationReserved { get; set; }
        public DbSet<UserPunctuationSource> UserPunctuationSource { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<ShopUser> ShopUser { get; set; }
        public DbSet<UserStatusLog> UserStatusLog { get; set; }
        public DbSet<Regional> Region { get; set; }
        public DbSet<PunctuationRobotConfiguration> PunctuationRobotConfiguration { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<GroupProduct> GroupProduct { get; set; }
        public DbSet<TrainingUserPoints> TrainingUserPoints { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<Network> Network { get; set; }
        public DbSet<FocusProduct> FocusProduct { get; set; }
        public DbSet<ExpiredConfigurationPoints> ExpiredConfigurationPoints { get; set; }
        public DbSet<CategoryProduct> CategoryProduct { get; set; }
        public DbSet<UserAccessCodeConfirmation> UserAccessCodeConfirmation { get; set; }
        public DbSet<UserAccessCodeInvite> UserAccessCodeInvite { get; set; }
        public DbSet<UserAccessLog> UserAccessLog { get; set; }
        public DbSet<FileStatus> FileStatus { get; set; }
        public DbSet<HierarchyFile> HierarchyFile { get; set; }
        public DbSet<HierarchyFileData> HierarcyFileData { get; set; }
        public DbSet<HierarchyFileDataError> HierarcyFileDataError { get; set; }
        public DbSet<Occurrence> Occurrence { get; set; }
        public DbSet<Phraseology> Phraseology { get; set; }
        public DbSet<PhraseologySubject> PhraseologySubject { get; set; }
        public DbSet<PhraseologyTypeSubject> PhraseologyTypeSubject { get; set; }
        public DbSet<PhraseologyCategory> PhraseologyCategory { get; set; }
        public DbSet<OccurrenceContactType> OccurrenceContactType { get; set; }
        public DbSet<OccurrenceMessage> OccurrenceMessage { get; set; }
        public DbSet<OccurrenceMessageType> OccurrenceMessageType { get; set; }
        public DbSet<OccurrenceStatus> OccurrenceStatus { get; set; }
        public DbSet<OccurrenceSubject> OccurrenceSubject { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleFile> SaleFile { get; set; }
        public DbSet<SaleFileData> SaleFileData { get; set; }
        public DbSet<SaleFileDataError> SaleFileDataError { get; set; }
        public DbSet<SaleFileSkuStatus> SaleFileSkuStatus { get; set; }
        public DbSet<ParticipantProduct> ParticipantProduct { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderCancel> OrderCancel { get; set; }
        public DbSet<OrderConfirm> OrderConfirm { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderReversalItem> OrderReversalItem { get; set; }
        public DbSet<OrderReversal> OrderReversal { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsRelated> NewsRelated { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<CampaignRelated> CampaignRelated { get; set; }
        public DbSet<CampaignShopRelated> CampaignShopRelated { get; set; }
        public DbSet<UserAccessCodeExpiration> UserAccessCodeExpiration { get; set; }
        public DbSet<LogsPunctuation> LogsPunctuation { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Configurations

            modelBuilder.ApplyConfiguration(new CampaignConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignShopRelatedConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignRelatedConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionQuizConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionQuizTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerQuizConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerUserQuizConfiguration());
            modelBuilder.ApplyConfiguration(new QuizConfiguration());
            modelBuilder.ApplyConfiguration(new RightAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new QuizRelatedConfiguration());
            modelBuilder.ApplyConfiguration(new QuizShopRelatedConfiguration());
            modelBuilder.ApplyConfiguration(new GroupProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfirmConfiguration());
            modelBuilder.ApplyConfiguration(new OrderCancelConfiguration());
            modelBuilder.ApplyConfiguration(new OrderReversalConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NewsRelatedConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccessCodeConfirmationConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccessCodeInviteConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccessCodeExpirationConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccessLogConfiguration());
            modelBuilder.ApplyConfiguration(new ExpiredConfigurationPointsConfiguration());
            modelBuilder.ApplyConfiguration(new FocusProductConfiguration());
            modelBuilder.ApplyConfiguration(new NetworkConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new ProducerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserStatusLogConfiguration());
            modelBuilder.ApplyConfiguration(new PunctuationRobotConfigurationConfiguration());
            modelBuilder.ApplyConfiguration(new RegionalConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new ShopUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserPunctuationReservedConfiguration());
            modelBuilder.ApplyConfiguration(new UserPunctuationConfiguration());
            modelBuilder.ApplyConfiguration(new UserPunctuationSourceConfiguration());
            modelBuilder.ApplyConfiguration(new UserStatusConfiguration());
            modelBuilder.ApplyConfiguration(new FileStatusConfiguration());
            modelBuilder.ApplyConfiguration(new HierarchyFileConfiguration());
            modelBuilder.ApplyConfiguration(new HierarchyFileDataConfiguration());
            modelBuilder.ApplyConfiguration(new HierarchyFileDataErrorConfiguration());
            modelBuilder.ApplyConfiguration(new PhraseologyConfiguration());
            modelBuilder.ApplyConfiguration(new PhraseologySubjectConfiguration());
            modelBuilder.ApplyConfiguration(new PhraseologyTypeSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new PhraseologyCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceMessageConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceMessageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceStatusConfiguration());
            modelBuilder.ApplyConfiguration(new OccurrenceSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new SaleFileConfiguration());
            modelBuilder.ApplyConfiguration(new SaleFileDataConfiguration());
            modelBuilder.ApplyConfiguration(new SaleFileDataErrorConfiguration());
            modelBuilder.ApplyConfiguration(new SaleFileSkuStatusConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingUserPointsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderReversalItemConfiguration());
            modelBuilder.ApplyConfiguration(new LogsPunctuationConfiguration());
            modelBuilder.Query<PreProcessingVM>();
            modelBuilder.Query<CountVM>();
            modelBuilder.Query<SKUClassificationVM>();

            base.OnModelCreating(modelBuilder);

            #endregion
        }

    }
}
