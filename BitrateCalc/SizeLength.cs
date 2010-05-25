using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BitrateCalc
{
	public enum SizeUnit { KB = 1, MB = 2, GB = 3, Kbps = 100, Mbps = 102 };

    public struct SizeLength
    {
        long bytes;
        TimeSpan length;

        public static SizeLength Max = new SizeLength(long.MaxValue, new TimeSpan(1));
        public static SizeLength Min = new SizeLength(1, TimeSpan.MaxValue);
        public static SizeLength Zero = new SizeLength(0, TimeSpan.Zero);

        public SizeLength ToNewSize(long bytes)
        {
            return new SizeLength(bytes, this.length);
        }

        public SizeLength ToNewLength(TimeSpan length, bool keepSize)
        {
            if (length.Ticks < 0) throw new ArgumentException("The length must be greater than zero.", "length");

            var bitrate = Kbps;
            var ratio = (decimal)this.length.Ticks / (decimal)length.Ticks;
            if (ratio == 0 || keepSize) ratio = 1;
            var result = new SizeLength((long)((decimal)this.bytes / ratio), length);
            //Trace.Assert(Math.Abs(result.Kbps - bitrate) < 0.01 || keepSize);
            return result;
        }

        public SizeLength(long bytes, TimeSpan length) 
        {
            if (bytes < 0) throw new ArgumentException("The byte size can't be negative", "bytes");
            if (length.Ticks < 0) throw new ArgumentException("The length can't be negative", "length");
            this.bytes = bytes;
            this.length = length;
        }

        public long Bytes { get { return bytes; } }
        public TimeSpan Length { get { return length; } }

        public bool UnknownLength { get { return length.Ticks <= 0; } }
        public bool UnknownSize { get { return bytes <= 0; } }

        public float GB { get { return MB / 1024F; } }
        public float MB { get { return KB / 1024F; } }
        public float KB { get { return (float)bytes / 1024F; } }

        public float Mbps { get { return UnknownLength ? 0F : Kbps / 1000F; } }
        public float Kbps { get { return UnknownLength ? 0F : (float)bytes * 8F / 1000F / (float)length.TotalSeconds; } }

    }
}
