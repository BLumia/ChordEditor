using common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chordgen
{
    class BeatEditor
    {
        private Timeline TL;
        private SongInfo Info;
        private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    if (enabled)
                    {
                        //Enable 
                    }
                    else
                    {
                        //Disable
                    }
                    enabled = value;
                }
            }
        }
        private int pointBeatID;
        private bool ValidPointer
        {
            get { return pointBeatID >= 0 && pointBeatID < Info.beats.Count; }
        }
        private int selectionLeftBeatID, selectionRightBeatID;
        private bool ValidSelection
        {
            get
            {
                return selectionLeftBeatID < selectionRightBeatID && selectionLeftBeatID >= 0 && selectionRightBeatID < Info.beats.Count;
            }
        }
        Pen whitePen = new Pen(Color.White);
        Pen selectionPen = new Pen(Color.LightPink, 2);
        Brush transbrush = new SolidBrush(Color.FromArgb(50, Color.White));
        Pen pointerPen = new Pen(Color.Red, 2);
        public BeatEditor(Timeline tl)
        {
            TL = tl;
            Info = TL.Info;
        }
        public int GetPreviousBeatID(double time)
        {
            int left = -1, right = Info.beats.Count, mid;
            while(right>left+1)
            {
                mid = (left + right) >> 1;
                if (time < Info.beats[mid].Time) right = mid; else left = mid;
            }
            return left;
        }
        public int GetNextBeatID(double time)
        {
            int left = -1, right = Info.beats.Count, mid;
            while (right > left + 1)
            {
                mid = (left + right) >> 1;
                if (time < Info.beats[mid].Time) right = mid; else left = mid;
            }
            return right;
        }
        public int GetNearestBeatID(double time)
        {
            int left = -1, right = Info.beats.Count, mid;
            while (right > left + 1)
            {
                mid = (left + right) >> 1;
                if (time < Info.beats[mid].Time) right = mid; else left = mid;
            }
            if (right == Info.beats.Count) return left;
            if (left == -1) return right;
            return Info.beats[right].Time - time < time - Info.beats[left].Time ? right : left;

        }
        public BeatInfo GetPreviousBeat(double time)
        {
            int id = GetPreviousBeatID(time);
            return id == -1 ? null : Info.beats[id];
        }
        public BeatInfo GetNextBeat(double time)
        {
            int id = GetNextBeatID(time);
            return id == Info.beats.Count ? null : Info.beats[id];
        }
        public BeatInfo GetNearestBeat(double time)
        {
            int id = GetNearestBeatID(time);
            return id == -1 ? null : Info.beats[id];
        }
        public void DrawBeatLine()
        {
            double tempLeftMostTime = TL.LeftMostTime, tempRightMostTime = TL.RightMostTime;
            int left = GetNextBeatID(tempLeftMostTime), right = GetPreviousBeatID(tempRightMostTime);
            //foreach (BeatInfo beat in Info.beats)
            for(int i=left;i<=right;++i)
            {
                BeatInfo beat = Info.beats[i];
                if (beat.Time >= tempLeftMostTime && beat.Time <= tempRightMostTime)
                {
                    int pos = TL.Time2Pos(beat.Time);
                    TL.G.DrawLine(whitePen, new Point(pos, TL.HorizonHeight), new Point(pos, TL.HorizonHeight - (beat.BarStart ? 10 : 7)));
                }
            }
        }
        public void EditModeUpdate()
        {
            if (!Enabled) return;
            pointBeatID = GetNearestBeatID(TL.CurrentTime);
            if(ValidPointer)
            {
                double time = Info.beats[pointBeatID].Time;
                int pos = TL.Time2Pos(time);
                TL.G.DrawRectangle(pointerPen, new Rectangle(pos - 2, TL.HorizonHeight - 20, 4, 20));
            }
            if (ValidSelection)
            {
                double time1 = Info.beats[selectionLeftBeatID].Time,
                time2 = Info.beats[selectionRightBeatID].Time;
                int pos1 = TL.Time2Pos(time1),
                    pos2 = TL.Time2Pos(time2);
                Rectangle rect = new Rectangle(pos1, TL.HorizonHeight - 30, pos2 - pos1, 30);
                TL.G.FillRectangle(transbrush, rect);
                TL.G.DrawRectangle(selectionPen, rect);
            }

        }

        public void MoveSelectionStart(double time)
        {
            selectionLeftBeatID = GetNearestBeatID(time);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionRightBeatID = selectionLeftBeatID + 1;
        }
        public void MoveSelectionEnd(double time)
        {
            selectionRightBeatID = GetNearestBeatID(time);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionLeftBeatID = selectionRightBeatID - 1;
        }
        public void ClearSelection()
        {
            selectionLeftBeatID = selectionRightBeatID = -1;
        }

        internal void DivideInterval()
        {
            if(ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "细分节拍");
                List<BeatInfo> newRange = new List<BeatInfo>((selectionRightBeatID - selectionLeftBeatID) * 2 + 1);
                for (int i=selectionLeftBeatID;i<selectionRightBeatID;++i)
                {
                    newRange.Add(Info.beats[i]);
                    BeatInfo beat = Info.beats[i].Clone() as BeatInfo;
                    beat.Time = (Info.beats[i].Time + Info.beats[i + 1].Time) / 2;
                    beat.BarStart = false;
                    newRange.Add(beat);
                }
                newRange.Add(Info.beats[selectionRightBeatID]);
                Info.beats.RemoveRange(selectionLeftBeatID, selectionRightBeatID - selectionLeftBeatID + 1);
                Info.beats.InsertRange(selectionLeftBeatID, newRange);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void MergeInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "泛化节拍");
                List<BeatInfo> newRange = new List<BeatInfo>((selectionRightBeatID - selectionLeftBeatID) * 2 + 1);
                bool use = true;
                for (int i = selectionLeftBeatID; i <= selectionRightBeatID; ++i)
                {
                    if(use)
                        newRange.Add(Info.beats[i]);
                    use = !use;
                }
                Info.beats.RemoveRange(selectionLeftBeatID, selectionRightBeatID - selectionLeftBeatID + 1);
                Info.beats.InsertRange(selectionLeftBeatID, newRange);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void NormalizeInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "节拍规整化");
                int bar = 0;
                for (int i = selectionLeftBeatID; i <= selectionRightBeatID; ++i)
                {
                    Info.beats[i].BarStart = bar == 0;
                    if ((++bar) % Info.MetreNumber == 0) bar = 0;
                }
            }
        }
        internal void LeftRotateInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "循环左移");
                bool firstStart = Info.beats[selectionLeftBeatID].BarStart;
                for (int i = selectionLeftBeatID; i < selectionRightBeatID; ++i)
                {
                    Info.beats[i].BarStart = Info.beats[i + 1].BarStart;
                }
                Info.beats[selectionRightBeatID].BarStart = firstStart;
            }
        }

        internal void RightRotateInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "循环右移");
                bool firstStart = Info.beats[selectionRightBeatID].BarStart;
                for (int i = selectionRightBeatID; i > selectionLeftBeatID; --i)
                {
                    Info.beats[i].BarStart = Info.beats[i - 1].BarStart;
                }
                Info.beats[selectionLeftBeatID].BarStart = firstStart;
            }
        }

        internal void DeleteSingleBeat()
        {
            if(ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "删除当前节拍");
                Info.beats.RemoveAt(pointBeatID);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void ModifyBarStartOfSingleBeat()
        {
            if (ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "修改起始属性");
                Info.beats[pointBeatID].BarStart = !Info.beats[pointBeatID].BarStart;
            }
        }

        internal void CreateSingleBeat()
        {
            if (ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "新增节拍");
                throw new NotImplementedException();
            }
        }

        internal void SelectAllBeat()
        {
            selectionLeftBeatID = 0;
            selectionRightBeatID = Info.beats.Count - 1;
        }

        internal void TonaltyModify(string newTonalty)
        {
            if (ValidSelection)
            {
                Tonalty tonalty = new Tonalty(newTonalty);
                Program.EditManager.BeforePreformEdit(Info, "修改调性为" + newTonalty);
                for (int i = selectionLeftBeatID; i < selectionRightBeatID; ++i)
                {
                    Info.beats[i].Tonalty = tonalty;
                }
            }

        }

        internal void TonaltySwitchMajMin()
        {
            if(ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "修改关系大小调");
                for (int i=selectionLeftBeatID;i<selectionRightBeatID;++i)
                {
                    Info.beats[i].Tonalty = Tonalty.RelativeTonalty(Info.beats[i].Tonalty);
                }
            }
        }
    }
}
