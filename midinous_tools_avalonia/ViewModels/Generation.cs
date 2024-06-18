using midinous_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midinous_tools_avalonia.ViewModels
{
    internal class Generation
    {
        public double x { get; set; }
        public double y { get; set; }
        public double scale { get; set; }
        public int type { get; set; }
        public virtual mn_json Generate() {  return new mn_json(); }
    }
}
