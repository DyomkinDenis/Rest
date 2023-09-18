using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repository;
        private readonly IToDoItemRepository _itemRepository;
        public class FilterAndPaginationRequest
        {
            public string? OrderBy { get; set; }
            public string? Filter { get; set; }
            public int Skip { get; set; } = 0;
            public int Take { get; set; } = 10;
        }
        public ToDoListService(IToDoListRepository repository, IToDoItemRepository itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
        }
        public Guid AddToDoList(ToDoListDTO toDoListDTO)
        {
            var toDoList = new ToDoList
            {
                Id = Guid.NewGuid(),
                Name = toDoListDTO.Name,
                Description = toDoListDTO.Description,
            };
            return _repository.AddToDoList(toDoList);
        }
        public void UpdateToDoList(ToDoListDTO toDoListDTO)
        {
            var toDoList = _repository.GetToDoList(toDoListDTO.Id);

            toDoList.Description = toDoListDTO.Description;
            toDoList.Name = toDoListDTO.Name;

            _repository.UpdateToDoList(toDoList);
        }
        public void ModifyToDoList(ToDoListDTO toDoListDTO)
        {
            var toDoList = _repository.GetToDoList(toDoListDTO.Id);

            toDoList.Description = string.IsNullOrEmpty(toDoListDTO.Description)? toDoList.Description : toDoListDTO.Description;
            toDoList.Name = string.IsNullOrEmpty(toDoListDTO.Name) ? toDoList.Name : toDoListDTO.Name;

            _repository.UpdateToDoList(toDoList);
        }

        public ToDoListDTO GetToDoList(string name)
        {
            var toDoList = _repository.GetToDoList(name);

            return new ToDoListDTO
            {
                Id = toDoList.Id,
                Name = toDoList.Name,
                Description = toDoList.Description
            };
        }

        public ToDoListDTO GetToDoList(Guid Id)
        {
            var toDoList = _repository.GetToDoList(Id);

            return new ToDoListDTO
            {
                Id = toDoList.Id,
                Name = toDoList.Name,
                Description = toDoList.Description
            };
        }
        public ToDoListDTO[] GetToDoLists()
        {
            var toDoLists = _repository.GetToDoLists();

            return toDoLists.Select(l=>new ToDoListDTO { Id = l.Id,Description = l.Description, Name = l.Name }).ToArray();
        }
        public ToDoListDTO[] GetToDoLists(FilterAndPaginationRequest request)
        {
            var toDoLists = _repository.GetToDoLists();


            if (!string.IsNullOrEmpty(request.Filter))
            {
                var splitted = request.Filter.Split(' ');
                if (splitted.Length > 2)
                {
                    var collumn = splitted[0].ToLower();
                    var stringOper = splitted[1].ToLower();
                    var value = splitted[2];

                    Expression<Func<ToDoList, bool>> expr = (collumn, stringOper) switch
                    {
                        ("name", "eq") => list => list.Name == value,
                        ("name", "ne") => list => list.Name != value,
                        ("description", "eq") => list => list.Description == value,
                        ("description", "ne") => list => list.Description != value,
                        _ => list => true,
                    };

                    toDoLists = toDoLists.Where(expr);
                }
            }

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                var splitted = request.OrderBy.Split(' ');
                var collumn = splitted[0];
                var order = splitted.Length > 1 ? splitted[1].ToLower() : "asc";

                Expression<Func<ToDoList, object>> keySelector = collumn switch
                {
                    "name" => list => list.Name,
                    "description" => list => list.Description,
                    "id" => list => list.Id,
                    _ => list => list.Id,
                };

                if (order == "desc")
                {
                    toDoLists = toDoLists.OrderByDescending(keySelector);
                }
                else
                {
                    toDoLists = toDoLists.OrderBy(keySelector);
                }
            }

            toDoLists = toDoLists.Skip(request.Skip).Take(request.Take);

            return toDoLists.Select(l => new ToDoListDTO { Id = l.Id, Description = l.Description, Name = l.Name }).ToArray();
        }

        public ToDoItemDTO[] GetToDoListItems(Guid Id)
        {
            var toDoItems = _itemRepository.GetToDoItems(Id);
            var items = toDoItems.Select(i => new ToDoItemDTO { Description = i.Description, Id = i.Id, Name = i.Name }).ToArray();
            return items;
        }

        public void DeleteToDoList(Guid Id)
        {
            _repository.MarkAsDeletedToDoList(Id);
        }
    }
}
