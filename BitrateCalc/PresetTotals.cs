using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public class PresetSize
    {
        public static PresetSize[] Presets = new PresetSize[]
        {            
            new PresetSize("1/4 CD (175MB)", 175 * 1024 * 1024),
            new PresetSize("1/2 CD (350MB)", 350 * 1024 * 1024),
            new PresetSize("CD (700MB)", 700 * 1024 * 1024),
            new PresetSize("2 CDs (1400MB)", 1400 * 1024 * 1024),
            new PresetSize("3 CDs (2100MB)", 2100L * 1024L * 1024L),
            new PresetSize("1/5 DVD (896MB)", 896 * 1024 * 1024),
            new PresetSize("1/4 DVD (1120MB)", 1120 * 1024 * 1024),
            new PresetSize("1/3 DVD (1492MB)", 1492 * 1024 * 1024),
            new PresetSize("1/2 DVD (2240MB)", 2240L * 1024L * 1024L),
            new PresetSize("DVD (4480MB)", 4480L * 1024L * 1024L),
            new PresetSize("1½ DVD (6720MB)", 6720L * 1024L * 1024L),
            new PresetSize("DVD-DL (8145MB)", 8145L * 1024L * 1024L),
            new PresetSize("BD (23450MB)", 23450L * 1024L * 1024L), // actual is 25025314816 bytes
            new PresetSize("BD-DL (46900MB)", 46900L * 1024L * 1024L) // actual is 7791181824 bytes
        };

        public PresetSize(string preset, long size)
        {
            Preset = preset;
            Size = size;
        }

        public string Preset { get; set; }
        public long Size { get; set; }
    }
}
