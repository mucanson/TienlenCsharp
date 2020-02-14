using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Rule
    {
        public int Checktype(List<Card> c)
        {
            if (c.First().Value == c.Last().Value)
                return 0;
            if (c.First().Value + c.Count - 1 == c.Last().Value)
                return 1;
            return 3;
        }
        bool OneValue(List<Card> c)
        {
            int n = c.Count;
            if (n > 4)
                return false;
            for (int i = 1; i < n; i++)
            {
                if (c[i - 1].Value != c[i].Value)
                    return false;
            }
            return true;
        }

        bool Straight(List<Card> c)
        {
            if (c.Last().Value == 15)
                return false;
            int n = c.Count;
            if (n < 3)
                return false;
            for (int i = 1; i < n; i++)
            {
                if (c[i - 1].Value != (c[i].Value - 1))
                {
                    //Console.WriteLine(c1[i - 1].Value.ToString());
                    //Console.WriteLine(c1[i].Value.ToString());
                    //Console.WriteLine(i.ToString());
                    return false;
                }
            }
            return true;
        }

        bool Fluss(List<Card> c)
        {
            if (c.Last().Value == 15)
                return false;
            int n = c.Count;
            if (n % 2 == 1 || n < 6)
                return false;
            for (int i = 2; i < n; i += 2)
            {
                if (c[i - 2].Value + 1 != c[i].Value)
                    return false;
            }
            for (int i = 1; i < n; i += 2)
            {
                if (c[i - 1].Value != c[i].Value)
                    return false;
            }
            return true;
        }
        public bool Check(List<Card> c1, List<Card> c2)
        {
            //MessageBox.Show(c2.Last().GetName());
            //MessageBox.Show(c1.Last().GetName());
            //MessageBox.Show(c2.Count.ToString());
            //MessageBox.Show(c1.Count.ToString());
            if (c1.Count == c2.Count)
            {
                if (c2.Last().Value > c1.Last().Value)
                    return false;
                if (c2.Last().Value == c1.Last().Value && c2.Last().Rank >= c1.Last().Rank)
                    return false;
                switch (Checktype(c2))
                {
                    case 0:
                        return OneValue(c1);
                    case 1:
                        return Straight(c1);
                    case 3:
                        return Fluss(c1);
                    default:
                        return true;
                }
            }
            else
            {
                if (Checktype(c2) != 0)
                    return false;
                if (c2.Last().Value != 15)
                    return false;
                switch (Checktype(c1))
                {
                    case 0:
                        if (!OneValue(c1) || c2.Count > 1 || c1.Count != 4)
                            return false;
                        break;
                    case 1:
                        return false;
                    case 3:
                        if (!Fluss(c1) || ((c2.Count + 2) * 2 > c1.Count))
                            return false;
                        break;
                    default:
                        return true;

                }
                return true;
            }
        }
        public bool Check(List<Card> c1)
        {
            if (c1.Count < 1)
                return false;
            switch (Checktype(c1))
            {
                case 0:
                    return OneValue(c1);
                case 1:
                    return Straight(c1);
                case 3:
                    return Fluss(c1);
                default:
                    return true;
            }
        }
        //public string Check(List<Card> c1, List<Card> c2)
        //{
        //    int n = c1.Count ;
        //    int m = c2.Count;
        //    if (n != m)
        //        return "deo danh duoc nha";
        //    if (c2[n].Value > c1[n].Value)
        //        return "deo danh duoc nha";
        //    if (c2[n].Value == c1[n].Value && c2[n].Rank > c1[n].Rank)
        //        return "deo danh duoc nha";
        //    switch (Checktype(c2))
        //    {
        //        case 0:
        //            if (OneValue(c1))
        //                return "OneValue";
        //            else
        //                return "deo danh duoc nha";
        //        case 1:
        //            if (Straight(c1))
        //                return "Straight";
        //            else
        //                return "deo danh duoc nha";
        //        case 3:
        //            if (Fluss(c1))
        //                return "Fluss";
        //            else
        //                return "deo danh duoc nha";
        //        default:
        //            return "trash";
        //    }
        //}
        //public string Check(List<Card> c1)
        //{
        //    //Console.WriteLine(Checktype(c1).ToString());
        //    switch (Checktype(c1))
        //    {
        //        case 0:
        //            if (OneValue(c1))
        //                return "OneValue";
        //            else
        //                return "deo danh duoc nha";
        //        case 1:
        //            if (Straight(c1))
        //                return "Straight";
        //            else
        //                return "deo danh duoc nha";
        //        case 3:
        //            if (Fluss(c1))
        //                return "Fluss";
        //            else
        //                return "deo danh duoc nha";
        //        default:
        //            return "trash";
        //    }
        //}





    }
}
