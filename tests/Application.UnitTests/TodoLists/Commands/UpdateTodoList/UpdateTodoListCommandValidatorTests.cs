using company_event_management.Application.TodoLists.Commands.UpdateTodoList;
using company_event_management.Application.UnitTests.Common;
using company_event_management.Domain.Entities;
using Shouldly;
using Xunit;

namespace company_event_management.Application.UnitTests.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public void IsValid_ShouldBeTrue_WhenListTitleIsUnique()
        {
            var command = new UpdateTodoListCommand
            {
                Id = 1,
                Title = "Shopping"
            };

            var validator = new UpdateTodoListCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
        {
            Context.TodoLists.Add(new TodoList { Title = "Shopping" });
            Context.SaveChanges();

            var command = new UpdateTodoListCommand
            {
                Id = 2,
                Title = "Shopping"
            };

            var validator = new UpdateTodoListCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}
