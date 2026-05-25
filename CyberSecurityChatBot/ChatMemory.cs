using System;

namespace CyberSecurityChatBot
{
    // Stores user information to personalise the conversation throughout the session
    public class ChatMemory
    {
        // The users name is captured when they introduce themselves
        public string UserName { get; set; }

        // The users favourite cybersecurity topic is captured when they express interest
        public string FavouriteTopic { get; set; }

        // The last topic discussed is captured to handle follow-up questions
        public string LastTopic { get; set; }

        // Returns true if the users name has been stored
        public bool HasName()
        {
            return !string.IsNullOrEmpty(UserName);
        }

        // Returns true if the users favourite topic has been stored
        public bool HasFavouriteTopic()
        {
            return !string.IsNullOrEmpty(FavouriteTopic);
        }
    }
}
