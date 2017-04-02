using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

using TheWorld.Services;
using TheWorld.Models;

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
            services.AddDbContext<WorldContext>();
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddTransient<WorldContextSeedData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // write Debug.Write into log
            loggerFactory.AddDebug();
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();  // Show exception page
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            seeder.EnsureSeedData().Wait();
        }
    }
}