using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using common;

namespace osu2beat
{
    class Program
    {
        //[DllImport("bass_fx.dll",CharSet =CharSet.Auto)]
        //public static extern bool BASS_FX_BPM_BeatDecodeGet(int channel,double startSec,double endSec,BASSFX)
        //private static BPMPROGRESSPROC _bpmProc;
        static string GlobalPath= @"C:\Users\jjy\AppData\Local\osu!\Songs";

        private static void MyBPMProc(int channel, float percent,IntPtr whatever)
        {
            Console.Write("{0}%\r", percent);
        }
        static double GetMP3Length(string MP3Path)
        {
            int stream = Bass.BASS_StreamCreateFile(MP3Path, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            //Console.WriteLine(Bass.BASS_ChannelPlay(stream, true));
            long len_in_byte = Bass.BASS_ChannelGetLength(stream, BASSMode.BASS_POS_BYTES);
            double time = Bass.BASS_ChannelBytes2Seconds(stream, len_in_byte);
            //ToyMp3.Mp3Stream mp3 = new ToyMp3.Mp3Stream(new FileStream("input.mp3",FileMode.Open));
            /*bpmProc = MyBPMProc;
            BassFx.LoadMe();
            float bpm = BassFx.BASS_FX_BPM_DecodeGet(stream, 0.0, time, 0, BASSFXBpm.BASS_FX_BPM_BKGRND | BASSFXBpm.BASS_FX_FREESOURCE | BASSFXBpm.BASS_FX_BPM_MULT2,
                                                  _bpmProc, IntPtr.Zero);
            BassFx.BASS_FX_BPM_Free(stream);
            */
            //Console.WriteLine(time);
            //Console.WriteLine(bpm);
            Bass.BASS_StreamFree(stream);
            return time;

        }
        static void Deal(string musicfoldername,string osufilename)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            SongInfo songinfo = new SongInfo();
            using (StreamReader sr = new StreamReader(GlobalPath + "\\" + musicfoldername + "\\" + osufilename))
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
                        if (split.Length == 8)
                        {
                            TimingPoint timingPoint = new TimingPoint();
                            timingPoint.Offset = double.Parse(split[0]);
                            timingPoint.BeatLength = double.Parse(split[1]);
                            timingPoint.TimeSignature = int.Parse(split[2]);
                            timingPoint.SampleSet = int.Parse(split[3]);
                            timingPoint.CustomSamples = int.Parse(split[4]);
                            timingPoint.Volumn = int.Parse(split[5]);
                            timingPoint.TimingChange = int.Parse(split[6]) > 0;
                            timingPoint.EffectFlag = int.Parse(split[7]);
                            songinfo.timingPoints.Add(timingPoint);
                        }
                        else if(split.Length>=2)
                        {
                            TimingPoint timingPoint = new TimingPoint();
                            timingPoint.Offset = double.Parse(split[0]);
                            timingPoint.BeatLength = double.Parse(split[1]);
                            songinfo.timingPoints.Add(timingPoint);

                        }
                    }
                    else
                    {
                        string DivisorRegex = @"^BeatDivisor:\s*(?<n>\d)\s*$";
                        Match match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.MetreNumber = int.Parse(match.Groups["n"].Value);
                        }
                        DivisorRegex = @"^AudioFilename:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.FileName = match.Groups["n"].Value;
                            songinfo.MP3Length = GetMP3Length(GlobalPath + "\\" + musicfoldername + "\\" + songinfo.FileName);
                        }
                    }
                }
            }
            if(songinfo.MetreNumber==0)
            {
                songinfo.MetreNumber = 4;
            }
            using (StreamWriter sw = new StreamWriter(GlobalPath + "\\" + musicfoldername + "\\" + songinfo.FileName  + ".txt"))
            {
                sw.Write(songinfo.ToBeatInfo());
            }
            using (FileStream sw = new FileStream(GlobalPath + "\\" + musicfoldername + "\\" + songinfo.FileName + ".myinfo",FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(sw, songinfo);
            }
            //Console.ReadKey();

        }
        static void Main(string[] args)
        {
            DirectoryInfo folder = new DirectoryInfo(GlobalPath);
            foreach(DirectoryInfo subfolder in folder.GetDirectories())
            {
                //MusicFolderName = subfolder.Name;
                FileInfo[] files = subfolder.GetFiles("*.osu");
                if(files.Length>0)
                {
                    FileInfo file = files[0];
                    //Console.WriteLine(file.Name);
                    Deal(subfolder.Name, file.Name);
                }
            }
            return;
        }
    }
}
