using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;
using Un4seen.Bass;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace chordgen
{
    class Timeline
    {
        public ChordEditor ChordEditor;
        public BeatEditor BeatEditor;
        public ChromaVisualizer ChromaVisualizer;

        PictureBox pictureBox;
        BufferedGraphics myBuffer;
        public Graphics G;
        public Graphics Target;
        public SongInfo Info;
        public int MP3stream;
        public double TimeScale = 50.0;
        public int TargetRightPos = 600;
        public int HorizonHeight = 50;
        public int AllHeight = 200;
        public double MouseDownTime;
        public int MouseDownPosition;


        public bool RelativeLabel { get; set; } = true;
        public bool NotRelativeLabel
        {
            get { return !RelativeLabel; }
            set { RelativeLabel = !value; }
        }

        public bool IsMouseInControl = false;
        public bool IsShiftDown = false;
        public bool IsCtrlDown = false;

        private int volumeMain = 50;
        public int VolumeMain
        {
            get
            {
                return volumeMain;
            }
            set
            {
                Bass.BASS_ChannelSetAttribute(MP3stream, BASSAttribute.BASS_ATTRIB_VOL, value / 100f);
                volumeMain = value;
            }
        }
        public enum MouseMode
        {
            Dragging=1,
            Chroma=2,
            Selection=3,
            Other=4
        };
        public MouseMode CurrentMouseMode;
        public long CurrentPosition
        {
            get
            {
                return Bass.BASS_ChannelGetPosition(MP3stream);
            }
            set
            {
                if (CurrentPosition < 0) CurrentPosition = 0;
                Bass.BASS_ChannelSetPosition(MP3stream, value);
            }
        }
        public double CurrentTime
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(MP3stream, CurrentPosition);
            }
            set
            {
                if (value < 0) value = 0;
                CurrentPosition = Bass.BASS_ChannelSeconds2Bytes(MP3stream, value);
            }
        }
        bool m_pause, m_drag;
        public bool Pause
        {
            get
            {
                return m_pause;
            }
            set
            {
                m_pause = value;
                if (m_drag || m_pause)
                    Bass.BASS_ChannelPause(MP3stream);
                else
                    Bass.BASS_ChannelPlay(MP3stream, false);
            }
        }
        public int MousePosX, MousePosY;

        public bool Drag
        {
            get
            {
                return m_drag;
            }
            set
            {
                m_drag = value;
                if (m_drag || m_pause)
                    Bass.BASS_ChannelPause(MP3stream);
                else
                    Bass.BASS_ChannelPlay(MP3stream, false);
            }
        }
        public bool Playing
        {
            get { return !m_drag && !m_pause; }
        }
        public bool IsMouseDown;
        public double TempCurrentTime;
        Pen whitePen = new Pen(Color.White);
        Pen redPen = new Pen(Color.Red);
        Pen greenPen = new Pen(Color.Green, 2);
        Brush WhiteBrush = Brushes.White;
        public Timeline(PictureBox bindingPictureBox,SongInfo info,string mp3Path)
        {
            pictureBox = bindingPictureBox;
            bindingPictureBox.BackColor = Color.Black;
            Target = bindingPictureBox.CreateGraphics();
            Info = info;
            MP3stream = Bass.BASS_StreamCreateFile(mp3Path, 0, 0, BASSFlag.BASS_SAMPLE_SOFTWARE);
            Bass.BASS_ChannelPlay(MP3stream, true);
            TempCurrentTime = CurrentTime;
            myBuffer = BufferedGraphicsManager.Current.Allocate(Target, new Rectangle(0, 0, TargetRightPos, AllHeight));
            G = myBuffer.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            ChordEditor = new ChordEditor(this);
            BeatEditor = new BeatEditor(this);
            ChromaVisualizer = new ChromaVisualizer(this);
        }
        public void Init()
        {
            ChromaVisualizer.PrepareChromaFrameImage();
        }
        public void Destroy()
        {
            Bass.BASS_StreamFree(MP3stream);
        }
        internal void KeyEvent(Keys keyCode, bool control, bool alt, bool shift)
        {
            switch(keyCode)
            {
                case Keys.ShiftKey:
                    IsShiftDown = true;
                    Triggers.ChordLabelChangeTrigger = true;
                    break;
                case Keys.ControlKey:
                    IsCtrlDown = true;
                    break;
                case Keys.Space:
                    Pause = !Pause;
                    break;
                case Keys.L:
                    VampProcessSong();
                    break;
                case Keys.P:
                    ChromaVisualizer.ChangeChromaMode();
                    Logger.Log("Chroma Mode Changed.");
                    break;
                case Keys.O:
                    if(ChordEditor.Enabled)
                    {
                        ChordEditor.ShowRawChord = !ChordEditor.ShowRawChord;
                        Logger.Log("Raw chord " + (ChordEditor.ShowRawChord ? "on" : "off"));
                    }
                    break;
                case Keys.Oemcomma:
                    if(ChordEditor.Enabled)
                        ChordEditor.MoveSelectionStart(CurrentTime);
                    else if(BeatEditor.Enabled)
                        BeatEditor.MoveSelectionStart(CurrentTime);
                    break;
                case Keys.OemPeriod:
                    if (ChordEditor.Enabled)
                        ChordEditor.MoveSelectionEnd(CurrentTime);
                    else if (BeatEditor.Enabled)
                        BeatEditor.MoveSelectionEnd(CurrentTime);
                    break;
                case Keys.OemQuestion:
                    if (ChordEditor.Enabled)
                        ChordEditor.ClearSelection();
                    else if (BeatEditor.Enabled)
                        BeatEditor.ClearSelection();
                    break;
                default:
                    for(int i=0;i<ChordEditor.InputKeys.Length;++i)
                    {
                        if(keyCode== ChordEditor.InputKeys[i])
                        {
                            ChordEditor.PerformInputChordIDUnderTonalty(i,Program.Form.RelativeLabelTonalty);
                            return;
                        }
                    }
                    Logger.Log(keyCode.ToString());
                    break;
            }
        }

        public void VampProcessSong()
        {
            //string result = VampInterface.GetVampCSV("test", Program.FileFolder + @"\" + Program.SoundFileName);
            Info.chroma = VampInterface.GetChroma(Program.FileFolder + @"\" + Program.SoundFileName);
            Info.CalcGlobalChroma();
            Info.EstimateGlobalTonalty();
            ChromaVisualizer.PrepareChromaFrameImage();
            List<RawChord> rawChords = VampInterface.GetRawChord(Program.FileFolder + @"\" + Program.SoundFileName);
            if (rawChords == null)
            {
                Logger.Log("[Warning]No data received, operation canceled by user.");
                return;
            }
            Info.rawChords = rawChords;
            Info.CalcBeatChord();
            Logger.Log("Loaded Vamp Data.");
            Program.Form.RefreshInterface();
            //MessageBox.Show(result);
        }

        internal void KeyUp(Keys keyCode, bool control, bool alt, bool shift)
        {
            switch (keyCode)
            {
                case Keys.ShiftKey:
                    IsShiftDown = false;
                    Triggers.ChordLabelChangeTrigger = true;
                    break;
                case Keys.ControlKey:
                    IsCtrlDown = false;
                    break;
                default:
                    break;
            }
        }


        internal void MouseWheel(int delta)
        {
            TimeScale *= Math.Exp(delta / 1000.0);
            if(TimeScale>5000)
            {
                TimeScale = 5000;
            }
            else if(TimeScale<10)
            {
                TimeScale = 10;
            }
        }

        public double Pos2Time(int pos)
        {
            return (pos - TargetRightPos / 2) / TimeScale + TempCurrentTime;
        }
        public int Time2Pos(double time)
        {
            return (int)((time - TempCurrentTime) * TimeScale) + TargetRightPos / 2;
        }
        public double LeftMostTime
        {
            get { return Pos2Time(0); }
        }
        public double RightMostTime
        {
            get { return Pos2Time(TargetRightPos); }
        }
        public void Draw()
        {
            TempCurrentTime = CurrentTime;
            G.Clear(Color.Black);
            G.DrawLine(whitePen, new Point(0, HorizonHeight), new Point(TargetRightPos, HorizonHeight));
            G.DrawLine(greenPen, new Point(Time2Pos(0), HorizonHeight), new Point(Time2Pos(0), 0));
            G.DrawLine(greenPen, new Point(Time2Pos(Info.MP3Length), HorizonHeight), new Point(Time2Pos(Info.MP3Length), 0));
            ChromaVisualizer.DrawChroma();
            BeatEditor.DrawBeatLine();
            ChordEditor.DrawChords();
            if(ChordEditor.Enabled)ChordEditor.EditModeUpdate();
            if (BeatEditor.Enabled) BeatEditor.EditModeUpdate();
            ChromaVisualizer.DrawTonalty();
            G.DrawLine(redPen, new Point(TargetRightPos / 2, 0), new Point(TargetRightPos / 2, AllHeight));
            myBuffer.Render(Target);
        }
        public void DragSetPosition(int newx)
        {
            CurrentTime = - (newx - MouseDownPosition) / TimeScale + MouseDownTime;
        }
        public void MouseDown(int x,int y)
        {
            IsMouseDown = true;
            if(CurrentMouseMode==MouseMode.Dragging)
            {
                Drag = true;
                MouseDownPosition = x;
                MouseDownTime = CurrentTime;
            }
            else if(CurrentMouseMode==MouseMode.Chroma)
            {
                ChromaVisualizer.ClickOnChromas(x, y);
            }
        }
        public void MouseUp(int x, int y)
        {
            IsMouseDown = false;
            Drag = false;
        }
        public void MouseMove(int x, int y)
        {
            MousePosX = x;
            MousePosY = y;
            if(!IsMouseDown)
            {
                if (y >= ChromaVisualizer.ChromaStart && y < ChromaVisualizer.ChromaStart + ChromaVisualizer.ChromaHeight * 12)
                {
                    CurrentMouseMode = MouseMode.Chroma;
                    pictureBox.Cursor = Cursors.Hand;
                }
                else if(y>= 25 && y < ChromaVisualizer.ChromaStart + ChromaVisualizer.ChromaHeight * 12)
                {
                    CurrentMouseMode = MouseMode.Dragging;
                    pictureBox.Cursor = Cursors.VSplit;
                }
                else if(y<25)
                {
                    CurrentMouseMode = MouseMode.Selection;
                    pictureBox.Cursor = Cursors.Default;
                }
                else
                {
                    CurrentMouseMode = MouseMode.Other;
                    pictureBox.Cursor = Cursors.Default;
                }
            }
            if (Drag)
                DragSetPosition(x);
        }
        public void MouseEnter()
        {
            IsMouseInControl = true;
        }
        public void MouseLeave()
        {
            IsMouseInControl = false;
        }
    }
}
