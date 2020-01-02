using CompanyEventManagement.GraphQL.Types;
using CompanyEventManagement.Persistence;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.GraphQL
{
    public class MainQuery : ObjectGraphType
    {
        public MainQuery(AppDbContext dbContext)
        {
             FieldAsync<ListGraphType<Types.UserType>>("users",
                resolve: async _ => await dbContext.Users.ToListAsync());

            FieldAsync<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id", Description = "User id" }),
                resolve: async context => await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == context.GetArgument<int>("id", 0)));
        }
    }
}
