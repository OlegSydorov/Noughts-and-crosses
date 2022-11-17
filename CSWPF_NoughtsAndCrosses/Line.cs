using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWPF_NoughtsAndCrosses
{
    class Line
    {
        int name;
        int nullCount;
        int threatcount;
        bool winFlag;
        string filler;

        Dictionary<int, string> elements;

        public int Name { get { return name; } set { name = value; } }
        public int NullCount { get { return nullCount; }  }
        public string Filler { get { return filler; } set { filler = value; } }
        public Dictionary<int, string> Elements { get { return elements; } set { elements = value; } }

        public int ThreatCount { get { return threatcount; } }
        public bool WinFlag { get { return winFlag; } }

        public Line(int x, int aa, string a, int bb, string b, int cc, string c)
        {
            elements = new Dictionary<int, string >(); 
            name = x;
            if (a == null) nullCount++;
            else filler = a;
            elements.Add(aa, a);
            if (b == null) nullCount++;
            else filler = b;
            elements.Add(bb, b);
            if (c == null) nullCount++;
            else filler = c;
            elements.Add(cc, c);
            if (nullCount!=2) filler = null;

            if (a == b && b == c) winFlag = true;
            else winFlag = false;

            if (a == b && c == null) threatcount = cc;
            else if (a == c && b == null) threatcount = bb;
            else if (b == c && a == null) threatcount = aa;
        }

        //public void Add(string elem)
        //{
        //    elements.Add(elem);
        //    if (elem == null) nullCount++;
        //}
    }
}
