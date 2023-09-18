using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<ToDoList> TodoLists { get; }

        DbSet<Domain.Entities.ToDoItem> TodoItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
