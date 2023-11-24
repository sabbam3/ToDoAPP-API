using System.Security.Claims;
using ToDoAPP_API.Models.Dto;
using ToDoAPP_API.Models.Entities;
using ToDoAPP_API.Models.Requests;

namespace ToDoAPP_API.Abstraction
{
    public interface IToDoService
    {
        Task CreateToDoAsync(ToDoCreateRequest request, int userId);
        Task ChangeTaskStatusAsync(ChangeTaskStatusRequest request, int userId);
        Task<List<GetToDoListDto>> GetToDoListAsync(int userId);
        Task<GetToDoListDto?> FindToDoAsync(int userId, string description, string title);
    }
}
