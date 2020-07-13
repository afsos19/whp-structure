using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Context.Academy;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.Repositories.Academy;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.Repositories.Academy;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.FilesProcessing.Injector
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrainingService>().To<TrainingService>();
            
            Bind<WhpDbContext>().ToSelf().InThreadScope()
            .WithConstructorArgument("options",
                new DbContextOptionsBuilder<WhpDbContext>()
                .UseSqlServer("Data Source=sqlfullbar09.c7ophga59fwr.us-east-1.rds.amazonaws.com;Initial Catalog=WHP_MaisTop_Prod;Persist Security Info=True;User ID=WHPMaisTop;Password=whpmt!@1@1902!;connect timeout=0;")
                .Options);

            Bind<WhpAcademyDbContext>().ToSelf().InThreadScope()
            .WithConstructorArgument("options",
                new DbContextOptionsBuilder<WhpAcademyDbContext>()
                .UseSqlServer("Data Source=sqlfullbar09.c7ophga59fwr.us-east-1.rds.amazonaws.com;Initial Catalog=WHPAcademiaVarejo;Persist Security Info=True;User ID=APPWHP;Password=WhP#2019!;connect timeout=0;")
                .Options);

            Bind<ITrainingUserRepository>().To<TrainingUserRepository>().InTransientScope();
            Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            Bind<ILogger>().ToMethod(p => NLog.LogManager.GetLogger(
                   p.Request.Target.Member.Name)).InTransientScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            Bind<ITrainingUserPointsRepository>().To<TrainingUserPointsRepository>().InTransientScope();
            Bind<IUserStatusRepository>().To<UserStatusRepository>().InTransientScope();
            Bind<IShopUserRepository>().To<ShopUserRepository>().InTransientScope();
            Bind<IUserPunctuationSourceRepository>().To<UserPunctuationSourceRepository>().InTransientScope();
            Bind<IUserPunctuationRepository>().To<UserPunctuationRepository>().InTransientScope();
            Bind<ITrainingRepository>().To<TrainingRepository>().InTransientScope();

        }
    }
}
