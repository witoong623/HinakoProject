using HinakoProject.AudioControllers;
using HinakoProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HinakoProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private float _currentVolume;
        public string[] _availableEndpoints = new string[] { "Speaker", "Headphone" };

        public event PropertyChangedEventHandler PropertyChanged;

        public string[] AvailableEndpoints
        {
            get { return _availableEndpoints; }
        }

        public string SelectedEndpoint { get; set; }

        public MainWindowViewModel()
        {
            _currentVolume = AudioEndpointClient.Current.Volume;
        }

        public void ActionOK()
        {
            if (SelectedEndpoint == null)
            {
                return;
            }
            
            // for now, leave speaker sound as it is
            if (SelectedEndpoint == "Speaker")
            {
                AudioVolumeSetting.HeadphoneVolume.Value = AudioEndpointClient.Current.Volume;
                AudioEndpointClient.Current.Volume = AudioVolumeSetting.SpeakerVolume.Value;

                ((App)Application.Current).HideWindow();
            }
            else if (SelectedEndpoint == "Headphone")
            {
                AudioVolumeSetting.SpeakerVolume.Value = AudioEndpointClient.Current.Volume;
                AudioEndpointClient.Current.Volume = AudioVolumeSetting.HeadphoneVolume.Value;

                ((App)Application.Current).HideWindow();
            }
        }

        public void ActionCancel()
        {
            ((App)Application.Current).HideWindow();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
