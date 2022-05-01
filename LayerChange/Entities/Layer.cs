using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerChange.Entities
{
    public class Layer
    {
        public string Name { get; set; }
        public List<string> Apps { get; set; }
        public List<string> Ignore { get; set; }
        public int LayerId { get; set; }
    }
    public class LayerWrapper
    {
        public List<Layer> Layers { get; set; }
    }
}
