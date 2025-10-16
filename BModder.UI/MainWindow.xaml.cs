using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BModder.UI
{
    public partial class MainWindow : Window
    {
        private InstallationProcess InstallationProcess;

        public MainWindow()
        {
            InitializeComponent();

            LogHelper.Init(LogBox);

            InstallationProcess = new InstallationProcess();

            InstallationProcess.ProgressChanged += OnInstallProgressChanged;
        }
        private void OnInstallProgressChanged(int percent, string message)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBarInstall.Value = percent;
            });
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

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            InstallationProcess.ConfigPath = ConfigPathTextBox.Text.Trim();
            InstallationProcess.GamePath = GamePathTextBox.Text.Trim();
            InstallationProcess.IsCleanInstall = isCleanInstall.IsChecked ?? false;

            InstallationProcess.Start();

            LogHelper.WriteLog("End...", LogHelper.LogType.Info);
        }
    }
}