using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace midinous_tools
{
    public class mn_json
    {

        public List<string> lines_json = new List<string>();
        public List<mn_point> lines_point = new List<mn_point>();
        public List<mn_path> lines_paths = new List<mn_path>();
        public void LoadJson(string[] lines)
        {
            Regex r = new Regex("^{\\s*\"Type\"\\s*:\\s*([1235])");
            foreach (var line in lines)
            {
                var match = r.Match(line);
                if (match.Success&&match.Groups.Count>1)
                {
                    var type = match.Groups[1];
                    if (type.Value=="5")
                    {
                        mn_path p=mn_path.Deserialize(line);
                        lines_paths.Add(p);
                    }
                    else
                    {
                        mn_point p = mn_point.Deserialize(line);
                        lines_point.Add(p);
                    }
                }
                else
                {
                    lines_json.Add(line);
                }
            }

        }
        public string SaveJson()
        {
            string save_data = "";
            foreach (var line in lines_json)
            {
                save_data += line + "\r\n";
            }
            foreach (var line in lines_point)
            {
                save_data += line.Serialize() + "\r\n";
            }
            foreach (var line in lines_paths)
            {
                save_data += line.Serialize() + "\r\n";
            }
            return save_data;
        }


        private static List<(double, double)> GeneratePolygonPoints(double sideLength, int numberOfSides, (double, double) origin)
        {
            var points = new List<(double, double)>();

            // Calculate the radius of the circumscribed circle
            double radius = sideLength / (2 * Math.Sin(Math.PI / numberOfSides));

            double startAngle = Math.PI / 2.0 - Math.PI / numberOfSides;

            double centerX = origin.Item1 + radius * Math.Cos(startAngle); ;
            double centerY = origin.Item2 - radius * Math.Sin(startAngle);



            // Calculate the starting angle

            for (int i = 0; i < numberOfSides; i++)
            {
                double angle = startAngle + i * 2 * Math.PI / numberOfSides;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                points.Add((x, y));
            }

            return points;
        }
        public void generate_poly_points(double sideLength, int numberOfSides, (double, double) origin, bool Path_cw=true, bool Path_ccw=false)
        {
            var points = GeneratePolygonPoints(sideLength,numberOfSides,origin);
            int startId = this.getMaxId()+1;
            int i = startId;
            int j = 0;

            foreach (var point in points)
            {
                mn_note note = new mn_note(i, "", (int)point.Item1, (int)point.Item2);
                int from = j == 0 ? (startId + numberOfSides - 1) : (i - 1);
                int to = j == (numberOfSides - 1) ? (startId) : (i + 1);
                //lines_paths.Add(new mn_path(from, i, false));
                if (Path_cw)
                {
                    lines_paths.Add(new mn_path(i, to, false));
                    note.SerializablePathFrom.Add(from);
                    note.SerializablePathTo.Add(to);
                }
                if (Path_ccw)
                {
                    lines_paths.Add(new mn_path(to, i, false));
                    note.SerializablePathFrom.Add(to);
                    note.SerializablePathTo.Add(from);
                }

                lines_point.Add(note);
                i++;
                j++;
            }
        }

        public void reset_Ids()
        {
            Dictionary<int,int> oldIds = new Dictionary<int,int>();
            lines_point=lines_point.OrderBy(e => e.Type).ToList();
            for (int i = 0; i < lines_point.Count; i++)
            {
                oldIds[lines_point[i].id] = i;
                lines_point[i].id = i;
            }
            for (int i = 0; i < lines_point.Count; i++)
            {
                for (int j = 0; j < lines_point[i].SerializableLogicPathFrom.Count; j++)
                {
                    lines_point[i].SerializableLogicPathFrom[j]= oldIds[lines_point[i].SerializableLogicPathFrom[j]];
                }
                for (int j = 0; j < lines_point[i].SerializableLogicPathTo.Count; j++)
                {
                    lines_point[i].SerializableLogicPathTo[j] = oldIds[lines_point[i].SerializableLogicPathTo[j]];
                }
                for (int j = 0; j < lines_point[i].SerializablePathFrom.Count; j++)
                {
                    lines_point[i].SerializablePathFrom[j] = oldIds[lines_point[i].SerializablePathFrom[j]];
                }
                for (int j = 0; j < lines_point[i].SerializablePathTo.Count; j++)
                {
                    lines_point[i].SerializablePathTo[j] = oldIds[lines_point[i].SerializablePathTo[j]];
                }
                //lines_point[i].SerializableLogicPathTo.ForEach(id => { id = oldIds[(int)id]; });
                //lines_point[i].SerializablePathFrom.ForEach(id => { id = oldIds[(int)id]; });
                //lines_point[i].SerializablePathTo.ForEach(id => { id = oldIds[(int)id]; });
            }
            foreach (var path in lines_paths)
            {
                path.source_id = oldIds[path.source_id];
                path.target_id = oldIds[path.target_id];
            }
            lines_paths=lines_paths.OrderBy(e => e.source_id).ToList();
        }

        public int getMaxId()
        {
            if(lines_point==null || lines_point.Count==0)
                return 0;
            return lines_point.OrderBy(e => e.id).Last().id;
        }
    }

    public class mn_path
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int Type { get; set; } = 5;
        public int source_id { get; set; }
        public int target_id { get; set; }
        public int weight { get; set; }
        public bool logic { get; set; }
        public int Mode { get; set; }
        //{"Type":5,"JSON":"{\"source_id\":0,\"target_id\":1,\"weight\":1,\"logic\":false,\"Mode\":0}"}
        public override string ToString()
        {
            return this.Serialize();
        }
        public mn_path(int type, string json) 
        {
            dynamic data = JsonConvert.DeserializeObject(json);
            this.Type=type;
            this.source_id=data.source_id;
            this.target_id=data.target_id;
            this.weight = data.weight;
            this.logic = data.logic;
            this.Mode = data.Mode;
        }

        public mn_path(int from, int to, bool logic,int Mode=2)
        {
            this.Type = 5;
            this.source_id = from;
            this.target_id = to;
            this.weight = 1;
            this.logic = logic;
            this.Mode = Mode;
        }

        public static mn_path Deserialize(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);
            int type = jsonObject["Type"].Value<int>();
            string json = jsonObject["JSON"].ToString();

            switch (type)
            {
                case 5:
                    return new mn_path(type, json);
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
        public string Serialize()
        {
            string json = System.Text.Json.JsonSerializer.Serialize<mn_path>(this);

            var jsonObject = new JObject
            {
                { "Type", this.Type },
                { "JSON", json }
            };
            return jsonObject.ToString(Formatting.None);
        }
    }

    public abstract class mn_point
    {

        [System.Text.Json.Serialization.JsonIgnore]
        public string JSON { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int Type { get; set; }
        public int id { get; set; }
        public string label { get; set; }
        public Color color { get; set; }
        public GroupData group_data { get; set; }
        public string Origin { get; set; }
        public int PathMode { get; set; }
        public int SignalMode { get; set; }
        public List<int> SerializablePathTo { get; set; }
        public List<int> SerializablePathFrom { get; set; }
        public List<int> SerializableLogicPathTo { get; set; }
        public List<int> SerializableLogicPathFrom { get; set; }

        public mn_point() { }

        public mn_point(int type)
        {
            this.Type = type;
        }
        public mn_point(int type, string json)
        {
            this.Type = type;
            this.JSON = json;
        }
        public override string ToString()
        {
            return this.Serialize();
        }
        public string Serialize()
        {
            string json = "";
            switch (this.Type)
            {
                case 1:
                    json = System.Text.Json.JsonSerializer.Serialize<mn_note>((mn_note)this);
                    break;
                case 2:
                    json = System.Text.Json.JsonSerializer.Serialize<mn_cc>((mn_cc)this);
                    break;
                case 3:
                    json = System.Text.Json.JsonSerializer.Serialize<mn_logic>((mn_logic)this);
                    break;
                default:
                    throw new ArgumentException("Invalid Type");
            }
            var jsonObject = new JObject
            {
                { "Type", this.Type },
                { "JSON", json }
            };
            return jsonObject.ToString(Formatting.None);
        }

        public static mn_point Deserialize(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);
            int type = jsonObject["Type"].Value<int>();
            string json = jsonObject["JSON"].ToString();

            switch (type)
            {
                case 1:
                    return new mn_note(type, json);
                case 2:
                    return new mn_cc(type, json);
                case 3:
                    return new mn_logic(type, json);
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
    }

    public class mn_note : mn_point
    {
        public bool force_scale { get; set; }
        public int probability { get; set; }
        public MidiData midi_data { get; set; }
        public RelativeMidiData relative_midi_data { get; set; }
        public bool mute { get; set; }
        public bool pass { get; set; }
        public bool start { get; set; }

        public mn_note(int id, string label, int x, int y) : base(1)
        {
            this.force_scale = false;
            this.probability = 100;
            this.midi_data = new MidiData();
            this.relative_midi_data = new RelativeMidiData();
            this.mute = false;
            this.pass = false;
            this.id = id;
            this.label = "label";
            this.color = new Color();
            this.group_data = new GroupData();
            this.start = false;
            this.Origin = "" + x + ", " + y;
            this.PathMode = 2;
            this.SignalMode = 0;
            this.SerializablePathTo = new List<int>();
            this.SerializablePathFrom = new List<int>();
            this.SerializableLogicPathTo = new List<int>();
            this.SerializableLogicPathFrom = new List<int>();
        }

        public mn_note(int type, string json) : base(type, json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);
            this.force_scale = data.force_scale;
            this.probability = data.probability;
            this.midi_data = data.midi_data.ToObject<MidiData>();
            this.relative_midi_data = data.relative_midi_data.ToObject<RelativeMidiData>();
            this.mute = data.mute;
            this.pass = data.pass;
            this.id = data.id;
            this.label = data.label;
            this.color = data.color.ToObject<Color>();
            this.group_data = data.group_data.ToObject<GroupData>();
            this.start = data.Start;
            this.Origin = data.Origin;
            this.PathMode = data.PathMode;
            this.SignalMode = data.SignalMode;
            this.SerializablePathTo = data.SerializablePathTo.ToObject<List<int>>();
            this.SerializablePathFrom = data.SerializablePathFrom.ToObject<List<int>>();
            this.SerializableLogicPathTo = data.SerializableLogicPathTo.ToObject<List<int>>();
            this.SerializableLogicPathFrom = data.SerializableLogicPathFrom.ToObject<List<int>>();
        }
    }

    public class mn_cc : mn_point
    {
        public bool SlewMode { get; set; }
        public int SlewValue { get; set; }
        public MidiData MidiData { get; set; }
        public RelativeMidiData RelativeMidiData { get; set; }
        public bool Mute { get; set; }
        public bool Pass { get; set; }
        public bool Start { get; set; }
        public mn_cc(int type, string json) : base(type, json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);
            this.SlewMode = data.slew_mode;
            this.SlewValue = data.slew_value;
            this.MidiData = data.midi_data.ToObject<MidiData>();
            this.RelativeMidiData = data.relative_midi_data.ToObject<RelativeMidiData>();
            this.Mute = data.mute;
            this.Pass = data.pass;
            this.id = data.id;
            this.label = data.label;
            this.color = data.color.ToObject<Color>();
            this.group_data = data.group_data.ToObject<GroupData>();
            this.Start = data.Start;
            this.Origin = data.Origin;
            this.PathMode = data.PathMode;
            this.SignalMode = data.SignalMode;
            this.SerializablePathTo = data.SerializablePathTo.ToObject<List<int>>();
            this.SerializablePathFrom = data.SerializablePathFrom.ToObject<List<int>>();
            this.SerializableLogicPathTo = data.SerializableLogicPathTo.ToObject<List<int>>();
            this.SerializableLogicPathFrom = data.SerializableLogicPathFrom.ToObject<List<int>>();
        }
    }

    public class mn_logic : mn_point
    {
        public int provision { get; set; }
        public bool negated { get; set; }
        public bool send_color { get; set; }
        public int Gate { get; set; }

        public mn_logic(int type, string json) : base(type, json)
        {
            dynamic data = JsonConvert.DeserializeObject(json);
            this.provision = data.provision;
            this.negated = data.negated;
            this.send_color = data.send_color;
            this.id = data.id;
            this.label = data.label;
            this.color = data.color.ToObject<Color>();
            this.group_data = data.group_data.ToObject<GroupData>();
            this.Gate = data.Gate;
            this.Origin = data.Origin;
            this.PathMode = data.PathMode;
            this.SignalMode = data.SignalMode;            
            this.SerializablePathTo = data.SerializablePathTo.ToObject<List<int>>();
            this.SerializablePathFrom = data.SerializablePathFrom.ToObject<List<int>>();
            this.SerializableLogicPathTo = data.SerializableLogicPathTo.ToObject<List<int>>();
            this.SerializableLogicPathFrom = data.SerializableLogicPathFrom.ToObject<List<int>>();
        }
    }

    public class MidiData
    {
        public int Primary { get; set; } = 60;
        public int Secondary { get; set; } = 100;
        public int Channel { get; set; } = 1;
        public int RawChannel { get; set; } = 0;
        public double Duration { get; set; } = 1.0;
        public int Repeat { get; set; } = 0;
        public int Root { get; set; } = 0;
        public int Scale { get; set; } = 0;
    }

    public class RelativeMidiData
    {
        public int Primary { get; set; }
        public int RangedPrimary { get; set; }
        public int Secondary { get; set; }
        public int Channel { get; set; }
        public double Duration { get; set; }
        public int Repeat { get; set; }
        public int Root { get; set; }
        public int Scale { get; set; }
        public RangeFields RangeFields { get; set; }
        public Pass Pass { get; set; }
    }

    public class RangeFields
    {
        public UpDown Primary { get; set; }
        public UpDown Secondary { get; set; }
        public UpDown Channel { get; set; }
        public UpDown Duration { get; set; }
        public UpDown Repeat { get; set; }
    }

    public class UpDown
    {
        public bool Up { get; set; } = false;
        public bool Down { get; set; } = false;
    }

    public class Pass
    {
        public bool Primary { get; set; } = false;
        public bool Secondary { get; set; } = false;
        public bool Channel { get; set; } = false;
        public bool Duration { get; set; } = false;
        public bool Repeat { get; set; } = false;
    }

    public class Color
    {
        public int B { get; set; } = 255;
        public int G { get; set; } = 255;
        public int R { get; set; } = 255;
        public int A { get; set; } = 255;
    }

    public class GroupData
    {
        public bool[] Groups { get; set; } = { false, false, false, false, false, false, false, false, false, false };
    }
}
