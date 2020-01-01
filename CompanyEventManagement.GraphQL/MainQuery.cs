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

        }
    }
}
