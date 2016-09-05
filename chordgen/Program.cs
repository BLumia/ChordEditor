using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace chordgen
{
    static class Program
    {
        public static string FullInfoPath;
        public static string FileFolder;
        public static string InfoFileName;
        public static string SoundFileName;
        public static Timeline TL;
        public static MainForm Form;
        public static EditManager EditManager;
        public static MidiManager MidiManager;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bass.BASS_Init(-1, 190000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new MainForm();
            Application.Run(Form);
        }
    }
}
