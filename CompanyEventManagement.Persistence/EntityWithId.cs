using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.Persistence
{
    public interface EntityWithId
    {
        int Id { get; set; }
    }
}
