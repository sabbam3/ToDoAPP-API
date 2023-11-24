using ToDoAPP_API.Models.Entities;

namespace ToDoAPP_API.Abstraction
{
    public interface IToDoRepository
    {
        Task InsertAsync(ToDoEntity entity);
        Task SaveChangesAsync();
        Task<ToDoEntity?> FindTaskAsync(int userId, int taskId);
        Task<List<ToDoEntity>> FindTasksAsync(int userId);
    }
        
        
}
