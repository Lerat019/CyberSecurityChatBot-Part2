using System;
using System.Media;
using System.IO;

namespace CyberSecurityChatBot
{
    public class AudioHelper
    {

        public static void PlayVoiceGreeting()
        {
            string audioFilePath = "greeting.wav";

            if (File.Exists(audioFilePath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioFilePath))
                    {
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error playing audio: " + ex.Message);
                }
            }
        }
    }
}
