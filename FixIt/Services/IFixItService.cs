using FixIt.Models;

namespace FixIt.Services
{
    public interface IFixItService
    {
        Task<ToDoItem[]> GetIncompleteAsync();
        Task<bool> AddItemAsync(ToDoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
