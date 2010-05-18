using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public enum AudioCodec : byte
    {
        Unknown, Mp3Cbr, Mp3Vbr, AacCbr, AacVbr, Vorbis, Dts, Ac3, Mp2, Wav, Pcm, Eac3, TrueHd, DtsMa
    }
}
