using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public abstract class Track
    {
        public Track()
        {
            OverheadRatio = 1;
        }

        public virtual TimeSpan Duration { get; protected set; }

        public virtual long RawBytes { get; set; }
        public virtual long OverheadBytes { get; set; }
        public virtual float OverheadRatio { get; set; }

        public virtual long TotalBytes { get { return (long)((float)RawBytes * OverheadRatio) + OverheadBytes; } }

        public virtual SizeLength SizeLength { get { return new SizeLength(RawBytes, Duration); } }
    }
}
