using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;

namespace Whp.MaisTop.CrossCutting.IoC.ScopeInjectors
{
    public static class ServiceScopeInjector
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INetworkService, NetworkService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IHierarchyFileService, HierarchyFileService>();
            services.AddScoped<ISaleFileService, SaleFileService>();
            services.AddScoped<IOccurrenceService, OccurrenceService>();
            services.AddScoped<IPunctuationService, PunctuationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ISaleProcessesService, SaleProcessesService>();
            services.AddScoped<IFriendInviteService, FriendInviteService>();
            services.AddScoped<IFocusProductService, FocusProductService>();
            services.AddScoped<IParticipantProductService, ParticipantProductService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserStatusService, UserStatusService>();
            services.AddScoped<IPhraseologyService, PhraseologyService>();
            services.AddScoped<IPhraseologyTypeSubjectService, PhraseologyTypeSubjectService>();
            services.AddScoped<IPhraseologySubjectService, PhraseologySubjectService>();
            services.AddScoped<IPhraseologyCategoryService, PhraseologyCategoryService>();
            services.AddScoped<IEaiSingleSignOnService, EaiSingleSignOnService>();
            services.AddScoped<ILogsPunctuationService, LogsPunctuationService>();

        }
    }
}
