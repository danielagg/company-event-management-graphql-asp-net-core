using CompanyEventManagement.Persistence.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.GraphQL.Types
{
    public class EventType : ObjectGraphType<Event>
    {
        public EventType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
        }
    }
}
