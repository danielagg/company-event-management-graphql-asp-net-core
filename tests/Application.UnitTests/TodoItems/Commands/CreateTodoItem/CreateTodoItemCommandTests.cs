using company_event_management.Application.TodoItems.Commands.CreateTodoItem;
using company_event_management.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace company_event_management.Application.UnitTests.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistTodoItem()
        {
            var command = new CreateTodoItemCommand
            {
                Title = "Do yet another thing."
            };

            var handler = new CreateTodoItemCommand.CreateTodoItemCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.TodoItems.Find(result);

            entity.ShouldNotBeNull();
            entity.Title.ShouldBe(command.Title);
        }
    }
}
