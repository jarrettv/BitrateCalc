using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public class AudioTrack : Track
    {
        public AudioTrack(string file) : base()
        {
            Load(file);
        }

        public AudioTrack(TimeSpan duration) : base()
        {
            this.Duration = duration;
        }

        public AudioCodec AudioCodec { get; set; }
        public int SamplingRate { get; set; }

        protected void Load(string file)
        {
            //TODO:
        }
    }
}
