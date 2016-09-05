using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using common;
using System.Runtime.Serialization.Formatters.Binary;

namespace chordgen
{
    class FileManager
    {
        public static void TryOpen(string fileName)
        {
            if(Program.TL!=null)
            {
                Program.TL.Destroy();
            }
            Program.FullInfoPath = fileName;
            Program.InfoFileName = Path.GetFileName(Program.FullInfoPath);
            Program.FileFolder = Path.GetDirectoryName(Program.FullInfoPath);
            SongInfo info = GetInfoFromFile(fileName);
            FixOldFormatBugs(info);
            Program.SoundFileName = info.FileName;
            Program.TL = new Timeline(Program.Form.TimelinePictureBox, info, Program.FileFolder + @"\" + Program.SoundFileName);
            Program.TL.Init();
            Program.Form.RefreshInterface();
            Program.EditManager.Reset();
        }
        public static void FixOldFormatBugs(SongInfo info)
        {
            foreach(BeatInfo beat in info.beats)
            {
                if (beat.ChordTag == null) beat.ChordTag = Chord.NonChord;
                if (beat.Tonalty == null) beat.Tonalty = Tonalty.NoTonalty;
                if (beat.Position!=0)
                {
                    beat.Time = beat.Position;
                    beat.Position = 0;
                }
            }
            if(info.MetreNumber==0)
            {
                info.MetreNumber = 4;
            }
        }
        public static SongInfo GetInfoFromFile(string infoPath)
        {
            try
            {
                using (FileStream fs = new FileStream(infoPath, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (SongInfo)bf.Deserialize(fs);
                }
            }
            catch
            {
                Logger.Log("[ERROR]Load Fail: File \""+infoPath+"\" Corrupted");
                return null;
            }
        }
        public static bool SaveInfoIntoFile(SongInfo info,string infoPath)
        {
            try
            {
                using (FileStream fs = new FileStream(infoPath, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, info);
                }
                return true;
            }
            catch(Exception e)
            {
                Logger.Log("[ERROR]Cannot Write Into File\"" + infoPath + "\"");
                Logger.Log("[ERROR]" + e.ToString());
                return false;
            }
        }

        public static bool TrySave(string fileName)
        {
            if (Program.TL != null)
            {
                if(SaveInfoIntoFile(Program.TL.Info,fileName))
                {
                    Program.FullInfoPath = fileName;
                    Program.InfoFileName = Path.GetFileName(Program.FullInfoPath);
                    Program.FileFolder = Path.GetDirectoryName(Program.FullInfoPath);
                    Logger.Log("Saved successfully.");
                    return true;
                }
            }
            return false;
        }
    }
}
