using company_event_management.Application.Common.Mappings;
using company_event_management.Domain.Entities;
using System.Collections.Generic;

namespace company_event_management.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
}
