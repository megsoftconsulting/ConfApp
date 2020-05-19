using System;
using System.Diagnostics;
using CoreLocation;
using HealthKit;

namespace ConfApp.iOS.Services
{
    public class PinedaCLRegion : CLCircularRegion
    {
        private readonly CLLocationCoordinate2D _p1;
        private readonly CLLocationCoordinate2D _p2;

        private static CLLocationCoordinate2D CalculateCenter(CLLocationCoordinate2D p1, CLLocationCoordinate2D p2)
        {
            var lat = (p1.Latitude + p2.Latitude) / 2;
            var lon = (p1.Longitude + p2.Longitude) / 2;
            var r = new CLLocationCoordinate2D(lat, lon);

            return r;
        }
        public PinedaCLRegion(CLLocationCoordinate2D p1, 
            CLLocationCoordinate2D p2,
            string id)
            :base(CalculateCenter(p1, p2), 5, id)
        {
            _p1 = p1;
            _p2 = p2;
        }

       
        public override bool ContainsCoordinate(CLLocationCoordinate2D coordinate)
        {
            OutputStringToConsole("CLCircularRegion.ContainsCoordinate called");
            return containsImpl(coordinate);
        }

        //public override bool Contains(CLLocationCoordinate2D coordinate)
        //{
        //    Debug.WriteLine("CLCircularRegion.Contains called");
        //    return containsImpl(coordinate);
        //}

        private bool containsImpl(CLLocationCoordinate2D p)
        {
            return isBounded(_p1.Latitude, _p1.Longitude, _p2.Latitude, _p2.Longitude, p.Latitude, p.Longitude);
        }

        bool isBounded(double top, double left,
            double bottom, double right,
            double latitude, double longitude)
        {
            /* Check latitude bounds first. */
            if (top >= latitude && latitude >= bottom)
            {
                /* If your bounding box doesn't wrap 
                   the date line the value
                   must be between the bounds.
                   If your bounding box does wrap the 
                   date line it only needs to be  
                   higher than the left bound or 
                   lower than the right bound. */
                if (left <= right && left <= longitude && longitude <= right)
                {
                    return true;
                }
                else if (left > right && (left <= longitude || longitude <= right))
                {
                    return true;
                }
            }
            return false;
        }

        protected PinedaCLRegion(IntPtr handle) : base(handle)
        {
        }

        const string FoundationLibrary = "/System/Library/Frameworks/Foundation.framework/Foundation";

        [System.Runtime.InteropServices.DllImport(FoundationLibrary)]
        extern static void NSLog(IntPtr format, IntPtr s);

        [System.Runtime.InteropServices.DllImport(FoundationLibrary, EntryPoint = "NSLog")]
        extern static void NSLog_ARM64(IntPtr format, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr s);

        static readonly bool Is64Bit = IntPtr.Size == 8;
        static readonly bool IsDevice = ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.DEVICE;

        static readonly Foundation.NSString nsFormat = new Foundation.NSString(@"%@");

        static void OutputStringToConsole(string text)
        {
            using (var nsText = new Foundation.NSString(text))
            {
                if (IsDevice && Is64Bit)
                {
                    NSLog_ARM64(nsFormat.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, nsText.Handle);
                }
                else
                {
                    NSLog(nsFormat.Handle, nsText.Handle);
                }
            }
        }

    }
}