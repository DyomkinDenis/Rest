using MediatR;

namespace Application.ToDo.Commands.CreateToDo
{
    public class CreateToDoCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
