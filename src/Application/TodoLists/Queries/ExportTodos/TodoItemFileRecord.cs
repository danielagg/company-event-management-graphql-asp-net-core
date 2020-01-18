using company_event_management.Application.Common.Mappings;
using company_event_management.Domain.Entities;

namespace company_event_management.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
