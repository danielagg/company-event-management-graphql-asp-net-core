using GraphQL;
using System;

namespace Schema
{
    public class EventSchema : GraphQL.Types.Schema
    {
        public EventSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<QueryType>();
        }
    }
}
