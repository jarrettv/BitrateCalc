using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Linq;

namespace BitrateCalc
{
    public class Calculator
    {
        private static readonly decimal mp4OverheadWithBframes = 10.4M;
        private static readonly decimal mp4OverheadWithoutBframes = 4.3M;
        private static readonly decimal aviVideoOverhead = 24M;
        private static readonly decimal cbrMP3Overhead = 23.75M;
        private static readonly decimal vbrMP3Overhead = 40M;
        private static readonly decimal ac3Overhead = 23.75M;
        private static readonly int AACBlockSize = 1024;
        private static readonly int AC3BlockSize = 1536;
        private static readonly int MP3BlockSize = 1152;
        private static readonly int VorbisBlockSize = 1024;
        private static readonly int mkvAudioTrackHeaderSize = 140;
        private static readonly int mkvVorbisTrackHeaderSize = 4096;
        private static readonly uint mkvIframeOverhead = 26;
        private static readonly uint mkvPframeOverhead = 13;
        private static readonly uint mkvBframeOverhead = 16;
        private static readonly string qualityCodecModifierValues = "MPEG2=1.022,ASP=1.018,AVC=1.0,VC1=1.004";

        public Calculator(long frames, float fps) :
            this(Container.Unknown, new VideoTrack(frames, fps), new AudioTrack[0], new ExtraTrack[0]) { }

        public Calculator(Container container, VideoTrack video, AudioTrack[] audio, ExtraTrack[] extras)
        {
            Container = container;
            VideoTrack = video;
            AudioTracks = audio;
            ExtraTracks = extras;
            QualityCoeffient = 0.75F;
        }

        public Container Container { get; set; }
        public VideoTrack VideoTrack { get; set; }
        public AudioTrack[] AudioTracks { get; set; }
        public ExtraTrack[] ExtraTracks { get; set; }

        public long TotalBytes { get; set; }
        public long TotalAudioBytes { get { return AudioTracks.Sum(t => t.TotalBytes); } }
        public long TotalExtraBytes { get { return ExtraTracks.Sum(t => t.TotalBytes); } }

        public SizeLength TotalSizeLength { get { return new SizeLength(TotalBytes, VideoTrack.Duration); } }
        
        // move into VideoTrack?
        public float QualityCodecModifier { get; set; }
        public float QualityCoeffient { get; set; }
        public float QualityEstimate { get; set; }
        public float BitsPerPixel { get; set; }
                
        public void CalcByTotalSize()
        {
            CalcVideoOverhead();
            CalcAudioOverhead();
            CalcExtraOverhead();
            CalcQualityCodecModifier();
            VideoTrack.RawBytes = (long)((float)TotalBytes / VideoTrack.OverheadRatio) - TotalAudioBytes - TotalExtraBytes - VideoTrack.OverheadBytes;
            CalcBitsPerPixel();
            CalcQualityEstimate();
        }

        public void CalcByVideoSize()
        {
            CalcVideoOverhead();
            CalcAudioOverhead();
            CalcExtraOverhead();
            CalcQualityCodecModifier();
            TotalBytes = VideoTrack.TotalBytes + TotalAudioBytes + TotalExtraBytes;
            CalcBitsPerPixel();
            CalcQualityEstimate();
        }

        public void CalcByBitsPerPixel()
        {
            CalcVideoOverhead();
            CalcAudioOverhead();
            CalcExtraOverhead();
            CalcQualityCodecModifier();
            VideoTrack.RawBytes = (long)(BitsPerPixel / 8F * (float)(VideoTrack.FrameSize.Width * VideoTrack.FrameSize.Height) * 
                (float)VideoTrack.Frames / VideoTrack.OverheadRatio) - VideoTrack.OverheadBytes;
            TotalBytes = VideoTrack.TotalBytes + TotalAudioBytes + TotalExtraBytes;
            CalcQualityEstimate();
        }

        public void CalcByQualityEstimate()
        {
            CalcVideoOverhead();
            CalcAudioOverhead();
            CalcExtraOverhead();
            CalcQualityCodecModifier();
            var bitrate = (float)QualityEstimate * ((float)Math.Pow((float)(VideoTrack.FrameSize.Width * VideoTrack.FrameSize.Height), 
                QualityCoeffient * QualityCodecModifier) * VideoTrack.FrameRate) / 1000F;
            VideoTrack.RawBytes = (long)(bitrate / 8F * 1000F * (float)VideoTrack.Duration.TotalSeconds) - VideoTrack.OverheadBytes;
            TotalBytes = VideoTrack.TotalBytes + TotalAudioBytes + TotalExtraBytes;
            CalcBitsPerPixel();
        }

        private void CalcBitsPerPixel()
        {
            BitsPerPixel = (float)VideoTrack.RawBytes * 8F / (float)VideoTrack.Frames / (float)(VideoTrack.FrameSize.Width * VideoTrack.FrameSize.Height);
        }

        private void CalcQualityEstimate()
        {
            QualityEstimate = (float)VideoTrack.SizeLength.Kbps / ((float)Math.Pow((float)(VideoTrack.FrameSize.Width * VideoTrack.FrameSize.Height),
                QualityCoeffient * QualityCodecModifier) * VideoTrack.FrameRate) * 1000F;
        }

        private void CalcQualityCodecModifier()
        {
            var qualityCodecModifiers = new Dictionary<string, float>();

            // read the values into the dictionary
            foreach (string mod in qualityCodecModifierValues.Split(','))
            {
                qualityCodecModifiers.Add(mod.Split('=')[0], float.Parse(mod.Split('=')[1]));
            }

            // use values when found in dictionary, otherwise default to no-modification
            if (qualityCodecModifiers.ContainsKey(VideoTrack.VideoCodec.ToString()))
                QualityCodecModifier = qualityCodecModifiers[VideoTrack.VideoCodec.ToString()];
            else
                QualityCodecModifier = 1F;
        }

