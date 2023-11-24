using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoAPP_API.Abstraction;
using ToDoAPP_API.Models.Dto;
using ToDoAPP_API.Models.Entities;
using ToDoAPP_API.Models.Requests;

namespace ToDoAPP_API.Controllers
{
    [ApiController]
    [Route("ToDo/controller")]

    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;
        private readonly UserManager<UserEntity> _entity;
        public ToDoController(IToDoService service, UserManager<UserEntity> entity)
        {
            _entity = entity;
            _service = service;
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("ToDoCreate")]
        public async Task<IActionResult> CreateToDoAsync(ToDoCreateRequest request)
        {
            var user = await _entity.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            else
            {
                await _service.CreateToDoAsync(request, user.Id);
                return Ok();
            }
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("change-to-do-status")]
        public async Task<IActionResult> ChangeTaskStatus(ChangeTaskStatusRequest request)
        {
            var user = await _entity.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            else
            {
                await _service.ChangeTaskStatusAsync(request, user.Id);
                return Ok();
            }
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-user-to-do-list")]
        public async Task<IActionResult> GetToDoList()
        {
            var user = await _entity.GetUserAsync(User);
            if (user == null) return NotFound();
            else return Ok(await _service.GetToDoListAsync(user.Id));
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("find-user-to-do")]
        public async Task<IActionResult> FindToDoAsync(string? title, string? description)
        {
           
            var user = await _entity.GetUserAsync(User);
            if (user == null) return NotFound();
            else return Ok(await _service.FindToDoAsync(user.Id, description, title));
        }
    }
}
