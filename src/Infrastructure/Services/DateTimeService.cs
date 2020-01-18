using company_event_management.Application.Common.Interfaces;
using System;

namespace company_event_management.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
