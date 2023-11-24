namespace ToDoAPP_API.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string ResetPasswordToken { get; set; }  
        public string Password { get; set; }    
    }
}
