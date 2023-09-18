using Domain.Entities;

namespace Domain.Repositories.Interfaces
{
    public interface IToDoItemRepository
    {
        void AddToDoItem(ToDoItem toDoItem);

        ToDoItem GetToDoItem(Guid id);
        ToDoItem GetToDoItem(string name);
        void RemoveToDoItem(Guid id);

        ToDoItem[] GetToDoItems(Guid ListId);
    }
}
