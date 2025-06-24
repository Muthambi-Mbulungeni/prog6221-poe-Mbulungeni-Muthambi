using System;
using System.IO;
using System.Media;
using System.Windows;

namespace ChatBotCyber.Services
{
    public class AudioPlayer
    {
        public void PlayWelcomeAudio()
        {
            if (!OperatingSystem.IsWindows()) return;

            try
            {
                string filePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MediaAssets",
                    "welcome_audio.wav"
                );

                if (File.Exists(filePath))
                {
                    using SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Audio error: {ex.Message}");
            }
        }
    }
}