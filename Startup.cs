using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.AspNetCore.Identity.SQLServer;
using Serilog;
using Squire.Core.Middlewares;
using war3playground.BusinessLogic.DatabaseContexts;
using war3playground.BusinessLogic.Services;
using war3playground.BusinessLogic.Services.Interfaces;
using war3playground.BusinessLogic.Settings;

namespace war3playground
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly ILogger logger;
        private readonly ISettings settings;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("localappsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            settings = new Settings(Configuration);
            logger = configureLogger(settings);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddSingleton(p => settings);
            services.AddDbContextPool<W3PContext>(p => p.UseSqlServer(settings.ConnectionString));
            services.AddSingleton(p => logger);

            services.AddMvc(config =>
            {
                config.ModelBinderProviders.Insert(0, new Piranha.Manager.Binders.AbstractModelBinderProvider());
            });

            var accessor = new HttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor>(p => accessor);

            services.AddPiranhaApplication();
            services.AddPiranhaFileStorage();
            services.AddPiranhaImageSharp();
            services.AddPiranhaEF(options =>
                options.UseSqlServer(settings.ConnectionString));
            services.AddPiranhaIdentityWithSeed<IdentitySQLServerDb>(options =>
                options.UseSqlServer(settings.ConnectionString));
            services.AddPiranhaManager();
            services.AddPiranhaMemCache();

            //Services
            services.AddScoped<IPlayerService, PlayerService>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, IApi api)
        {
            // Initialize Piranha
            App.Init();

            // Configure cache level
            App.CacheLevel = Piranha.Cache.CacheLevel.Basic;

            // Build content types
            var pageTypeBuilder = new Piranha.AttributeBuilder.PageTypeBuilder(api)
                .AddType(typeof(Models.BlogArchive))
                .AddType(typeof(Models.StandardPage));
            pageTypeBuilder.Build()
                .DeleteOrphans();
            var postTypeBuilder = new Piranha.AttributeBuilder.PostTypeBuilder(api)
                .AddType(typeof(Models.BlogPost))
                .AddType(typeof(Models.KingOfTheHillPost));
            postTypeBuilder.Build()
                .DeleteOrphans();
            var siteTypeBuilder = new Piranha.AttributeBuilder.SiteTypeBuilder(api)
                .AddType(typeof(Models.BlogSite));
            siteTypeBuilder.Build()
                .DeleteOrphans();

            // Register middleware
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseGlobalErrorHandler(true);
            app.UseCorrelationToken();
            app.UseRequestLogging();
            app.UsePerformanceLogging(settings.PerformaceWarningMinimumInMS);
            app.UseAuthentication();
            app.UsePiranha();
            app.UsePiranhaManager();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");
            });

            logger.Debug($"Service started (v{Program.GetVersion})");
        }

        private ILogger configureLogger(ISettings settings)
        {
            return new LoggerConfiguration()
              .Enrich.FromLogContext()
              .MinimumLevel.Verbose()
              .WriteTo.ColoredConsole(settings.Logging.LogLevel, "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
              .WriteTo.MSSqlServer(settings.ConnectionString, "Logs", settings.Logging.LogLevel, columnOptions: settings.Logging.ColumnOptions)
              .CreateLogger();
        }
    }
}
