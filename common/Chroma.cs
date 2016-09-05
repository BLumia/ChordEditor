using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class Chroma
    {
        [Serializable]
        public class ChromaFrame
        {
            public double Time;
            public double[] D;
            public ChromaFrame()
            {
                D = new double[12];
            }
        }
        public double GlobalMax;
        public ChromaFrame[] Frames;
        public double FrameLength;
        public Chroma(string inputData)
        {
            GlobalMax = 0;
            int tget=inputData.LastIndexOf("\"");
            string[] splited = inputData.Substring(tget+1).Split(',');
            int n = (splited.Length - 1) / 13;
            Frames = new ChromaFrame[n];
            for(int i=0;i<n;++i)
            {
                Frames[i] = new ChromaFrame();
                Frames[i].Time = double.Parse(splited[i * 13 + 1]);
                for(int j=0;j<12;++j)
                {
                    int rel = (j + 3) % 12;//原始数据从A开始到Ab
                    Frames[i].D[j] = double.Parse(splited[i * 13 + rel + 2]);
                    GlobalMax = Math.Max(GlobalMax, Frames[i].D[j]);
                }
            }
            if (n > 1)
            {
                FrameLength = Frames[1].Time - Frames[0].Time;
            }
            else FrameLength = 0;
        }
    }
}
