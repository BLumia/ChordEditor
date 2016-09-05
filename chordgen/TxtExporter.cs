using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;
using System.IO;

namespace chordgen
{
    class TxtExporter : IExporter
    {
        public void ExportToFile(SongInfo info, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < info.beats.Count; ++i)
                {
                    if (i == 0 || info.beats[i].ChordTag?.ToString() != info.beats[i - 1].ChordTag?.ToString())
                    {
                        sw.WriteLine(info.beats[i].Time + "\t" + info.beats[i].ChordTag.ToString());
                    }
                }
            }
        }
    }
}
