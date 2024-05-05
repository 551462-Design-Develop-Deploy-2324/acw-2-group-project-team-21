namespace ChatApp.Models
{
    public class FeedbackViewModel
    {
        public int Understanding { get; set; }
        public int Pace { get; set; }
        public string Comments { get; set; }

        public string RagUnderstandingColor
        {
            get
            {
                // Determine color based on Understanding score
                if (Understanding >= 1 && Understanding <= 2)
                    return "red";
                else if (Understanding == 3)
                    return "amber";
                else if (Understanding >= 4 && Understanding <= 5)
                    return "green";
                else
                    return "black"; // Default color
            }
        }

        public string RagPaceColor
        {
            get
            {
                // Determine color based on Pace score
                if (Pace >= 1 && Pace <= 2)
                    return "red";
                else if (Pace == 3)
                    return "amber";
                else if (Pace >= 4 && Pace <= 5)
                    return "green";
                else
                    return "black"; // Default color
            }
        }
    }

}