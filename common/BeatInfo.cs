using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class BeatInfo: ICloneable
    {
        public double Time { get; set; }
        [Obsolete]
        public double Position { get; set; }
        public string Tag { get; set; }
        public Chord ChordTag { get; set; }
        public double[] ChromaD { get; set; }
        public bool BarStart { get; set; }
        public Tonalty Tonalty { get; set; }
        public BeatInfo()
        {
            Tonalty = Tonalty.NoTonalty;
            ChordTag = Chord.NonChord;
        }
        public override string ToString()
        {
            return Time.ToString() + '\t' + Tag.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
