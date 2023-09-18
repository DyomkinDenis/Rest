using Domain.Entities;

namespace Domain.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        Guid AddToDoList(ToDoList toDoList);
        void UpdateToDoList(ToDoList toDoList);
        ToDoList GetToDoList(Guid id);
        ToDoList GetToDoList(string name);
        void RemoveToDoList(Guid id);
        IQueryable<ToDoList> GetToDoLists();
        void MarkAsDeletedToDoList(Guid id);
    }
}
