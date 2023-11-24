namespace ToDoAPP_API.Models.Requests
{
    public class ChangeTaskStatusRequest
    {
        
        public int TaskId { get; set; }
        public int Status { get; set; }
    }
}
