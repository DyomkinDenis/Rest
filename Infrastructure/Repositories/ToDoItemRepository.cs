using Domain.Entities;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddToDoItem(ToDoItem toDoItem)
        {
            _context.TodoItems.Add(toDoItem);
            _context.SaveChangesAsync();
        }

        public ToDoItem GetToDoItem(Guid id)
        {
            return _context.TodoItems.FirstOrDefault(td=>td.Id == id);
        }

        public ToDoItem GetToDoItem(string name)
        {
            return _context.TodoItems.FirstOrDefault(t=>t.Name == name);
        }
        public ToDoItem[] GetToDoItems(Guid ListId)
        {

            var list = _context.TodoLists.Where(l=>!l.IsDeleted).Include(a => a.Items).SingleOrDefault(l=>l.Id == ListId)?.Items;
            return list.ToArray();
        }

        public void RemoveToDoItem(Guid id)
        {
            var toDo = GetToDoItem(id);
            _context.TodoItems.Remove(toDo);
        }
    }
}
