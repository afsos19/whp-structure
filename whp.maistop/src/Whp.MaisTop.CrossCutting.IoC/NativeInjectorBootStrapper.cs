using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Repositories;
using Whp.MaisTop.Data.UoW;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Whp.MaisTop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Whp.MaisTop.Business.Configurations;
using Whp.MaisTop.CrossCutting.IoC.Middleware;
using System.Security.Claims;
using Whp.MaisTop.CrossCutting.IoC.ScopeInjectors;
using Swashbuckle.AspNetCore.Swagger;
using NLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Whp.MaisTop.Data.Context.Academy;

namespace Whp.MaisTop.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WhpDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<WhpAcademyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Academy")));

            services.AddAutoMapper();

            RepositoryScopeInjector.Add(services);
            ServiceScopeInjector.Add(services);

            services.AddSingleton(Configuration.GetSection("SMSConfiguration").Get<SMSConfiguration>());
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<ILogger>(LogManager.GetCurrentClassLogger());

            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            // Add the access_token as a claim, as we may actually need it
                            var accessToken = context.SecurityToken as JwtSecurityToken;
                            if (accessToken != null)
                            {
                                ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                if (identity != null)
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Role, accessToken.Claims.Where(c => c.Type == ClaimTypes.Role).First().Value));
                                    identity.AddClaim(new Claim("id", accessToken.Claims.Where(c => c.Type == "id").First().Value));
                                    identity.AddClaim(new Claim("network", accessToken.Claims.Where(c => c.Type == "network").First().Value));
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                            new Info
                            {
                                Title = "+TOP API",
                                Version = "v1",
                            });
                c.EnableAnnotations();

            });

            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddAuthorization()
            .AddJsonFormatters()
            .AddApiExplorer();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Home";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Metodo para tratar virgula no banco
            var usCulture = new CultureInfo("en-US");
            var supportedCultures = new[] { usCulture };

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(usCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "")),
                RequestPath = "",
            });

            app.UseCors("Policy");
            app.UseAuthentication();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Host = httpReq.Host.Value;
                    swaggerDoc.Schemes = new List<string>() { httpReq.Scheme };
                    swaggerDoc.BasePath = httpReq.PathBase;
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "+TOP API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

    }
}
