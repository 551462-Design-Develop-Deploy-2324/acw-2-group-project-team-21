using System;

namespace ChatApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ReplyToId { get; set; }
        public List<ChatMessage> Replies { get; set; } = new List<ChatMessage>();

        public static ChatMessage FromString(string input)
        {
            var parts = input.Split('|');
            return new ChatMessage
            {
                Username = parts[0],
                Message = parts[1],
                Timestamp = DateTime.Parse(parts[2]),
                ReplyToId = int.TryParse(parts[3], out var replyId) ? replyId : (int?)null
            };
        }

        public override string ToString()
        {
            var replyId = ReplyToId.HasValue ? ReplyToId.ToString() : "";
            return $"{Username}|{Message}|{Timestamp}|{replyId}";
        }
    }
}
