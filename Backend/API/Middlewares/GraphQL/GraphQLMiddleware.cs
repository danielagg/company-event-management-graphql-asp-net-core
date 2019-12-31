using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyEventManagement.Middlewares.GraphQL
{
    public class GraphQLMiddleware<TSchema>
        where TSchema : ISchema 
    {
        private readonly RequestDelegate next;
        private readonly TSchema schema;
        private readonly IDocumentExecuter executer;
        private readonly IDocumentWriter writer;

        public GraphQLMiddleware(
            RequestDelegate next,
            TSchema schema,
            IDocumentExecuter executer,
            IDocumentWriter writer)
        {
            this.next = next;
            this.schema = schema;
            this.executer = executer;
            this.writer = writer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Method.ToLower() != "post" && context.Request.Path != "/graphql")
            {
                await next(context);
                return;
            }

            var requestBody = DeserializeFromStream<GraphQLRequest>(context.Request.Body);

            var result = await executer.ExecuteAsync(new ExecutionOptions
            {
                Schema = schema,
                Query = requestBody.Query,
                OperationName = requestBody.OperationName,
                Inputs = requestBody.Variables.ToInputs()
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)(result.Errors?.Any() ?? false
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.OK);

            var resultBody = await writer.WriteToStringAsync(result);
            await context.Response.WriteAsync(resultBody);

            // deliberately not calling next
        }

        public T DeserializeFromStream<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}
