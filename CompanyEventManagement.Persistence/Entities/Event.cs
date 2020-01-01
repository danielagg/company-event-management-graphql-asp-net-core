using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEventManagement.Persistence.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public int OrganizerUserId { get; set; }
        public virtual User OrganizerUser { get; set; }
        public int ModifiedByUserId { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual List<User> Attendees { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
