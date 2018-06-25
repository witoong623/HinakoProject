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

        protected override void OnStartup(StartupEventArgs e)
        {
            if (_mainWindow == null)
            {
                _mainWindow = new MainWindow();
                var viewModel = new MainWindowViewModel();
                _mainWindow.DataContext = viewModel;
            }

            AudioEndpointClient.Current.AudioEndpointChange += AudioEndpointChange;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AudioEndpointClient.Current.AudioEndpointChange -= AudioEndpointChange;
        }

        private void AudioEndpointChange(object sender, EventArgs e)
        {
            _mainWindow.Dispatcher.Invoke(() => _mainWindow.Show());
        }

        public void CloseWindow()
        {
            _mainWindow.Hide();
        }
    }
}
