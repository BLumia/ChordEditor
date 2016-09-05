using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class Chord: ICloneable
    {
        public enum NonChordTypeEnum
        {
            NonChord=0,
            XMark=1,
            QMark=2
        };

        public static string[] Num2RomeBig = new string[] { "I", "I#", "II", "IIIb", "III", "IV", "IV#", "V", "VIb", "VI", "VIIb", "VII" };
        public static string[] Num2RomeSmall = new string[] { "i", "i#", "ii", "iiib", "iii", "iv", "iv#", "v", "vib", "vi", "viib", "vii" };

        public static string[] Num2Char = new string[] { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "Ab", "A", "Bb", "B" };
        public static string[] Num2NoteString = new string[] { "1", "#1", "2", "b3", "3", "4", "#4", "5", "b6", "6", "b7", "7" };
        public int Scale;//Absolute
        public bool MajMin;
        public NonChordTypeEnum NonChordType;
        public static Chord NonChord = new Chord(NonChordTypeEnum.NonChord);
        public override string ToString()
        {
            if (Scale == -1)
            {
                switch(NonChordType)
                {
                    case NonChordTypeEnum.NonChord:
                        return "N";
                    case NonChordTypeEnum.XMark:
                        return "X";
                    case NonChordTypeEnum.QMark:
                        return "?";
                }
            }
            return Num2Char[Scale] + (MajMin? "" : "m");
        }
        public string ToString(Tonalty tonalty)
        {
            if(tonalty.Root==-1)
            {
                return ToString();
            }
            if (Scale == -1)
            {
                switch (NonChordType)
                {
                    case NonChordTypeEnum.NonChord:
                        return "N";
                    case NonChordTypeEnum.XMark:
                        return "X";
                    case NonChordTypeEnum.QMark:
                        return "?";
                }
            }
            int delta = Scale - tonalty.Root;
            if (delta < 0) delta += 12;
            return MajMin ? Num2RomeBig[delta] : Num2RomeSmall[delta];
        }
        public Chord(int scale,bool majmin)
        {
            if(scale<0||scale>=12)
            {
                throw new ArgumentException("Scale out of range");
            }
            Scale = scale;
            MajMin = majmin;
        }
        public Chord()
        {
            Scale = -1;
            MajMin = false;
            NonChordType = NonChordTypeEnum.NonChord;
        }
        public Chord(NonChordTypeEnum nonChordType)
        {
            Scale = -1;
            MajMin = false;
            NonChordType = nonChordType;
        }
        public Chord(string absoluteChordName)
        {
            int p = 0;
            switch(absoluteChordName[p])
            {
                case 'N':
                    Scale = -1;
                    MajMin = false;
                    NonChordType = NonChordTypeEnum.NonChord;
                    return;
                case 'X':
                    Scale = -1;
                    MajMin = false;
                    NonChordType = NonChordTypeEnum.XMark;
                    return;
                case '?':
                    Scale = -1;
                    MajMin = false;
                    NonChordType = NonChordTypeEnum.QMark;
                    return;
                case 'C':
                    Scale = 0;
                    break;
                case 'D':
                    Scale = 2;
                    break;
                case 'E':
                    Scale = 4;
                    break;
                case 'F':
                    Scale = 5;
                    break;
                case 'G':
                    Scale = 7;
                    break;
                case 'A':
                    Scale = 9;
                    break;
                case 'B':
                    Scale = 11;
                    break;
            }
            ++p;
            if(absoluteChordName[p]=='#')
            {
                ++Scale;
                ++p;
                if (Scale == 12) Scale = 0;
            }
            else if(absoluteChordName[p]=='b')
            {
                --Scale;
                ++p;
                if (Scale == -1) Scale = 11;
            }
            if(absoluteChordName[p]=='m')
            {
                MajMin = absoluteChordName.Length > p + 3 && (absoluteChordName.Substring(p, 3) == "maj");
            }
            else
            {
                MajMin = true;
            }
        }
        public int[] ToNotes()
        {
            if(Scale==-1)
            {
                return new int[0];
            }
            int[] result = new int[3];
            result[0] = Scale;
            result[1] = Scale + (MajMin ? 4 : 3);
            if (result[1] >= 12) result[1] -= 12;
            result[2] = Scale + 7;
            if (result[2] >= 12) result[2] -= 12;
            return result;
        }
        public int[] ToNotesUnclamped()
        {
            if (Scale == -1)
            {
                return new int[0];
            }
            int[] result = new int[3];
            result[0] = Scale;
            result[1] = Scale + (MajMin ? 4 : 3);
            result[2] = Scale + 7;
            return result;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
