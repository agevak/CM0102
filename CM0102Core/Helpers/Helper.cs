using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CM.Helpers
{
    public static class Helper
    {
        public static string Serialize<T>(T obj, Formatting formatting = Formatting.None)
        {
            string result = JsonConvert.SerializeObject(obj, formatting, new StringEnumConverter());
            return result;
        }

        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T DeepCopy<T>(T obj)
        {
            return Deserialize<T>(Serialize(obj));
        }

        public static int? GetConfig(string name)
        {
            //if (ConfigurationSettings.AppSettings.)
            return null;
        }

        [DllImport("user32.dll")]
        public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);
    }
}
