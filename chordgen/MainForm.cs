using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass.AddOn.Midi;
using common;
using System.IO;

namespace chordgen
{
    public partial class MainForm : Form
    {
        public Tonalty RelativeLabelTonalty = Tonalty.NoTonalty;
        public Label[] ChordLabels;
        public bool[] DefaultChordLabelsMajMin = { true, false, false, true, false, true, false, true, false, false, true, false };
        public MainForm()
        {
            InitializeComponent();
            ChordLabels = new Label[] { ChordLabel1,ChordLabel2,ChordLabel3,ChordLabel4,ChordLabel5,ChordLabel6,
                ChordLabel7,ChordLabel8,ChordLabel9,ChordLabel10,ChordLabel11,ChordLabel12,ChordLabelN,ChordLabelX,ChordLabelQ
            };

        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.EditManager = new EditManager();
            Program.MidiManager = new MidiManager();
            Program.MidiManager.Init();
            TimelinePictureBox.MouseWheel += TimelinePictureBox_MouseWheel;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            timer1.Enabled = true;
            Logger.Register(logText);
            RefreshInterface();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                Program.TL.Draw();
                //if (Triggers.ChordLabelChangeTrigger)
                //{
                Tonalty currentTonalty = Program.TL.ChromaVisualizer.GetCurrentTonalty();
                if (Triggers.ChordLabelChangeTrigger||RelativeLabelTonalty.ToString()!= currentTonalty.ToString())
                {
                    Triggers.ChordLabelChangeTrigger = false;
                    RelativeLabelTonalty = currentTonalty;
                    for (int i = 0; i < 15; ++i)
                    {
                        ChordLabels[i].Text =
                            Program.TL.ChordEditor.GetChordLabelTextUnderTonalty(i, RelativeLabelTonalty);
                    }

                }
                //}
            }

        }

