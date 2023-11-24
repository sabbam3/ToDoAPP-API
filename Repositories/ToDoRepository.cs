using ToDoAPP_API.DB;
using ToDoAPP_API.Models.Entities;
using ToDoAPP_API.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPP_API.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _db;
        public ToDoRepository(AppDbContext db)
        {
            _db = db;   
        }
        public async Task InsertAsync(ToDoEntity entity)
        {
            await _db.Todos.AddAsync(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<ToDoEntity?> FindTaskAsync(int userId, int taskId)
        {
          return await _db.Todos.Where(s => s.UserId == userId && s.Id == taskId).FirstOrDefaultAsync();
        }
        public async Task<List<ToDoEntity>> FindTasksAsync(int userId)
        {
            return await _db.Todos.Where(s => s.UserId == userId).ToListAsync();
        }
    }
}
