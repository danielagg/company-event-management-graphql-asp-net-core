using company_event_management.Application.TodoLists.Commands.CreateTodoList;
using company_event_management.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace company_event_management.Application.UnitTests.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistTodoList()
        {
            var command = new CreateTodoListCommand
            {
                Title = "Bucket List"
            };

            var handler = new CreateTodoListCommand.CreateTodoListCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.TodoLists.Find(result);

            entity.ShouldNotBeNull();
            entity.Title.ShouldBe(command.Title);
        }
    }
}
