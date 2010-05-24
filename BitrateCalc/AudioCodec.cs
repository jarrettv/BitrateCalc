using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitrateCalc
{
    public enum AudioCodec : byte
    {
        Unknown, Mp3Cbr, Mp3Vbr, AacCbr, AacVbr, OggVorbis, Dts, Ac3, Mp2, Wav, Pcm, Eac3, TrueHd, DtsMa
    }
    public static class AudioCodecs
    {
        public static readonly List<AudioCodec> List = Enum.GetValues(typeof(AudioCodec)).Cast<AudioCodec>().ToList();
    }
}
