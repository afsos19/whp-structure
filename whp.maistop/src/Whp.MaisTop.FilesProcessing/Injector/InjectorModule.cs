using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Whp.MaisTop.Business.Configurations;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.FilesProcessing.Injector
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHierarchyProcessesService>().To<HierarchyProcessesService>();
            Bind<ISaleProcessesService>().To<SaleProcessesService>();
            Bind<IEmailService>().To<EmailService>();
            //Bind<WhpDbContext>().ToSelf().InThreadScope()
            //.WithConstructorArgument("options",
            //    new DbContextOptionsBuilder<WhpDbContext>()
            //    .UseSqlServer("Data Source=sqlfullbar07.c7ophga59fwr.us-east-1.rds.amazonaws.com;Initial Catalog=WhpMaisTopDev;Persist Security Info=True;User ID=WHPMaisTopDEV;Password=top!dev!;connect timeout=0;")
            //    .Options);
            Bind<WhpDbContext>().ToSelf().InThreadScope()
            .WithConstructorArgument("options",
                new DbContextOptionsBuilder<WhpDbContext>()
                .UseSqlServer("Data Source=sqlfullbar09.c7ophga59fwr.us-east-1.rds.amazonaws.com;Initial Catalog=WHP_MaisTop_Prod;Persist Security Info=True;User ID=WHPMaisTop;Password=whpmt!@1@1902!;connect timeout=0;")
                .Options);
            Bind<EmailConfiguration>()
              .ToMethod(context =>
                     new EmailConfiguration
                     {
                         HostPORT = ConfigurationManager.AppSettings.Get("HostPORT").ToString(),
                         HostSMTP = ConfigurationManager.AppSettings.Get("HostSMTP").ToString(),
                         HostSender = ConfigurationManager.AppSettings.Get("HostSender").ToString(),
                         SmtpUser = ConfigurationManager.AppSettings.Get("SmtpUser").ToString(),
                         SmtpPassword = ConfigurationManager.AppSettings.Get("SmtpPassword").ToString()
                     }
              );
            Bind<ILogger>().ToMethod(p => NLog.LogManager.GetLogger(
                   p.Request.Target.Member.Name)).InTransientScope();
            Bind<IHierarchyFileRepository>().To<HierarchyFileRepository>().InTransientScope();
            Bind<IHierarchyFileDataRepository>().To<HierarchyFileDataRepository>().InTransientScope();
            Bind<IHierarchyFileDataErrorRepository>().To<HierarchyFileDataErrorRepository>().InTransientScope();
            Bind<IFileStatusRepository>().To<FileStatusRepository>().InTransientScope();
            Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            Bind<IUserStatusRepository>().To<UserStatusRepository>().InTransientScope();
            Bind<INetworkRepository>().To<NetworkRepository>().InTransientScope();
            Bind<IShopRepository>().To<ShopRepository>().InTransientScope();
            Bind<IShopUserRepository>().To<ShopUserRepository>().InTransientScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            Bind<IOfficeRepository>().To<OfficeRepository>().InTransientScope();
            Bind<ISaleRepository>().To<SaleRepository>().InTransientScope();
            Bind<ISaleFileRepository>().To<SaleFileRepository>().InTransientScope();
            Bind<ICategoryProductRepository>().To<CategoryProductRepository>().InTransientScope();
            Bind<ISaleFileDataRepository>().To<SaleFileDataRepository>().InTransientScope();
            Bind<ISaleFileDataErrorRepository>().To<SaleFileDataErrorRepository>().InTransientScope();
            Bind<ISaleFileSkuStatusRepository>().To<SaleFileSkuStatusRepository>().InTransientScope();
            Bind<IProductRepository>().To<ProductRepository>().InTransientScope();
            Bind<IUserStatusLogRepository>().To<UserStatusLogRepository>().InTransientScope();
            Bind<IFocusProductRepository>().To<FocusProductRepository>().InTransientScope();
            Bind<IParticipantProductRepository>().To<ParticipantProductRepository>().InTransientScope();
            Bind<IUserPunctuationRepository>().To<UserPunctuationRepository>().InTransientScope();
            Bind<IUserPunctuationSourceRepository>().To<UserPunctuationSourceRepository>().InTransientScope();
            Bind<IUserAccessCodeInviteRepository>().To<UserAccessCodeInviteRepository>().InTransientScope();
            Bind<IMapper>().To<Mapper>().InTransientScope();
        }
    }
}
