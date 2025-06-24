using ChatBotCyber.Models;
using ChatBotCyber.Services;
using ChatBotCyber.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ChatBotCyber.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BotService _botService;
        private readonly UserManager _userManager;
        private readonly AudioPlayer _audioPlayer;
        private readonly ImageDisplay _imageDisplay;
        private string _userInput = string.Empty;
        private string _userName = string.Empty;
        private bool _expectingFavoriteTopic = false;
        private UserProfile _currentUser = new UserProfile(); // Initialize with new instance

        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();
        public ICommand SendCommand { get; }
        public ICommand ShowTasksCommand { get; }
        public ICommand StartQuizCommand { get; }
        public ICommand ShowHelpCommand { get; }

        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _botService = new BotService();
            _userManager = new UserManager();
            _audioPlayer = new AudioPlayer();
            _imageDisplay = new ImageDisplay();

            SendCommand = new RelayCommand(SendMessage);
            ShowTasksCommand = new RelayCommand(ShowTaskWindow);
            StartQuizCommand = new RelayCommand(StartQuiz);
            ShowHelpCommand = new RelayCommand(ShowHelp);

            InitializeChat();
        }

        private void InitializeChat()
        {
            _audioPlayer.PlayWelcomeAudio();
            _imageDisplay.ShowAsciiArt();
            Messages.Add(new ChatMessage("CyberBot", "Hi, I'm CyberBot! What's your name?"));
        }

        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;

            Messages.Add(new ChatMessage("You", UserInput));

            string response;
            if (string.IsNullOrEmpty(_userName))
            {
                _userName = UserInput.Trim();
                _currentUser = _userManager.GetOrCreateUser(_userName);
                response = $"Nice to meet you, {_userName}! What's your favorite cybersecurity topic?";
                _expectingFavoriteTopic = true;
            }
            else if (_expectingFavoriteTopic)
            {
                // Capture favorite topic
                string topic = ExtractTopicFromInput(UserInput);
                _currentUser.FavoriteTopic = topic;
                _userManager.UpdateFavoriteTopic(_userName, topic);
                response = $"Awesome, {_userName}! I'll remember you like {topic}. How can I help you today?";
                _expectingFavoriteTopic = false;
            }
            else
            {
                response = _botService.ProcessInput(UserInput, _userName, _currentUser.FavoriteTopic);
            }

            Messages.Add(new ChatMessage("CyberBot", response ?? string.Empty));
            UserInput = string.Empty;
        }

        private string ExtractTopicFromInput(string input)
        {
            string lowered = input.ToLower();
            var topics = _botService.GetAllTopics();

            foreach (var topic in topics)
            {
                if (lowered.Contains(topic))
                {
                    return topic;
                }
            }

            // Default topic if none matched
            return "cybersecurity";
        }

        private void ShowTaskWindow()
        {
            new TaskWindow().ShowDialog();
        }

        private void StartQuiz()
        {
            string response = _botService.StartQuiz();
            Messages.Add(new ChatMessage("CyberBot", response));
        }

        private void ShowHelp()
        {
            string helpText = _botService.GetHelpText();
            Messages.Add(new ChatMessage("CyberBot", helpText));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}