using FixIt.Data;
using FixIt.Models;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Services
{
    public class FixItService : IFixItService
    {
        private readonly ApplicationDbContext context;
        public FixItService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddItemAsync(ToDoItem newItem)
        {
            newItem.ToDoItemId = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(8);
            context.Add(newItem);
            var saveResult = await context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<ToDoItem[]> GetIncompleteAsync()
        {
            var items = await context.ToDoItems
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await context.ToDoItems
                .Where(x => x.ToDoItemId == id)
                .SingleOrDefaultAsync();
            if (item == null) return false;
            item.IsDone = true;
            var saveResult = await context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated

        }
    }
}
