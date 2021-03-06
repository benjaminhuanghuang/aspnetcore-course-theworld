﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.AspNetCore.Authentication.Cookies;

using TheWorld.Services;
using TheWorld.Models;
using TheWorld.ViewModels;

using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        private IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency Injection
            services.AddSingleton(Configuration);

            if (_env.IsEnvironment("Development"))
                services.AddScoped<IMailService, DebugMailService>();
            else
            {
                // Use a real mail service
            }

            services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 4;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == 200)
                        {
                        ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                        ctx.Response.Redirect(ctx.RedirectUri);
                        }

                        await Task.Yield();
                    }
                };
            }).AddEntityFrameworkStores<WorldContext>();

            services.AddDbContext<WorldContext>();
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddTransient<WorldContextSeedData>();
            services.AddTransient<GeoCoordsService>();
            services.AddLogging();

            // Use camel case when return result as json
            services.AddMvc(config =>{
                //redirect request to https
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // write Debug.Write into debug window
            loggerFactory.AddDebug(LogLevel.Information);
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();  // Show exception page

                // app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                //     HotModuleReplacement = true
                // });
            }
            else
            {

            }
            Mapper.Initialize(config =>
            {
                // Map enities to dto models
                // Two way mapping
                config.CreateMap<TripVM, Trip>().ReverseMap();
                config.CreateMap<StopVM, Stop>().ReverseMap();

            });
            app.UseStaticFiles();
            app.UseIdentity();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(config =>
            {
                config.MapRoute(
                name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "App", action = "Index" }
                );
            });
            seeder.EnsureSeedData().Wait();
        }
    }
}