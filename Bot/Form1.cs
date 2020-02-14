using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot
{
    public partial class Form1 : Form
    {
        Card[] Cards = new Card[52];
        List<Card> cards = new List<Card>();
        Bot[] bot =new Bot[3];
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Cards[i * 13 + j] = new Card(j + 3, i + 1);
                }
            }
            Mix();
            for (int y = 0; y < 3; y++)
            {
                cards.Clear();
                for (int i = 0; i < 13; i++)
                {
                    cards.Add(Cards[13*y+i]);
                }
                cards.Sort(compare);
                for (int i = 0; i < 13; i++)
                {
                    switch (y)
                    {
                        case 0:
                            textBox1.AppendText(cards[i].GetName() + "#");
                            break;
                        case 1:
                            textBox2.AppendText(cards[i].GetName() + "#");
                            break;
                        case 2:
                            textBox3.AppendText(cards[i].GetName() + "#");
                            break;
                    }
                }
                bot[y] = new Bot(cards);
            }
            cards.Clear();
            for (int i=39;i<52;i++)
            {
                cards.Add(Cards[i]);
            }
            cards.Sort(compare);
            for (int i = 0; i < 13; i++)
            {
                textBox4.AppendText(cards[i].GetName() + "#");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<Card> temp = new List<Card>();
            //string[] str = textBox1.Text.Split('|');
            //for(int i=1;i<str.Count();i++)
            //{ 
            //    temp.Add(new Card(str[i]));
            //}
            //bot.Checkcards(temp);
            List<Card> temp =bot[0].BotFirstTurn();
            textBox6.Text = "";
            foreach (Card item in temp)
            {
                textBox6.AppendText(item.GetName() + "#");
            }
            textBox1.Text="";
            foreach (Card item in bot[0].returncard())
            {
                textBox1.AppendText(item.GetName() + "#");
            }
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
        void Mix()
        {
            Random rnd = new Random();
            Card temp;
            int n;
            for (int i = 0; i < 52; i++)
            {
                n = rnd.Next(0, 51);
                temp = Cards[i];
                Cards[i] = Cards[n];
                Cards[n] = temp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Card> temp = bot[1].BotFirstTurn();
            foreach (Card item in temp)
            {
                textBox6.AppendText(item.GetName() + "#");
            }
            //foreach (Card item in bot[1].GetCards())
            //{
            //    textBox6.AppendText(item.GetName() + "#");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Card> temp = bot[2].BotFirstTurn();
            foreach (Card item in temp)
            {
                textBox6.AppendText(item.GetName() + "#");
            }
            //foreach (Card item in bot[2].GetCards())
            //{
            //    textBox6.AppendText(item.GetName() + "#");
            //}
        }
    }
}
