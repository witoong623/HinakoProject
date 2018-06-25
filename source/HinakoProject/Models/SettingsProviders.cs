using MetroTrilithon.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HinakoProject.Models
{
    public class SettingsProviders
    {
        public static string LocalFilePath { get; }
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "Settings.xaml");

        public static ISerializationProvider Local { get; } = new FileSettingsProvider(LocalFilePath);
    }
}
