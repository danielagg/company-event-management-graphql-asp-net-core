using CompanyEventManagement.GraphQL;
using CompanyEventManagement.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEventManagement.API
{
    public static class StartupExtensions
    {
        public static void SetupGraphQL(this IServiceCollection services)
        {
            services
               .AddTransient<MainSchema>()
               .AddTransient<MainQuery>()
               .AddTransient<MainMutation>()
               .AddTransient<UserType>()
               .AddTransient<UserPositionEnumType>();
        }
    }
}
