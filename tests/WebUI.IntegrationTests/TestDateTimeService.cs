using company_event_management.Application.Common.Interfaces;
using System;

namespace company_event_management.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
