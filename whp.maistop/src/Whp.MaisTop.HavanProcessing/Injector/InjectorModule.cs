using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.HavanProcessing.Injector
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHavanService>().To<HavanService>();

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

            Bind<ILogger>().ToMethod(p => NLog.LogManager.GetLogger(
                   p.Request.Target.Member.Name)).InTransientScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            Bind<IShopRepository>().To<ShopRepository>().InTransientScope();
            Bind<IShopUserRepository>().To<ShopUserRepository>().InTransientScope();
            Bind<IOfficeRepository>().To<OfficeRepository>().InTransientScope();
            Bind<IUserStatusRepository>().To<UserStatusRepository>().InTransientScope();
            Bind<ISaleFileDataRepository>().To<SaleFileDataRepository>().InTransientScope();
            Bind<ISaleFileRepository>().To<SaleFileRepository>().InTransientScope();
            Bind<IFileStatusRepository>().To<FileStatusRepository>().InTransientScope();
            Bind<INetworkRepository>().To<NetworkRepository>().InTransientScope();
            Bind<ISaleFileSkuStatusRepository>().To<SaleFileSkuStatusRepository>().InTransientScope();
            Bind<ISaleRepository>().To<SaleRepository>().InTransientScope();
            Bind<IUserStatusLogRepository>().To<UserStatusLogRepository>().InTransientScope();
            Bind<IProductRepository>().To<ProductRepository>().InTransientScope();

        }
    }
}