        private void CalcVideoOverhead()
        {
            if (Container == Container.Mp4)
            {
                var overhead = VideoTrack.HasBframes ? mp4OverheadWithBframes : mp4OverheadWithoutBframes;
                VideoTrack.OverheadBytes = (long)(overhead * (decimal)VideoTrack.Frames);
            }
            else if (Container == Container.Mkv)
            {
                long nbIframes = VideoTrack.Frames / 10;
                long nbBframes = VideoTrack.HasBframes ? (VideoTrack.Frames - nbIframes) / 2 : 0;
                long nbPframes = VideoTrack.Frames - nbIframes - nbBframes;
                VideoTrack.OverheadBytes = (long)
                    (4300D + 1400D + nbIframes * mkvIframeOverhead + nbPframes * mkvPframeOverhead +
                    nbBframes * mkvBframeOverhead + 
                    VideoTrack.Duration.TotalSeconds * 12 / 2); // 12 bytes per cluster overhoad
            }
            else if (Container == Container.Avi)
            {
                VideoTrack.OverheadBytes = (long)(VideoTrack.Frames * aviVideoOverhead);
            }
            else if (Container == Container.M2ts)
            {
                // for m2ts, video overhead is a ratio (rather than constant)
                VideoTrack.OverheadRatio = 106F / 100F;
            }
        }

        private void CalcAudioOverhead()
        {
            foreach (var audio in AudioTracks)
            {
                if (Container == Container.Mkv)
                {
                    audio.OverheadBytes = GetMkvAudioOverhead(audio.AudioCodec, 48000, audio.Duration.TotalSeconds);
                }
                else if (Container == Container.M2ts)
                {
                    audio.OverheadBytes = GetM2tsAudioOverhead(audio.AudioCodec, 48000, audio.Duration.TotalSeconds);
                    audio.OverheadRatio = 106F / 100F;
                }
                else if (Container == Container.Avi)
                {
                    audio.OverheadBytes = (long)(GetAviAudioOverhead(audio.AudioCodec) * VideoTrack.Frames);
                }
            }
        }

        private void CalcExtraOverhead()
        {
            foreach (var extra in ExtraTracks)
            {
                if (Container == Container.M2ts) extra.OverheadRatio = 2;
            }
        }

        /// <summary>
        /// gets the overhead a given audio type will incurr in the matroska container
        /// given its length and sampling rate
        /// </summary>
        /// <param name="AudioType">type of the audio track</param>
        /// <param name="samplingRate">sampling rate of the audio track</param>
        /// <param name="length">length of the audio track</param>
        /// <returns>overhead this audio track will incurr</returns>
        private static int GetMkvAudioOverhead(AudioCodec audioType, int samplingRate, double length)
        {
            Int64 nbSamples = Convert.ToInt64((double)samplingRate * length);
            int headerSize = mkvAudioTrackHeaderSize;
            int samplesPerBlock = 0;
            if (audioType == AudioCodec.AacVbr || audioType == AudioCodec.AacCbr)
                samplesPerBlock = AACBlockSize;
            else if (audioType == AudioCodec.Mp3Cbr || audioType == AudioCodec.Mp3Vbr || audioType == AudioCodec.Dts)
                samplesPerBlock = MP3BlockSize;
            else if (audioType == AudioCodec.Ac3)
                samplesPerBlock = AC3BlockSize;
            else if (audioType == AudioCodec.OggVorbis)
            {
                samplesPerBlock = VorbisBlockSize;
                headerSize = mkvVorbisTrackHeaderSize;
            }
            else // unknown types..
            {
                samplesPerBlock = AC3BlockSize;
            }
            double blockOverhead = (double)nbSamples / (double)samplesPerBlock * 22.0 / 8.0;
            int overhead = (int)(headerSize + 5 * length + blockOverhead);
            return overhead;
        }

        /// <summary>
        /// gets the overhead a given audio type will incurr in the m2ts container
        /// given its length and sampling rate
        /// </summary>
        /// <param name="AudioType">type of the audio track</param>
        /// <param name="samplingRate">sampling rate of the audio track</param>
        /// <param name="length">length of the audio track</param>
        /// <returns>overhead this audio track will incurr</returns>
        private static int GetM2tsAudioOverhead(AudioCodec audioType, int samplingRate, double length)
        {
            // TODO: ??
            return 0;
        }

        /// <summary>
        /// gets the avi container overhead for the given audio type and bitrate mode
        /// bitrate mode only needs to be taken into account for MP3 but it's there for all cases nontheless
        /// </summary>
        /// <param name="AudioType">the type of audio</param>
        /// <param name="bitrateMode">the bitrate mode of the given audio type</param>
        /// <returns>the overhead in bytes per frame</returns>
        private static decimal GetAviAudioOverhead(AudioCodec audioType)
        {
            if (audioType == AudioCodec.Ac3)
                return ac3Overhead;
            else if (audioType == AudioCodec.Mp3Vbr)
                return vbrMP3Overhead;
            else if (audioType == AudioCodec.Mp3Cbr)
                return cbrMP3Overhead;
            else if (audioType == AudioCodec.AacVbr)
                return vbrMP3Overhead;
            else if (audioType == AudioCodec.AacCbr)
                return cbrMP3Overhead;
            else if (audioType == AudioCodec.Dts)
                return ac3Overhead;
            else
                return 0;
        }
    }
}
