using HinakoProject.AudioControllers;
using HinakoProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HinakoProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private System.Windows.Forms.NotifyIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Construct asking window and bind to viewmodel
            if (_mainWindow == null)
            {
                _mainWindow = new MainWindow();
                var viewModel = new MainWindowViewModel();
                _mainWindow.DataContext = viewModel;
            }

            AudioEndpointClient.Current.AudioEndpointChange += AudioEndpointChange;
            ShowNotifyIcon();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AudioEndpointClient.Current.AudioEndpointChange -= AudioEndpointChange;
        }

        // Event listener that shows asking window every time endpoint change
        private void AudioEndpointChange(object sender, EventArgs e)
        {
            _mainWindow.Dispatcher.Invoke(() => _mainWindow.Show());
        }

        private void ShowNotifyIcon()
        {
            const string iconUri = "pack://application:,,,/HinakoProject;Component/Assets/tasktray.ico";

            Uri uri;
            if (!Uri.TryCreate(iconUri, UriKind.Absolute, out uri)) return;

            var streamResource = GetResourceStream(uri);
            if (streamResource == null) return;

            using (var stream = streamResource.Stream)
            {
                notifyIcon = new System.Windows.Forms.NotifyIcon
                {
                    Text = "HinakoProject",
                    Icon = new System.Drawing.Icon(stream, new System.Drawing.Size(16, 16)),
                    Visible = true,
                    ContextMenu = new System.Windows.Forms.ContextMenu(new[]
                    {
                        new System.Windows.Forms.MenuItem("S&etting", (sender, e) => this.ShowAskingWindow()),
                        new System.Windows.Forms.MenuItem("E&xit (X)", (sender, args) => this.Shutdown())
                    })
                };
            }
        }

        /// <summary>
        /// Show asking window to ask whether user plug in or unplug
        /// </summary>
        public void ShowAskingWindow()
        {
            Debug.WriteLine("ShowAskingWindow was called");
            _mainWindow.Dispatcher.Invoke(() => _mainWindow.Show());
        }

        /// <summary>
        /// Hide asking window
        /// </summary>
        public void HideWindow()
        {
            _mainWindow.Hide();
        }
    }
}
