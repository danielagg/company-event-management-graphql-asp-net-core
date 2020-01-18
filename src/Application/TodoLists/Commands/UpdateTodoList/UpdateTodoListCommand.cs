using company_event_management.Application.Common.Exceptions;
using company_event_management.Application.Common.Interfaces;
using company_event_management.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace company_event_management.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateTodoListCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TodoLists.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoList), request.Id);
                }

                entity.Title = request.Title;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
