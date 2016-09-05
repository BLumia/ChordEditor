using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class TimingPoint
    {
        public double Offset { get; set; }
        public double BeatLength { get; set; }
        public int TimeSignature { get; set; }
        public int SampleSet { get; set; }
        public int CustomSamples { get; set; }
        public int Volumn { get; set; }
        public bool TimingChange { get; set; }
        public int EffectFlag { get; set; }
        public double BPM { get { return BeatLength > 0 ? 60000 / BeatLength : 0; } }
        public double Position { get { return Offset / 1000.0; } }

    }
}
