using CompanyEventManagement.Persistence.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.GraphQL.Types
{
    public class EventAttendeeType : ObjectGraphType<EventAttendee>
    {
        public EventAttendeeType()
        {
            Field(x => x.Id);
            Field(x => x.EventId);
            Field(x => x.UserId);
            Field(x => x.IsCancelled);
            Field(x => x.JoinedOn);
        }
    }
}
