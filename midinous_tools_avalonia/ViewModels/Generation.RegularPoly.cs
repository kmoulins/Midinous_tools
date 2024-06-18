using Microsoft.CodeAnalysis.Diagnostics;
using midinous_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midinous_tools_avalonia.ViewModels
{
    internal class RegularPoly : Generation
    {
        public int numSides { get; set; }
        public bool GenCW { get; set; }
        public bool GenCCW { get; set; }
        public override mn_json Generate()
        {
            var tmp=new mn_json();
            tmp.generate_poly_points(this.scale,this.numSides,(x,y),type,GenCW,GenCCW);
            return tmp;
        }
    }
}

