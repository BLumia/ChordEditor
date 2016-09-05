using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class SongInfo
    {
        public List<TimingPoint> timingPoints = new List<TimingPoint>();
        public List<BeatInfo> beats;
        public List<RawChord> rawChords;
        public Chroma chroma;
        public double MaxBeatChroma, MaxGlobalChroma;
        public double[] GlobalChroma;
        public int MetreNumber { get; set; }
        public string FileName { get; set; }
        public double MP3Length { get; set; }
        //int tonalty = -1;
        /*public int Tonalty
        {
            get { return tonalty; }
            set
            {
                tonalty = value;
                Triggers.ChordLabelChangeTrigger = true;
            }
        }*/
        public void CalcBeatChord()
        {
            if (beats.Count == 0) return;
            int p = 0;
            while (p < rawChords.Count && rawChords[p].Time + rawChords[p].Length < beats[0].Time)
            {
                ++p;
            }
            for (int i = 0; i < beats.Count; ++i)
            {
                double beatStart = beats[i].Time;
                double beatEnd = i == beats.Count - 1 ? MP3Length : beats[i + 1].Time;
                Dictionary<string, double> chordLen = new Dictionary<string, double>();

                while (p < rawChords.Count && rawChords[p].Time + rawChords[p].Length < beatEnd)
                {
                    if (!chordLen.ContainsKey(rawChords[p].Chord))
                        chordLen[rawChords[p].Chord] = 0;
                    chordLen[rawChords[p].Chord] += Math.Min(rawChords[p].Length, rawChords[p].Time + rawChords[p].Length - beatStart);
                    ++p;
                }
                if(p< rawChords.Count)
                {
                    if (!chordLen.ContainsKey(rawChords[p].Chord))
                        chordLen[rawChords[p].Chord] = 0;
                    chordLen[rawChords[p].Chord] += beatEnd - Math.Max(beatStart, rawChords[p].Time);
                }
                string maxAtStr = "N";
                double maxValue = 0;
                foreach (KeyValuePair<string,double> kv in chordLen)
                {
                    if(kv.Value>maxValue)
                    {
                        maxValue = kv.Value;
                        maxAtStr = kv.Key;
                    }
                }
                beats[i].ChordTag = new Chord(maxAtStr);
            }

        }
        public void CalcBeatChroma()
        {
            if (beats.Count == 0) return;
            MaxBeatChroma = 0;
            int p = 0;
            while (p < chroma.Frames.Length && chroma.Frames[p].Time < beats[0].Time)
            {
                ++p;
            }
            for (int i=0;i<beats.Count-1;++i)
            {
                beats[i].ChromaD = new double[12];
                while(p<chroma.Frames.Length&&chroma.Frames[p].Time<beats[i+1].Time)
                {
                    for(int j=0;j<12;++j)
                    {
                        beats[i].ChromaD[j] += chroma.Frames[p].D[j];
                    }
                    ++p;
                }
                for (int j = 0; j < 12; ++j)
                {
                    MaxBeatChroma = Math.Max(MaxBeatChroma, beats[i].ChromaD[j]);
                }
            }
        }
        public void CalcGlobalChroma()
        {
            GlobalChroma = new double[12];
            MaxGlobalChroma = 0;
            int p = 0;
            while (p < chroma.Frames.Length)
            {
                for (int j = 0; j < 12; ++j)
                {
                    GlobalChroma[j] += chroma.Frames[p].D[j];
                }
                ++p;
            }
            for (int j = 0; j < 12; ++j)
            {
                MaxGlobalChroma = Math.Max(MaxGlobalChroma, GlobalChroma[j]);
            }

        }

        public void EstimateGlobalTonalty()
        {
            if (GlobalChroma == null) return;
            int[] Adder = new int[] { 0, 2, 4, 5, 7, 9, 11 };
            int maxAt = 0;
            double maxValue = 0;
            for(int i=0;i<12;++i)
            {
                double currentValue = 0;
                foreach(int j in Adder)
                {
                    currentValue += GlobalChroma[(i + j) % 12];
                }
                if(currentValue>maxValue)
                {
                    maxValue = currentValue;
                    maxAt = i;
                }
            }
            int laPos = maxAt - 3 < 0 ? maxAt + 9 : maxAt - 3;
            Tonalty sampleTonalty = new Tonalty(maxAt, GlobalChroma[laPos] > GlobalChroma[maxAt] ? false : true);
            foreach (BeatInfo beat in beats)
            {
                beat.Tonalty = sampleTonalty;
            }
        }
        public string ToBeatInfo()
        {
            beats = new List<BeatInfo>();
            double DeltaEPS = 0.050;
            TimingPoint lastTp=null;
            for (int i=0;i<timingPoints.Count;++i)
            {
                TimingPoint tp;
                tp = timingPoints[i];
                if (tp.BeatLength>0)
                {
                    if (lastTp != null)
                    {
                        double limitPosition = tp.Position - DeltaEPS;
                        double deltaTime = lastTp.BeatLength / 1000.0;
                        BeatInfo binfo = new BeatInfo();
                        binfo.Time = lastTp.Position;
                        binfo.Tag = lastTp.BPM.ToString();
                        binfo.BarStart = true;
                        int barCount = 0;
                        beats.Add(binfo);
                        for (double pos=lastTp.Position+deltaTime;pos<=limitPosition;pos+= deltaTime)
                        {
                            binfo = new BeatInfo();
                            binfo.Time = pos;
                            binfo.Tag = "-";
                            ++barCount;
                            if(barCount==MetreNumber)
                            {
                                binfo.BarStart = true;
                                barCount = 0;
                            }
                            beats.Add(binfo);
                        }
                    }
                    lastTp = tp;
                }
            }
            if (lastTp != null)
            {
                double limitPosition = MP3Length + DeltaEPS;
                double deltaTime = lastTp.BeatLength / 1000.0;
                BeatInfo binfo = new BeatInfo();
                binfo.Time = lastTp.Position;
                binfo.Tag = lastTp.BPM.ToString();
                binfo.BarStart = true;
                int barCount = 0;
                beats.Add(binfo);
                for (double pos = lastTp.Position + deltaTime; pos <= limitPosition; pos += deltaTime)
                {
                    binfo = new BeatInfo();
                    binfo.Time = pos;
                    binfo.Tag = "-";
                    ++barCount;
                    if (barCount == MetreNumber)
                    {
                        binfo.BarStart = true;
                        barCount = 0;
                    }
                    beats.Add(binfo);
                }
            }
            return string.Join(Environment.NewLine, beats);
        }
    }
}
