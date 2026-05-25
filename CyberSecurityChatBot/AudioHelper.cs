using System;
using System.Media;
using System.IO;

namespace CyberSecurityChatBot
{
    // Handles audio playback for the chatbot's voice greeting
    public class AudioHelper
    {
        // Plays the WAV voice greeting file when the application starts
        public static void PlayVoiceGreeting()
        {
            string audioFilePath = "greeting.wav";

            // Check if the audio file exists before attempting to play it
            if (File.Exists(audioFilePath))
            {
                try
                {
                    // Play the greeting synchronously so it finishes before the UI loads
                    using (SoundPlayer player = new SoundPlayer(audioFilePath))
                    {
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    // Display error message if audio playback fails
                    Console.WriteLine("Error playing audio: " + ex.Message);
                }
            }
        }
    }
}