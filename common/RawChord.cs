using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class RawChord
    {
        public double Time;
        public string Chord;
        public double Length;
        public RawChord(double time,string chord)
        {
            Time = time;
            Chord = chord;
        }
    }
}
