namespace ChatApp.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int Understanding { get; set; } // Range: 1-5
        public int Pace { get; set; } // Range: 1-5
        public string Comments { get; set; }
    }

}
