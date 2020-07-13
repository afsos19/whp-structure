using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.Repositories.Academy;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.Repositories.Academy;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.CrossCutting.IoC.ScopeInjectors
{
    public static class RepositoryScopeInjector
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<IRightAnswerRepository, RightAnswerRepository>();
            services.AddScoped<IRegionalRepository, RegionalRepository>();
            services.AddScoped<IUserPunctuationSourceRepository, UserPunctuationSourceRepository>();
            services.AddScoped<IPunctuationRobotConfigurationRepository, PunctuationRobotConfigurationRepository>();
            services.AddScoped<IUserAccessCodeConfirmationRepository, UserAccessCodeConfirmationRepository>();
            services.AddScoped<IParticipantProductRepository, ParticipantProductRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopUserRepository, ShopUserRepository>();
            services.AddScoped<INetworkRepository, NetworkRepository>();
            services.AddScoped<IFileStatusRepository, FileStatusRepository>();
            services.AddScoped<IHierarchyFileDataErrorRepository, HierarchyFileDataErrorRepository>();
            services.AddScoped<IHierarchyFileDataRepository, HierarchyFileDataRepository>();
            services.AddScoped<IHierarchyFileRepository, HierarchyFileRepository>();
            services.AddScoped<ISaleFileDataErrorRepository, SaleFileDataErrorRepository>();
            services.AddScoped<ISaleFileDataRepository, SaleFileDataRepository>();
            services.AddScoped<ISaleFileRepository, SaleFileRepository>();
            services.AddScoped<ISaleFileDataRepository, SaleFileDataRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
            services.AddScoped<ISaleFileSkuStatusRepository, SaleFileSkuStatusRepository>();
            services.AddScoped<IFocusProductRepository, FocusProductRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IOccurrenceContactTypeRepository, OccurrenceContactTypeRepository>();
            services.AddScoped<IOccurrenceMessageTypeRepository, OccurrenceMessageTypeRepository>();
            services.AddScoped<IOccurrenceRepository, OccurrenceRepository>();
            services.AddScoped<IOccurrenceMessageRepository, OccurrenceMessageRepository>();
            services.AddScoped<IOccurrenceSubjectRepository, OccurrenceSubjectRepository>();
            services.AddScoped<IOccurrenceStatusRepository, OccurrenceStatusRepository>();
            services.AddScoped<IExpiredConfigurationPointsRepository, ExpiredConfigurationPointsRepository>();
            services.AddScoped<IUserPunctuationRepository, UserPunctuationRepository>();
            services.AddScoped<ITrainingUserPointsRepository, TrainingUserPointsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderCancelRepository, OrderCancelRepository>();
            services.AddScoped<IOrderConfirmRepository, OrderConfirmRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderReversalItemRepository, OrderReversalItemRepository>();
            services.AddScoped<IOrderReversalRepository, OrderReversalRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuestionQuizTypeRepository, QuestionQuizTypeRepository>();
            services.AddScoped<IAnswerUserQuizRepository, AnswerUserQuizRepository>();
            services.AddScoped<IAnswerQuizRepository, AnswerQuizRepository>();
            services.AddScoped<IUserStatusLogRepository, UserStatusLogRepository>();
            services.AddScoped<IUserAccessLogRepository, UserAccessLogRepository>();
            services.AddScoped<IUserAccessCodeInviteRepository, UserAccessCodeInviteRepository>();
            services.AddScoped<IUserAccessCodeExpirationRepository, UserAccessCodeExpirationRepository>();
            services.AddScoped<IGroupProductRepository, GroupProductRepository>();
            services.AddScoped<IUserPunctuationReservedRepository, UserPunctuationReservedRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IPhraseologyRepository, PhraseologyRepository>();
            services.AddScoped<IPhraseologyCategoryRepository, PhraseologyCategoryRepository>();
            services.AddScoped<IPhraseologyTypeSubjectRepository, PhraseologyTypeSubjectRepository>();
            services.AddScoped<IPhraseologySubjectRepository, PhraseologySubjectRepository>();
            services.AddScoped<ILogsPunctuationRepository, LogsPunctuationRepository>();

        }
    }
}

