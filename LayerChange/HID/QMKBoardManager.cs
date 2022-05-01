using HidLibrary;
using LayerChange.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LayerChange.HID
{
    public class QMKBoardManager
    {
        private static QMKBoardManager instance;

        public static QMKBoardManager GetManager
        {
            get
            {
                if (instance == null)
                    instance = new QMKBoardManager();
                return instance;
            }
        }
        private HidDevice HidDevice;

        private ushort VendorId;
        private ushort ProductId;
        private ushort UsagePage;
        private ushort Usage;

        public QMKBoardManager()
        {
            LoadDevice();
            EnumarateDevice();
        }

        public void LoadDevice()
        {
            using (StreamReader streamReader = new StreamReader("Device.json"))
            {
                string jsonString = streamReader.ReadToEnd();
                Device dev = JsonSerializer.Deserialize<Device>(jsonString);

                VendorId = (ushort)uint.Parse(dev.VendorId, System.Globalization.NumberStyles.AllowHexSpecifier);
                ProductId = (ushort)uint.Parse(dev.ProductId, System.Globalization.NumberStyles.AllowHexSpecifier);
                UsagePage = (ushort)uint.Parse(dev.UsagePage, System.Globalization.NumberStyles.AllowHexSpecifier);
                Usage = (ushort)uint.Parse(dev.Usage, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
        }

        public void EnumarateDevice()
        {
            int v = 0x0032;
            int p = 0x0061;
            var tempList = HidDevices.Enumerate(VendorId, ProductId).ToList();
            HidDevice = HidDevices.Enumerate(VendorId, ProductId).ToList()
                .Where(hd => (ushort)hd.Capabilities.Usage == Usage && (ushort)hd.Capabilities.UsagePage == UsagePage).FirstOrDefault();
        }

        public void ChangeLayer(int layer)
        {
            if(HidDevice != null)
            {
                byte[] OutData = new byte[HidDevice.Capabilities.OutputReportByteLength - 1];
                OutData[0] = 0x00;
                OutData[1] = 0x03;
                OutData[2] = (byte)(0x0A + layer);

                HidDevice.Write(OutData);
            }
        }
    }
}
