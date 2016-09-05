using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;

namespace chordgen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*TimelinePictureBox.MouseWheel += TimelinePictureBox_MouseWheel;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            if (openFileDialog1.ShowDialog()==DialogResult.Cancel)
            {
                Close();
                return;
            }
            Program.FullInfoPath = openFileDialog1.FileName;
            Program.InfoFileName = System.IO.Path.GetFileName(Program.FullInfoPath);
            Program.FileFolder = System.IO.Path.GetDirectoryName(Program.FullInfoPath);
            Program.SoundFileName = System.IO.Path.GetFileNameWithoutExtension(Program.FullInfoPath);
            Program.TL = new Timeline(TimelinePictureBox, Program.FullInfoPath, Program.FileFolder + @"\" + Program.SoundFileName);
            timer1.Enabled = true;
            Logger.Register(logText);*/
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.TL.CurrentTime.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.TL.CurrentTime = 0.0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Program.TL.Draw();
        }

        private void TimelinePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Program.TL.MouseDown(e.X, e.Y);
        }

        private void TimelinePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            Program.TL.MouseUp(e.X, e.Y);

        }

        private void TimelinePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Program.TL.MouseMove(e.X, e.Y);
        }
        private void TimelinePictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Program.TL.MouseWheel(e.Delta);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Program.TL.KeyEvent(e.KeyCode,e.Control,e.Alt,e.Shift);

        }

        private void defToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
