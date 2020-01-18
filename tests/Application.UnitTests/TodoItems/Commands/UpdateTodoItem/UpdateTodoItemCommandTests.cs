using company_event_management.Application.Common.Exceptions;
using company_event_management.Application.TodoItems.Commands.UpdateTodoItem;
using company_event_management.Application.UnitTests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace company_event_management.Application.UnitTests.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldUpdatePersistedTodoItem()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 1,
                Title = "This thing is also done.",
                Done = true
            };

            var handler = new UpdateTodoItemCommand.UpdateTodoItemCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.TodoItems.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.Title.ShouldBe(command.Title);
            entity.Done.ShouldBeTrue();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 99,
                Title = "This item doesn't exist.",
                Done = false
            };

            var sut = new UpdateTodoItemCommand.UpdateTodoItemCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() => 
                sut.Handle(command, CancellationToken.None));
        }
    }
}