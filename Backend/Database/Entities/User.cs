using System;

namespace Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