        private void TimelinePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseDown(e.X, e.Y);
        }

        private void TimelinePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseUp(e.X, e.Y);

        }

        private void TimelinePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseMove(e.X, e.Y);
        }
        private void TimelinePictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseWheel(e.Delta);
        }
        private void TimelinePictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseEnter();

        }

        private void TimelinePictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseLeave();

        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if(Program.TL!=null)
                Program.TL.KeyEvent(e.KeyCode, e.Control, e.Alt, e.Shift);
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyUp(e.KeyCode, e.Control, e.Alt, e.Shift);

        }
        private int GetChordLabelIndex(object sender)
        {
            for(int i=0;i<ChordLabels.Length;++i)
            {
                if (ChordLabels[i] == sender)
                    return i;
            }
            return -1;
        }

        private void ChordLabels_MouseClick(object sender, MouseEventArgs e)
        {
            if(Program.TL!=null)
            {
                int id = GetChordLabelIndex(sender);
                if (e.Button == MouseButtons.Right && id < 12)
                {
                    Program.MidiManager.PlayChordNotes(Program.TL.ChordEditor.GetChordFromInputUnderTonalty(id,RelativeLabelTonalty));
                }
                else if(e.Button==MouseButtons.Left)
                {
                    Program.TL.ChordEditor.PerformInputChordIDUnderTonalty(id,RelativeLabelTonalty);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Label Clicked");
            Focus();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Label2 Click");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                Program.TL.ChordEditor.Enabled = false;
                Program.TL.BeatEditor.Enabled = false;
                switch (tabControl1.SelectedTab.Text)
                {
                    case "和弦":
                        Program.TL.ChordEditor.Enabled = true;
                        break;
                    case "节拍与调性":
                        Program.TL.BeatEditor.Enabled = true;
                        break;
                }
            }
        }

        public void RefreshInterface()
        {
            tabControl1_SelectedIndexChanged(null, null);
            Triggers.ChordLabelChangeTrigger = true;
            RelativeLabelTonalty = Tonalty.NoTonalty;
            if (Program.TL!=null)
            {
                trackBarVolumeMain.DataBindings.Clear();
                trackBarVolumeMain.DataBindings.Add("Value", Program.TL, "VolumeMain", true, DataSourceUpdateMode.OnPropertyChanged);
                trackBarVolumeMIDI.DataBindings.Clear();
                trackBarVolumeMIDI.DataBindings.Add("Value", Program.MidiManager, "VolumeNote", true, DataSourceUpdateMode.OnPropertyChanged);
                trackBarMIDIDelay.DataBindings.Clear();
                trackBarMIDIDelay.DataBindings.Add("Value", Program.MidiManager, "NoteDelay", true, DataSourceUpdateMode.OnPropertyChanged);
                checkBoxAutoPlayChord.DataBindings.Clear();
                checkBoxAutoPlayChord.DataBindings.Add("Checked", Program.TL.ChordEditor, "AutoPlayMidi", true, DataSourceUpdateMode.OnPropertyChanged);
                comboBox_Metre.Text = Program.TL.Info.MetreNumber.ToString();
                //if (Program.TL.Info.Tonalty != -1)
                //    comboBox_GLTonalty.SelectedIndex = Program.TL.Info.Tonalty;
                comboBoxAlignBeats.Items.Clear();
                Program.TL.ChordEditor.AlignBeats = new List<int>();
                int bd = Program.TL.Info.MetreNumber;
                for (int i=1;i< bd; ++i)
                {
                    if(bd % i==0)
                    {
                        comboBoxAlignBeats.Items.Add(i + "拍");
                        Program.TL.ChordEditor.AlignBeats.Add(i);
                    }
                }
                int index = comboBoxAlignBeats.Items.Count;
                for (int i=1;i*bd<=12||i==1;i*=2)
                {
                    comboBoxAlignBeats.Items.Add(i + "小节");
                    Program.TL.ChordEditor.AlignBeats.Add(i * bd);
                }
                comboBoxAlignBeats.SelectedIndex = index;
                radioButtonAbsolute.DataBindings.Clear();
                radioButtonAbsolute.DataBindings.Add("Checked", Program.TL, "NotRelativeLabel", true, DataSourceUpdateMode.OnPropertyChanged);
                radioButtonRelative.DataBindings.Clear();
                radioButtonRelative.DataBindings.Add("Checked", Program.TL, "RelativeLabel", true, DataSourceUpdateMode.OnPropertyChanged);
                Text = "Edit Mode: " + Program.SoundFileName;
            }
            else
            {
                Text = "Chord Editor";
            }


        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openInfoFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            FileManager.TryOpen(openInfoFileDialog.FileName);
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Program.TL!=null)
            {
                if (!string.IsNullOrEmpty(Program.InfoFileName))
                {
                    if (FileManager.TrySave(Program.FullInfoPath))
                        return;
                }
                另存为ToolStripMenuItem_Click(sender, e);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                if (saveInfoFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                FileManager.TrySave(saveInfoFileDialog.FileName);
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openOSUFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            SongInfo info = OsuAnalyzer.ExtractFromOSUFile(openOSUFileDialog.FileName);
            if(info!=null)
            {
                if(Program.TL!=null)
                    Program.TL.Destroy();
                Program.FileFolder = Path.GetDirectoryName(openOSUFileDialog.FileName);
                Program.FullInfoPath = null;
                Program.InfoFileName = null;
                Program.SoundFileName = info.FileName;
                Program.TL = new Timeline(Program.Form.TimelinePictureBox, info, Program.FileFolder + @"\" + Program.SoundFileName);
                Program.TL.Init();
                Program.Form.RefreshInterface();
                Program.EditManager.Reset();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                List<BeatInfo> beats = Program.EditManager.Undo(Program.TL.Info);
                if (beats == null)
                {
                    MessageBox.Show("发生严重错误!");
                }
                else Program.TL.Info.beats = beats;

            }
        }
        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                List<BeatInfo> beats= Program.EditManager.Redo();
                if (beats == null)
                {
                    MessageBox.Show("发生严重错误!");
                }
                else Program.TL.Info.beats = beats;

            }

        }


        private void 编辑ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            撤销ToolStripMenuItem.Enabled = Program.EditManager.CanUndo;
            重做ToolStripMenuItem.Enabled = Program.EditManager.CanRedo;
            撤销ToolStripMenuItem.Text = "撤销" + Program.EditManager.LastUndoInfo;
            重做ToolStripMenuItem.Text = "重做" + Program.EditManager.LastRedoInfo;
        }

        private void comboBoxAlignBeats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Program.TL!=null&&comboBoxAlignBeats.SelectedIndex!=-1)
            {
                Program.TL.ChordEditor.AlignBeat =
                    Program.TL.ChordEditor.AlignBeats[comboBoxAlignBeats.SelectedIndex];
            }
        }

        private void BeatEditorButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wtf");
        }

        private void button_BEDivide_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.DivideInterval();
        }

        private void button_BEMerge_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.MergeInterval();
        }

        private void button_BENormalize_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.NormalizeInterval();

        }

        private void button_BERol_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.LeftRotateInterval();
        }

        private void button_BERor_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.RightRotateInterval();

        }

        private void button_BEDelete_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.DeleteSingleBeat();

        }

        private void button_BENew_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.CreateSingleBeat();

        }

        private void button_BEBarStart_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.ModifyBarStartOfSingleBeat();

        }

        private void comboBox_Metre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null && comboBox_Metre.SelectedItem != null)
                Program.TL.Info.MetreNumber = int.Parse(comboBox_Metre.SelectedItem.ToString());

        }

        private void button_BESelectAll_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.SelectAllBeat();
        }

        private void comboBox_GLTonalty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Program.TL != null && comboBox_GLTonalty.SelectedIndex != -1)
            //    Program.TL.Info.Tonalty = comboBox_GLTonalty.SelectedIndex;

        }

        private void comboBox_TETonalty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null&&comboBox_TETonalty.SelectedIndex!=-1)
                Program.TL.BeatEditor.TonaltyModify(comboBox_TETonalty.SelectedItem as string);
            comboBox_TETonalty.SelectedIndex = -1;
        }

        private void button_TESwitchMajMin_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.TonaltySwitchMajMin();

        }

        private void button_ExportTXT_Click(object sender, EventArgs e)
        {
            if(Program.TL!=null)
            {
                TxtExporter txtExporter = new TxtExporter();
                try
                {
                    txtExporter.ExportToFile(Program.TL.Info, Program.FileFolder +  @"\chordtag.txt");
                    Logger.Log("Exported successfully.");
                }
                catch(Exception ex)
                {
                    Logger.Log(ex.ToString());
                }

            }

        }

        private void radioButtonAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            Triggers.ChordLabelChangeTrigger = true;
        }

        private void radioButtonRelative_CheckedChanged(object sender, EventArgs e)
        {
            Triggers.ChordLabelChangeTrigger = true;

        }
    }
}
