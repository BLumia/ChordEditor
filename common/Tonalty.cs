using common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    [Serializable]
    public class Tonalty
    {
        private static string Scale2TonaltyName(int scale)
        {
            if(scale==-1)
            {
                return "?";
            }
            return Chord.Num2Char[scale];
        }
        public bool IsOnNaturalScale(int note)
        {
            if(Root==-1)
            {
                return true;
            }
            int delta = note - Root < 0 ? note - Root + 12 : note - Root;
            return (delta == 0 || delta == 2 || delta == 4 || delta == 5 || delta == 7 || delta == 9 || delta == 11);
        }

        public string NoteNameUnderTonalty(int note)
        {
            if(Root==-1)
            {
                return Chord.Num2Char[note];
            }
            return Chord.Num2NoteString[note - Root < 0 ? note - Root + 12 : note - Root];
        }
        public int Root; // Always Refer to 1(Do).
        public bool MajMin;
        public Tonalty()
        {
            Root = -1;
            MajMin = false;
        }
        static readonly string majTonaltySampleString = "C.D.EF.G.A.B",
            minTonaltySampleString = "a.bc.d.ef.g.";
        public Tonalty(string tonaltyName)
        {
            int p = 0;
            int tget = majTonaltySampleString.IndexOf(tonaltyName[p]);
            if (tget!=-1)
            {
                Root = tget;
                MajMin = true;
            }
            else
            {
                tget = minTonaltySampleString.IndexOf(tonaltyName[p]);
                if(tget!=-1)
                {
                    Root = tget;
                    MajMin = false;
                }
                else
                {
                    Root = -1;
                    MajMin = false;
                    return;
                }
            }
            ++p;
            if (tonaltyName[p] == '#')
            {
                ++Root;
                ++p;
                if (Root == 12) Root = 0;
            }
            else if (tonaltyName[p] == 'b')
            {
                --Root;
                ++p;
                if (Root == -1) Root = 11;
            }
        }
        public Tonalty(int root,bool majmin)
        {
            Root = root;
            MajMin = majmin;
        }
        public override string ToString()
        {
            //string res = "1 = " + Scale2TonaltyName(Root);
            //return Root == -1 ? res : res + "(" + (MajMin ? "Maj" : "min") + ")";
            string res= Scale2TonaltyName(Root);
            if (Root == -1 ) return res;
            if(MajMin==false)
            {
                res = Scale2TonaltyName(Root - 3 < 0 ? Root + 9 : Root - 3);
                return res + " min";
            }
            return res + " Maj";
        }
        public static Tonalty NoTonalty = new Tonalty();

        public static Tonalty RelativeTonalty(Tonalty tonalty)
        {
            return new Tonalty(tonalty.Root, !tonalty.MajMin);
        }
    }
}
