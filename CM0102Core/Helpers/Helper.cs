using System;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public UInt32 dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;
    }

    public enum FlashWindow : uint
    {
        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>    
        FLASHW_STOP = 0,

        /// <summary>
        /// Flash the window caption
        /// </summary>
        FLASHW_CAPTION = 1,

        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        FLASHW_TRAY = 2,

        /// <summary>
        /// Flash both the window caption and taskbar button.
        /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        FLASHW_ALL = 3,

        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        FLASHW_TIMER = 4,

        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        FLASHW_TIMERNOFG = 12
    }
}
