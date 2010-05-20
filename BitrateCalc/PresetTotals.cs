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
            new PresetSize("1 CD   (700MB)", 700 * 1024 * 1024),
            new PresetSize("2 CDs (1400MB)", 1400 * 1024 * 1024),
            new PresetSize("3 CDs (2100MB)", 2100L * 1024L * 1024L),
            new PresetSize("1/3 DVD (1493MB)", 1493 * 1024 * 1024),
            new PresetSize("1/4 DVD (1120MB)", 1120 * 1024 * 1024),
            new PresetSize("1/5 DVD (896MB)", 896 * 1024 * 1024),
            new PresetSize("1 DVD (4482MB)", 4482L * 1024L * 1024L),
            new PresetSize("1 DVD-9 (8152MB)", 8152L * 1024L * 1024L),
            new PresetSize("1 BD-5 (4482MB)", 4482L * 1024L * 1024L),
            new PresetSize("1 BD-9 (8152MB)", 8152L * 1024L * 1024L),
            new PresetSize("1 BD (23450MB)", 23450L * 1024L * 1024L)
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
