using company_event_management.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace company_event_management.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
