using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MetroTrilithon.Serialization;

namespace HinakoProject.Models
{
    public class AudioVolumeSetting
    {
        public static SerializableProperty<float> SpeakerVolume { get; }
            = new SerializableProperty<float>(GetKey(), SettingsProviders.Local, 0.35f);

        public static SerializableProperty<float> HeadphoneVolume { get; }
            = new SerializableProperty<float>(GetKey(), SettingsProviders.Local, 0.04f);

        private static string GetKey([CallerMemberName] string caller = "")
        {
            return nameof(AudioVolumeSetting) + "." + caller;
        }
    }
}
