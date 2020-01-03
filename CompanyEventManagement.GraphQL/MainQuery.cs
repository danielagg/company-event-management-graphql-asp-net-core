using CompanyEventManagement.GraphQL.Types;
using CompanyEventManagement.Persistence;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEventManagement.GraphQL
{
    public class MainQuery : ObjectGraphType
    {
        public MainQuery(AppDbContext dbContext)
        {
            CreateUserQueries(dbContext);
            CreateEventQueries(dbContext);
        }

        #region users

        private void CreateUserQueries(AppDbContext dbContext)
        {
            FieldAsync<ListGraphType<UserType>>("users",
                resolve: async _ => await dbContext.Users.ToListAsync());

            FieldAsync<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                        Description = "User id"
                    }),
                resolve: async context => await FindEntityById(context, dbContext.Users));

            FieldAsync<ListGraphType<UserType>>("eventAttendes",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>>()
                   {
                       Name = "eventId",
                       Description = "Event id"
                   }),
               resolve: async context => await dbContext.Attendees
                .Where(a => a.EventId == context.GetArgument<int>("eventId", 0))
                .Select(a => a.User)
                .ToListAsync());
        }

        #endregion

        #region events

        private void CreateEventQueries(AppDbContext dbContext)
        {
            FieldAsync<ListGraphType<EventType>>("events",
                resolve: async _ => await dbContext.Events.ToListAsync());

            FieldAsync<EventType>("event",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>>()
                   {
                       Name = "id",
                       Description = "Event id"
                   }),
               resolve: async context => await FindEntityById(context, dbContext.Events));
        }

        #endregion

        #region utils

        private async Task<T> FindEntityById<T>(ResolveFieldContext<object> context, DbSet<T> dbSet)
            where T : class, EntityWithId
        {
            return await dbSet
                .FirstOrDefaultAsync(u => u.Id == context.GetArgument<int>("id", 0));
        }

        #endregion
    }
}
