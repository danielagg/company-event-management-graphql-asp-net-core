using CompanyEventManagement.Persistence;
using CompanyEventManagement.Persistence.Entities;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEventManagement.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        private readonly AppDbContext dbContext;

        public UserType(AppDbContext dbContext)
        {
            this.dbContext = dbContext;

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.EmailAddress);
            Field<UserPositionEnumType>("Position");
            Field(x => x.CreatedOn);

            FieldAsync<ListGraphType<EventType>>("events",
              resolve: async context => await ResolveUserAttendingEvents(context));             
        }

        private async Task<List<Event>> ResolveUserAttendingEvents(ResolveFieldContext<User> context)
        {
            var events = await dbContext.Attendees
                .Where(a => a.UserId == context.Source.Id)
                .Select(a => a.Event)
                .ToListAsync();

            return events;
        }
    }
}
