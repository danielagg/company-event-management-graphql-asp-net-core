using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.Persistence.Entities
{
    public class EventAttendee : EntityWithId
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime JoinedOn { get; set; }
    }
}
