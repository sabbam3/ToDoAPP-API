using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoAPP_API.Abstraction;
using ToDoAPP_API.Models.Dto;
using ToDoAPP_API.Models.Entities;
using ToDoAPP_API.Models.Requests;

namespace ToDoAPP_API.Services
{
    public class ToDoService : IToDoService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IToDoRepository _repository;
        public ToDoService(IToDoRepository repository, UserManager<UserEntity> userManager)
        {
            _repository = repository;
            _userManager = userManager;

        }
        public async Task CreateToDoAsync(ToDoCreateRequest request,int userId)
        {
            
            var entity = new ToDoEntity();
            entity.UserId = userId;
            entity.Title = request.Title;
            entity.Status = ToDoStatus.New;
            entity.Description = request.Description;
            entity.DeadLine = request.Deadline;
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();
            
        }
        public async Task ChangeTaskStatusAsync(ChangeTaskStatusRequest request, int userId)
        {
            var task = await _repository.FindTaskAsync(userId, request.TaskId);
            if(task != null) { task.Status = (ToDoStatus)request.Status; }
            await _repository.SaveChangesAsync();
        }
        public async Task<List<GetToDoListDto>> GetToDoListAsync(int userId)
        {
            var toDos = await _repository.FindTasksAsync(userId);
            var toDoList = toDos.Select(s => new GetToDoListDto()
            {
                Title = s.Title,
                Description = s.Description,
                Status = s.Status.ToString(),
                DeadLine = s.DeadLine.ToString()
            }).ToList();
            return toDoList;
        }
        public async Task<GetToDoListDto?> FindToDoAsync(int userId, string? description, string? title)
        {
            var toDos = await _repository.FindTasksAsync(userId);
            var toDo = toDos.Where(s => s.Title.ToLower() == title || s.Description.ToLower() == description)
                .ToList();
                var toDoDto = toDo.Select(s => new GetToDoListDto()
                {
                    Title = s.Title,
                    Description = s.Description,
                    Status = s.Status.ToString(),
                    DeadLine = s.DeadLine.ToString()
                }).FirstOrDefault();
            return toDoDto;
        }
    }
}
