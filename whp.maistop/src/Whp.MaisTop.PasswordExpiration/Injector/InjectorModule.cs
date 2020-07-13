﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Mapping;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.PasswordExpiration.Injector
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();

            Bind<WhpDbContext>().ToSelf().InThreadScope()
            .WithConstructorArgument("options",
                new DbContextOptionsBuilder<WhpDbContext>()
                .UseSqlServer("Data Source=sqlfullbar09.c7ophga59fwr.us-east-1.rds.amazonaws.com;Initial Catalog=WHP_MaisTop_Prod;Persist Security Info=True;User ID=WHPMaisTop;Password=whpmt!@1@1902!;connect timeout=0;")
                .Options);

            Bind<IShopUserRepository>().To<ShopUserRepository>().InTransientScope();
            Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            Bind<ILogger>().ToMethod(p => NLog.LogManager.GetLogger(
                   p.Request.Target.Member.Name)).InTransientScope();
            Bind<IMapper>()
            .ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });
                return config.CreateMapper();
            }).InTransientScope();
            Bind<IUserStatusRepository>().To<UserStatusRepository>().InTransientScope();
            Bind<IUserStatusLogRepository>().To<UserStatusLogRepository>().InTransientScope();
            Bind<IHostingEnvironment>().To<HostingEnvironment>();

        }
    }
}
