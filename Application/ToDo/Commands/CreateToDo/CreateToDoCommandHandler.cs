using Domain.Repositories.Interfaces;
using MediatR;

namespace Application.ToDo.Commands.CreateToDo
{
    public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Guid>
    {
        private readonly IToDoItemRepository _repository;

        public CreateToDoCommandHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }
        public Task<Guid> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = new Domain.Entities.ToDoItem()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };

            _repository.AddToDoItem(toDo);


            return Task.FromResult(toDo.Id);
        }
    }
}
