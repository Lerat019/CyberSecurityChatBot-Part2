using System;

namespace CyberSecurityChatBot
{
    public class ChatMemory
    {
        public string UserName { get; set; }
        public string FavouriteTopic { get; set; }
        public string LastTopic { get; set; }

        public bool HasName()
        {
            return !string.IsNullOrEmpty(UserName);
        }
        public bool HasFavouriteTopic()
        {
            return !string.IsNullOrEmpty(FavouriteTopic);
        }
    }
}

