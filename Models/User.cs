namespace ChatApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int SupervisorId { get; set; }
    }
}
