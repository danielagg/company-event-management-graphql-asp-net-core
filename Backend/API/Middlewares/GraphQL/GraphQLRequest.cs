using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEventManagement.Middlewares.GraphQL
{
    public class GraphQLRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }

        private JObject _variables = new JObject();
        public JObject Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }
    }
}
