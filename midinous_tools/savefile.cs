using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace midinous_tools
{
    internal class savefile
    {
        public JsonDocument json;
        public string filename;
        public savefile(string file) {            
            json=JsonDocument.Parse(File.ReadAllText(file));
        }
    }
}
