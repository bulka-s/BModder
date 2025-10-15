using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BModder.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем начальный текст при загрузке окна
            GamePathTextBox.Text = "Enter your game path...";
            GamePathTextBox.Foreground = System.Windows.Media.Brushes.Gray;
        }

        private void GamePathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Enter your game path...")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void ConfigPathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(ConfigPathTextBox.Text))
            {
                textBox.Text = "Enter your game path...";
            }
        }
        private void ConfigTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(ConfigPathTextBox.Text))
            {
                textBox.Text = "Enter your config path...";
            }
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ValidateNames = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "Select Folder";

            if (dialog.ShowDialog() == true)
            {
                string selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
                GamePathTextBox.Text = selectedPath;
            }
        }
        private void BrowseConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ValidateNames = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "Select Folder";

            if (dialog.ShowDialog() == true)
            {
                string selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
                ConfigPathTextBox.Text = selectedPath;
            }
        }

        private void CheckGameButton_Click(object sender, RoutedEventArgs e)
        {
            string path = GamePathTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(path) || path == "Enter your game path...")
            {
                Log("❌ Please enter a game path.");
                return;
            }

            if (File.Exists(System.IO.Path.Combine(path, "Lethal Company.exe")))
            {
                Log("✅ Game found!");
            }
            else
            {
                Log("⚠️ Game not found.");
            }
        }
        private void CheckConfigButton_Click(object sender, RoutedEventArgs e)
        {
            string path = GamePathTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(path) || path == "Enter your config path...")
            {
                Log("❌ Please enter a config path.");
                return;
            }

            if (File.Exists(System.IO.Path.Combine(path, "config.json")))
            {
                Log("✅ Config found!");
            }
            else
            {
                Log("⚠️ Config not found.");
            }
        }

        private void Log(string message)
        {
            LogTextBox.AppendText($"{DateTime.Now:T} | {message}\n");
            LogTextBox.ScrollToEnd();
        }
    }
}