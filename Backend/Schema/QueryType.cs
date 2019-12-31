using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schema
{
    public class QueryType : ObjectGraphType
    {
        public QueryType()
        {
            Name = "Query";

            Field<NonNullGraphType<StringGraphType>>(
                name: "message",
                resolve: context => "Hello world");
        }
    }
}
