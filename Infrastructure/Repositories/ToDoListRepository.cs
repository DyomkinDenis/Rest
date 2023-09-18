using Domain.Entities;
using Domain.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Infrastructure.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Guid AddToDoList(ToDoList toDoList)
        {
            _context.TodoLists.Add(toDoList);
            _context.SaveChangesAsync();
            return toDoList.Id;
        }

        public void UpdateToDoList(ToDoList toDoList)
        {
            if(_context.TodoLists.Any(l=>l.Id == toDoList.Id && !l.IsDeleted))
            {
                _context.TodoLists.Update(toDoList);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Списка не существует");
            }
        }

        public IQueryable<ToDoList> GetToDoLists()
        {
            return _context.TodoLists.Where(l => !l.IsDeleted);
        }

        public ToDoList GetToDoList(Guid id)
        {
            return _context.TodoLists.Where(l => !l.IsDeleted).FirstOrDefault(tl=>tl.Id == id);
        }

        public ToDoList GetToDoList(string name)
        {
            return _context.TodoLists.Where(l => !l.IsDeleted).FirstOrDefault(tl => tl.Name == name);
        }

        public void RemoveToDoList(Guid id)
        {
            var list = _context.TodoLists.Where(l => !l.IsDeleted).FirstOrDefault(tl => tl.Id == id);

            if (list != null)
            {
                _context.TodoLists.Remove(list);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Списка не существует");
            }
        }

        public void MarkAsDeletedToDoList(Guid id)
        {
            var list = GetToDoList(id);
            if (list != null)
            {
                list.IsDeleted = true;
                _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Списка не существует");
            }
        }
    }
}
