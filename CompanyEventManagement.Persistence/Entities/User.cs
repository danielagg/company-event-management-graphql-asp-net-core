using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEventManagement.Persistence.Entities
{
    public class User : EntityWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public UserPositionType Position { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public enum UserPositionType
    {
        JuniorDeveloper,
        Developer,
        SeniorDeveloper,
        TeachnicalTeamLead,
        Manager,
        Admin,
        SysAdmin,
        Other
    }
}
