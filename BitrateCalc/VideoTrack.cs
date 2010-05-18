using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BitrateCalc
{
    public class VideoTrack : Track
    {
        public VideoTrack(long frames, float fps) : base()
        {
            if (fps <= 0) throw new ArgumentException("Frames per second must be greater than zero.", "fps");
            if (frames <= 0) throw new ArgumentException("Frames must be greater than zero.", "frames");

            this.Frames = frames;
            this.FrameRate = fps;
        }

        public VideoCodec VideoCodec { get; set; }
        public bool HasBframes { get; set; }
        public long Frames { get; set; }
        public float FrameRate { get; set; }
        public override TimeSpan Duration { get { return new TimeSpan((long)((float)Frames / FrameRate) * TimeSpan.TicksPerSecond); } }
        
        public Size FrameSize { get; set; }
    }
}
