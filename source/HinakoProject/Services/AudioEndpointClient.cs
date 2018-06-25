using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HinakoProject.AudioControllers
{
    public enum EndpointType { Speaker, Headphone }

    public class AudioEndpointClient : IMMNotificationClient
    {
        private const int WAITTIME = 1000;

        public event EventHandler AudioEndpointChange;

        private int _raiseCount;
        private object _mutex = new object();
        private DateTime _startTime;
        private MMDeviceEnumerator _enumerator;
        private MMDevice _defaultDeviceEndpoint;

        public static AudioEndpointClient Current { get; } = new AudioEndpointClient();

        public AudioEndpointClient()
        {
            _startTime = new DateTime();
            _enumerator = new MMDeviceEnumerator();
            _enumerator.RegisterEndpointNotificationCallback(this);

            _defaultDeviceEndpoint = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key)
        {
            lock (_mutex)
            {
                Console.WriteLine("OnPropertyValueChanged was called in AudioEndpointClient");
                if (_raiseCount >= 10)
                {
                    OnAudioEndpointChange();
                    _raiseCount = 0;
                    return;
                }

                TimeSpan diff = _startTime.Subtract(DateTime.Now);
                if (diff.Milliseconds <= 1500)
                {
                    _raiseCount++;
                }
                else
                {
                    _raiseCount = 0;
                    _startTime = DateTime.Now;
                }
            }
        }

        public float Volume
        {
            get { return _defaultDeviceEndpoint.AudioEndpointVolume.MasterVolumeLevelScalar; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be between 0 and 1");
                }

                _defaultDeviceEndpoint.AudioEndpointVolume.MasterVolumeLevelScalar = value;
            }
        }

        private void OnAudioEndpointChange()
        {
            AudioEndpointChange?.Invoke(this, EventArgs.Empty);
        }

        #region not implement method
        void IMMNotificationClient.OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            throw new NotImplementedException();
        }

        void IMMNotificationClient.OnDeviceAdded(string pwstrDeviceId)
        {
            throw new NotImplementedException();
        }

        void IMMNotificationClient.OnDeviceRemoved(string deviceId)
        {
            throw new NotImplementedException();
        }

        void IMMNotificationClient.OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
