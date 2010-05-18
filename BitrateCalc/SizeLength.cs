using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
	public enum SizeUnit { KB, MB, GB, Kbps, Mbps };

    public struct SizeLength
    {
        long bytes;
        TimeSpan length;

        public static SizeLength Max = new SizeLength(long.MaxValue, new TimeSpan(1));
        public static SizeLength Min = new SizeLength(1, TimeSpan.MaxValue);
        public static SizeLength Zero = new SizeLength(0, TimeSpan.Zero);

        public SizeLength ToNewSize(long bytes)
        {
            return new SizeLength(bytes, length);
        }

        public SizeLength(long bytes, TimeSpan length) 
        {
            this.bytes = bytes;
            this.length = length;
        }

        public long Bytes { get { return bytes; } }
        public TimeSpan Length { get { return length; } }

        public float GB { get { return MB / 1024F; } }
        public float MB { get { return KB / 1024F; } }
        public float KB { get { return (float)bytes / 1024F; } }

        public float Mbps { get { return length == TimeSpan.Zero ? 0F : Kbps / 1000F; } }
        public float Kbps { get { return length == TimeSpan.Zero ? 0F : (float)bytes * 8F / 1000F / (float)length.TotalSeconds; } }

    }
}
