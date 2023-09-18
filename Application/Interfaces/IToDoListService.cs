using Application.DTO;
using static Application.Services.ToDoListService;

namespace Application.Interfaces
{
    public interface IToDoListService
    {
        Guid AddToDoList(ToDoListDTO toDoListDTO);
        void ModifyToDoList(ToDoListDTO toDoListDTO);
        void UpdateToDoList(ToDoListDTO toDoListDTO);

        ToDoListDTO GetToDoList(string name);
        ToDoListDTO GetToDoList(Guid Id);

        void DeleteToDoList(Guid Id);

        ToDoItemDTO[] GetToDoListItems(Guid Id);

        ToDoListDTO[] GetToDoLists();
        ToDoListDTO[] GetToDoLists(FilterAndPaginationRequest request);
    }
}
