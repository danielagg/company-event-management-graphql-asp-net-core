using CompanyEventManagement.Persistence;
using CompanyEventManagement.Persistence.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(AppDbContext dbContext)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.EmailAddress);
            Field<UserPositionEnumType>("Position");
            Field(x => x.CreatedOn);

            //FieldAsync<ListGraphType<OrderType>>("orders",
            //  resolve: async context => await dbContext.Orders.Where(order => order.UserId == context.Source.Id).ToListAsync());
        }
    }

    public class UserPositionEnumType : EnumerationGraphType<UserPositionType>
    {
        public UserPositionEnumType()
        {
            Name = "Position";
            Description = "Enumeration for the position.";
        }
    }
}
