using System.Windows;
using System.Windows.Input;
using ChatBotCyber.ViewModels;

namespace ChatBotCyber
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is MainViewModel vm)
            {
                vm.SendCommand.Execute(null);
            }
        }
    }
}