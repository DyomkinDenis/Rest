using MediatR;

namespace Application.ToDo.Commands.Queries.GetToDoItemDetail
{
    public class GetToDoItemDetailQuery : IRequest<ToDoItemVM>
    {
        public string Name { get; set; }
    }
}
