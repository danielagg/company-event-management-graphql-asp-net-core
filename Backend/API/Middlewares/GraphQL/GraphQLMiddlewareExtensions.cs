using GraphQL.Types;
using Microsoft.AspNetCore.Builder;

namespace CompanyEventManagement.Middlewares.GraphQL
{
    public static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL<TSchema>(
            this IApplicationBuilder builder)
            where TSchema : ISchema
        {
            return builder.UseMiddleware<GraphQLMiddleware<TSchema>>();
        }
    }
}
