using CompanyEventManagement.Middlewares.GraphQL;
using Database;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schema;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace CompanyEventManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            // Configuration per env
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //builder.AddUserSecrets<Startup>();
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddHttpContextAccessor();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
            });

            // in-memory cache for tokens and subscriptions. Production apps will typically use some method of persistent storage.
            services.AddMemoryCache();
            services.AddSession();

            // Add application services.
            services.AddSingleton(Configuration);

            // GraphQL related
            services.AddTransient<IDependencyResolver>(serviceProvider =>
            {
                return new FuncDependencyResolver(serviceProvider.GetService);
            });
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddTransient<QueryType>();
            services.AddTransient<EventSchema>();

            services.Configure<HstsOptions>(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var databaseConnectionString = "Server=(localdb)\\mssqllocaldb;Database=CompanyEventManagement-db;Trusted_Connection=True;ConnectRetryCount=0"; // todo, move to secure vault

            services.AddDbContextPool<AppDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(databaseConnectionString, builder =>
                    {
                        builder.MigrationsAssembly("CompanyEventManagement");
                        builder.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                    }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            app.UseGraphQL<EventSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            using (var serviceScope = app.ApplicationServices
               .GetRequiredService<IServiceScopeFactory>()
               .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
