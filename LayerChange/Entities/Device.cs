using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerChange.Entities
{
    public class Device
    {
        public string ProductId { get; set; }
        public string VendorId { get; set; }
        public string UsagePage { get; set; }
        public string Usage { get; set; }
    }
}
