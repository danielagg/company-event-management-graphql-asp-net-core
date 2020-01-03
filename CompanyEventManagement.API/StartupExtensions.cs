using CompanyEventManagement.GraphQL;
using CompanyEventManagement.GraphQL.Types;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEventManagement.API
{
    public static class StartupExtensions
    {
        private static void RegisterTypes(IServiceCollection services)
        {
            services
                .AddTransient<MainSchema>()
                .AddTransient<MainQuery>()
                .AddTransient<MainMutation>()
                .AddTransient<UserType>()
                .AddTransient<UserPositionEnumType>()
                .AddTransient<EventType>()
                .AddTransient<EventAttendeeType>();
        }

        public static void SetupGraphQL(this IServiceCollection services)
        {
            RegisterTypes(services);

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            }).AddGraphTypes(ServiceLifetime.Scoped);

            services.AddTransient<IDependencyResolver>(sp => new FuncDependencyResolver(sp.GetRequiredService));
            services.AddTransient<ISchema>(sp => new MainSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }
    }
}
