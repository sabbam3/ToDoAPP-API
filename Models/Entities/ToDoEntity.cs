namespace ToDoAPP_API.Models.Entities
{
    public enum ToDoStatus
    {
        New = 0,
        Finished = 1,
        Canceled = 2,

    }
    public class ToDoEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title {  get; set; }  
        public string Description { get; set; } 
        public DateTime? DeadLine { get; set; } 
        public ToDoStatus Status { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}
