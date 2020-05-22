using System;
using System.Runtime.InteropServices;
using ConfApp.Services;
using UIKit;

namespace ConfApp.iOS.Services
{
    public class ScreenshotService : IScreenshotService
    {
        public byte[] Capture()
        {
            var capture = UIScreen.MainScreen.Capture();
            using (var data = capture.AsPNG())
            {
                var bytes = new byte[data.Length];
                Marshal.Copy(data.Bytes, bytes, 0, Convert.ToInt32(data.Length));
                return bytes;
            }
        }
    }
}