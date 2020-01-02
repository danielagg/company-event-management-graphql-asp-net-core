using CompanyEventManagement.Persistence.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEventManagement.GraphQL.Types
{
    public class UserPositionEnumType : EnumerationGraphType<UserPositionType>
    {
        public UserPositionEnumType()
        {
            Name = "Position";
            Description = "Enumeration for the position.";
        }
    }
}
