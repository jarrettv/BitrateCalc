using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public class ExtraTrack : Track
    {
        public ExtraTrack(TimeSpan duration)
            : base()
        {
            this.Duration = duration;
        }

        public ExtraTrack(string file) : base()
        {
            // TODO:
        }
    }
}
