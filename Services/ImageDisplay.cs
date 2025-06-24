using ChatBotCyber.Models;
using ChatBotCyber.ViewModels;
using System;
using System.Windows;

namespace ChatBotCyber.Services
{
    public class ImageDisplay
    {
        public void ShowAsciiArt()
        {
            string asciiArt = @"
  ____          _          _       _   _                  
 / ___|   _ ___| |__   ___| |__   | | | | __ _ _ __ _   _ 
| |  | | | / __| '_ \ / _ \ '_ \  | |_| |/ _` | '__| | | |
| |__| |_| \__ \ | | |  __/ |_) | |  _  | (_| | |  | |_| |
 \____\__,_|___/_| |_|\___|_.__/  |_| |_|\__,_|_|   \__, |
                                                     |___/ 
 Cybersecurity Awareness Bot
-----------------------------------------------";

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow &&
                        mainWindow.DataContext is MainViewModel vm)
                    {
                        vm.Messages.Add(new ChatMessage("CyberBot", asciiArt));
                    }
                });
            }
        }
    }
}