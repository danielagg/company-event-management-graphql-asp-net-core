using GraphQL;
using GraphQL.Types;

namespace CompanyEventManagement.GraphQL
{
    public class MainSchema : Schema
    {
        public MainSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MainQuery>();
            Mutation = resolver.Resolve<MainMutation>();
        }
    }
}
