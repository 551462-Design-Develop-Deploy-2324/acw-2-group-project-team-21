using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ChatRoom
    {
        public string RoomName { get; set; }
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
