using AutoMapper;
using Domain.Repositories.Interfaces;
using MediatR;

namespace Application.ToDo.Commands.Queries.GetToDoItemDetail
{
    public class GetToDoItemDetailQueryHandler : IRequestHandler<GetToDoItemDetailQuery, ToDoItemVM>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _repository;

        public GetToDoItemDetailQueryHandler(IToDoItemRepository repository, IMapper mapper)
            => (_repository, _mapper) = (repository, mapper);
        public Task<ToDoItemVM> Handle(GetToDoItemDetailQuery request, CancellationToken cancellationToken)
        {
            var item = _repository.GetToDoItem(request.Name);

            return Task.FromResult(_mapper.Map<ToDoItemVM>(item));
        }
    }
}
