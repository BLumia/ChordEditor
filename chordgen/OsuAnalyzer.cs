using common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace chordgen
{
    class OsuAnalyzer
    {
        public static double GetMP3Length(string MP3Path)
        {
            int stream = Bass.BASS_StreamCreateFile(MP3Path, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            long len_in_byte = Bass.BASS_ChannelGetLength(stream, BASSMode.BASS_POS_BYTES);
            double time = Bass.BASS_ChannelBytes2Seconds(stream, len_in_byte);
            Bass.BASS_StreamFree(stream);
            return time;
        }
        public static SongInfo ExtractFromOSUFile(string osuFilename)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            SongInfo songinfo = new SongInfo();
            string directoryName = Path.GetDirectoryName(osuFilename);
            using (StreamReader sr = new StreamReader(osuFilename))
            {
                bool tpContext = false;
                while (!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine();
                    if (buffer.Contains("[TimingPoints]"))
                    {
                        tpContext = true;
                    }
                    else if (buffer.Length >= 1 && buffer[0] == '[')
                    {
                        tpContext = false;
                    }
                    else if (tpContext)
                    {
                        string[] split = buffer.Split(',');
                        if (split.Length >=2)
                        {
                            TimingPoint timingPoint = new TimingPoint();
                            timingPoint.Offset = double.Parse(split[0]);
                            timingPoint.BeatLength = double.Parse(split[1]);
                            if(split.Length>=3)
                            {
                                timingPoint.TimeSignature = int.Parse(split[2]);
                                if(split.Length==8)
                                {
                                    timingPoint.SampleSet = int.Parse(split[3]);
                                    timingPoint.CustomSamples = int.Parse(split[4]);
                                    timingPoint.Volumn = int.Parse(split[5]);
                                    timingPoint.TimingChange = int.Parse(split[6]) > 0;
                                    timingPoint.EffectFlag = int.Parse(split[7]);

                                }
                            }
                            songinfo.timingPoints.Add(timingPoint);
                            //triple metre or quadruple metre
                            if (songinfo.MetreNumber == 0 && timingPoint.TimeSignature>0)
                            {
                                songinfo.MetreNumber = timingPoint.TimeSignature;
                            }
                        }
                    }
                    else
                    {
                        string DivisorRegex;
                        Match match;
                        /*DivisorRegex = @"^BeatDivisor:\s*(?<n>\d)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.MetreNumber = int.Parse(match.Groups["n"].Value);
                        }*/
                        DivisorRegex = @"^AudioFilename:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.FileName = match.Groups["n"].Value;
                            try
                            {
                                songinfo.MP3Length = GetMP3Length(directoryName + "\\" + songinfo.FileName);
                            }
                            catch
                            {
                                Logger.Log("[ERROR]Import Failed: Illegal MP3 File.");
                                return null;
                            }
                        }
                    }
                }
            }
            if (songinfo.MetreNumber == 0)
            {
                songinfo.MetreNumber = 4;
            }
            using (StreamWriter sw = new StreamWriter(osuFilename + ".txt"))
            {
                sw.Write(songinfo.ToBeatInfo());
            }
            /*using (FileStream sw = new FileStream(filename + ".myinfo", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(sw, songinfo);
            }*/
            //Console.ReadKey();
            return songinfo;
        }
    }
}
