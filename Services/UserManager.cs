using ChatBotCyber.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ChatBotCyber.Services
{
    public class UserManager
    {
        private const string FilePath = "user_profiles.json";
        private Dictionary<string, UserProfile> _users = new Dictionary<string, UserProfile>(StringComparer.OrdinalIgnoreCase);

        public UserManager()
        {
            LoadUsers();
        }

        public UserProfile GetOrCreateUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new UserProfile { Name = "Guest" };

            if (_users.TryGetValue(name, out var user))
            {
                user.LastSeen = DateTime.Now;
                return user;
            }

            var newUser = new UserProfile
            {
                Name = name,
                FavoriteTopic = "cybersecurity"  // Default topic
            };
            _users[name] = newUser;
            SaveUsers();
            return newUser;
        }

        public void UpdateFavoriteTopic(string name, string topic)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            if (_users.TryGetValue(name, out var user))
            {
                user.FavoriteTopic = topic ?? "cybersecurity";
                user.LastSeen = DateTime.Now;
                SaveUsers();
            }
        }

        private void LoadUsers()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string json = File.ReadAllText(FilePath);
                    _users = JsonSerializer.Deserialize<Dictionary<string, UserProfile>>(json)
                             ?? new Dictionary<string, UserProfile>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading users: {ex.Message}");
                }
            }
        }

        private void SaveUsers()
        {
            try
            {
                string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }
        }
    }
}