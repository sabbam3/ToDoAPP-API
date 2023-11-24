using ToDoAPP_API.Models.Entities;

namespace ToDoAPP_API.Models.Dto
{
    
    public class GetToDoListDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? DeadLine { get; set; }
        public string Status { get; set; }    
    }
}
