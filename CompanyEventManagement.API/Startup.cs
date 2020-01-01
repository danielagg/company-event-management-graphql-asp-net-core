using CompanyEventManagement.GraphQL;
using CompanyEventManagement.GraphQL.Types;
using CompanyEventManagement.Persistence;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CompanyEventManagement.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            // Configuration per env
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue;
            });

            services.AddMemoryCache();
            services.AddSession();

            services.AddSingleton(Configuration);

            // GraphQL related
            

            services.Configure<HstsOptions>(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddHttpClient();

            services.AddMvc();

            var databaseConnectionString = "Server=(localdb)\\mssqllocaldb;Database=company-event-management-db;Trusted_Connection=True;ConnectRetryCount=0"; // todo, move to secure vault

            services.AddDbContextPool<AppDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(databaseConnectionString, builder =>
                    {
                        builder.MigrationsAssembly("CompanyEventManagement.Persistence");
                        builder.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                    }));

            services.AddTransient<IDependencyResolver>(sp => new FuncDependencyResolver(sp.GetRequiredService));
            services.AddTransient<MainSchema>();
            services.AddTransient<MainQuery>();
            services.AddTransient<MainMutation>();
            services.AddTransient<UserType>();

            services.AddGraphQL(options => options.ExposeExceptions = true)
                .AddGraphTypes(ServiceLifetime.Scoped);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            app.UseGraphQL<MainSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());


            using var serviceScope = app.ApplicationServices
               .GetRequiredService<IServiceScopeFactory>()
               .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
