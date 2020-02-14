using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;

namespace Server
{
    class Bot
    {
        string name = "";
        Rule rule = new Rule();
        List<Card> CardBot = new List<Card>();

        public string Name { get => name; set => name = value; }

        public Bot(List<Card> c, string str)
        {
            CardBot = c;
            CardBot.Sort(compare);
            Name = str;
        }

        public List<Card> BotFirstTurn()
        {
            List<Card> match = new List<Card>();
            for (int i = CardBot.Count; i > 0; i--)
            {
                match.Clear();
                if (GetTheLongestCombination(ref match, 0, i))
                {
                    break;
                }
            }
            foreach (Card item in match)
            {
                CardBot.Remove(item);
            }
            return match;
        }


        public bool GetTheLongestCombination(ref List<Card> cards, int location, int max)
        {
            for (int i = location; i <= cards.Count + CardBot.Count - max; i++)
            {
                cards.Add(CardBot[i]);
                if (cards.Count == max)
                {
                    string str = "";
                    for (int y = 0; y < max; y++)
                        str += "#" + cards[y].GetName();
                    Console.WriteLine(str);
                    if (rule.Check(cards))
                        return true;
                }
                else
                    if (GetTheLongestCombination(ref cards, i + 1, max))
                    return true;
                cards.RemoveAt(cards.Count - 1);
            }
            return false;
        }


        public List<Card> Checkcards(List<Card> OPlayerCard)
        {

            if (CardBot.Count >= OPlayerCard.Count)
            {
                List<Card> TempCard = new List<Card>();
                GetCards(ref TempCard, OPlayerCard, 0);
                if (TempCard.Count != 0)
                    return TempCard;
            }
            return null;
        }
        public List<Card> Checkcards(string str)
        {
            string[] s = str.Split('|');
            List<Card> OPlayerCard=new List<Card>();
            for(int i=1;i<s.Length;i++)
            {
                OPlayerCard.Add(new Card(s[i]));
            }
            if (CardBot.Count >= OPlayerCard.Count)
            {
                List<Card> TempCard = new List<Card>();
                GetCards(ref TempCard, OPlayerCard, 0);
                if (TempCard != null)
                {
                    foreach(Card item in TempCard)
                    {
                        CardBot.Remove(item);
                    }
                    return TempCard;
                }
            }
            return null;
        }


        public int CardLeft()
        {
            return CardBot.Count;
        }


        public bool GetCards(ref List<Card> cards, List<Card> OPlayerCard, int location)
        {
            for (int i = location; i <= CardBot.Count - OPlayerCard.Count + cards.Count; i++)
            {
                cards.Add(CardBot[i]);
                if (cards.Count == OPlayerCard.Count)
                {
                    if (rule.Check(cards, OPlayerCard))
                    {
                        return true;
                    }
                }
                else
                {
                    if (GetCards(ref cards, OPlayerCard, i + 1))
                        return true;
                }
                cards.RemoveAt(cards.Count - 1);
            }
            return false;
        }
        public List<Card> returncard()
        {
            return CardBot;
        }
        static int compare(Card a, Card b)
        {
            if (a.Value > b.Value)
                return 1;
            if (a.Value == b.Value && a.Rank > b.Rank)
                return 1;
            if (a.Value < b.Value)
                return -1;
            if (a.Value == b.Value && a.Rank < b.Rank)
                return -1;
            return 0;
        }
    }
}
