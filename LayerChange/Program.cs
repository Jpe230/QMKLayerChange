using LayerChange.Layers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HidLibrary;
using System.Linq;
using LayerChange.HID;

namespace LayerChange
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            using (Tray tray = new Tray())
            {
                _ = LayerManager.GetManager;
                _ = QMKBoardManager.GetManager;

                tray.Display();

                Application.Run();
            }


        }

        //static void test()
        //{
        //    HidDevice HidDevice;

        //    var HidDeviceList = HidDevices.Enumerate(0x0032, 0x0061).ToList()
        //        .Where(hd => (ushort)hd.Capabilities.Usage == 0x61 && (ushort)hd.Capabilities.UsagePage == 0xFF60).ToList();

        //    if (HidDeviceList.Count > 0)
        //    {

        //        // Grab the first device
        //        HidDevice = HidDeviceList[0];

        //        byte[] OutData = new byte[HidDevice.Capabilities.OutputReportByteLength - 1];

        //        // Send a report to initiate an error sound
        //        OutData[0] = 0x00;
        //        OutData[1] = 0x03;
        //        OutData[2] = 10 + 2;

        //        HidDevice.Write(OutData);
        //    }
        //}
    }
}
