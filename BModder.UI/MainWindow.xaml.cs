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

        private void GamePathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter your game path...";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
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
                GamePathTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void CheckGameButton_Click(object sender, RoutedEventArgs e)
        {
            string path = GamePathTextBox.Text.Trim();

            // Проверяем, не является ли текст placeholder'ом
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

        private void CheckModsButton_Click(object sender, RoutedEventArgs e)
        {
            string path = GamePathTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(path) || path == "Enter your game path...")
            {
                Log("❌ Please enter a game path first.");
                return;
            }

            string modsFile = "mods.json";
            if (!File.Exists(modsFile))
            {
                Log("❌ mods.json not found!");
                return;
            }

            try
            {
                var mods = ModManager.LoadMods(modsFile);
                Log($"Found {mods.Count} mods:");

                foreach (var mod in mods)
                {
                    bool installed = mod.IsInstalled(path);
                    string symbol = installed ? "✅" : "⚠️";
                    Log($"{symbol} {mod.Name}");
                }
            }
            catch (Exception ex)
            {
                Log($"❌ Error loading mods: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            LogTextBox.AppendText($"{DateTime.Now:T} | {message}\n");
            LogTextBox.ScrollToEnd();
        }
    }
}