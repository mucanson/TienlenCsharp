using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Card
    {
        private int value;
        private int rank;

        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
        public int Rank
        {
            get
            {
                return rank;
            }

            set
            {
                rank = value;
            }
        }

        public Card(string str)
        {
            Console.WriteLine(str);
            string[] s = str.Split('.');
            Value = Int32.Parse(s[0]);
            Rank = Int32.Parse(s[1]);
        }
        public Card(int i1,int i2)
        {
            Value = i1;
            Rank = i2;
        }
        public string GetName()
        {
            return Value + "." + Rank;
        }
        public int Compare(Card c)
        {
            if (Value > c.Value || (Value == c.Value && Rank > c.Rank))
                return 1;
            return -1;
        }
    }
}
