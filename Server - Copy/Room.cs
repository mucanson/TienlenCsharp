using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Timers;

namespace Server
{
    unsafe class Room
    {
        Card[] Cards = new Card[52];
        string data = "";
        string PreviousCard = "";
        List<Player> players = new List<Player>();
        bool Playing = false;
        Player CurrentPlayer;
        Player LastPlayer = null;
        Bot[] serverbot;

        public Room()
        {
            InitCards();
        }
        public Room(SocketModel sm)
        {
            LastPlayer = null;
            Add(sm);
            InitCards();
            Mix();
        }
        public int compare(Card a, Card b)
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
        public int FindFistTurn()
        {
            if (LastPlayer != null)
            {
                for (int i = 0; i < players.Count; i++)
                    if (LastPlayer.SocketPlayer == players[i].SocketPlayer)
                        return i;
            }
            int min = 0;
            for (int i = 1; i < players.Count * 13; i++)
                if (compare(Cards[min], Cards[i]) == 1)
                    min = i;
            return min / 13;
        }
        void InitCards()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Cards[i * 13 + j] = new Card(j + 3, i + 1);
                }
            }
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
        public bool CanJoin()
        {
            if (players.Count == 4 || Playing)
                return false;
            return true;
        }
        public bool ReadyToStar()
        {
            if (players.Count == 1)
                return false;
            foreach (Player item in players)
                if (!item.PlayerStatus)
                    return item.PlayerStatus;
            return true;
        }

        public void Add(SocketModel s)
        {
            players.Add(new Player(s));
        }
        public void SendCard()
        {
            string str = "";
            for (int i = 0; i < players.Count; i++)
            {
                str = "YourCard";
                for (int j = 0; j < 13; j++)
                {
                    str += "|" + Cards[i * 13 + j].GetName();
                }
                players[i].SocketPlayer.SendData(str);
                players[i].NumberOfCard = 13;
            }
        }

        public void Ready(SocketModel s)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].SocketPlayer == s)
                {
                    players[i].PlayerStatus = true;
                }
            }

            if (ReadyToStar())
            {
                Thread t = new Thread(StartGame);
                t.IsBackground = true;
                t.Start();
            }
        }

        public void Cancel(SocketModel s)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].SocketPlayer == s)
                {
                    players[i].PlayerStatus = false;
                }
            }
        }
        void StartGame()
        {
            Playing = true;
            int count = 0;
            int i = FindFistTurn();
            PreviousCard = "|";
            Mix();
            SendCard();
            CurrentPlayer = players[i];
            LastPlayer = CurrentPlayer;
            while (players.Count > 1)
            {

                if (CurrentPlayer == LastPlayer)
                {
                    //count = (PreviousCard.Split('|').Count() - 1);
                    //if (count==-1)
                    //    SendAll("FlipDown|" + 0);
                    //else
                    //    SendAll("FlipDown|" + count);
                    //Thread.Sleep(100);
                    CurrentPlayer.SocketPlayer.SendData("Yourturn");

                }
                else
                {
                    CurrentPlayer.SocketPlayer.SendData("Yourturn" + PreviousCard);
                }
                count = 0;
                while (count < 60)
                {
                    if (data != "")
                    {
                        if (data != "Skip")
                        {
                            SendOtherPlayer(CurrentPlayer.SocketPlayer, data);
                            CurrentPlayer.NumberOfCard = CurrentPlayer.NumberOfCard - data.Split('|').Length + 1;
                            if (CurrentPlayer.NumberOfCard == 0)
                            {
                                reset();
                                SendAll("Win|" + players[i].SocketPlayer.GetRemoteEndpoint());
                                return;
                            }
                            PreviousCard = data;
                            LastPlayer = CurrentPlayer;
                            break;
                        }
                        else
                            break;

                    }
                    count++;
                    Thread.Sleep(200);
                }
                data = "";
                i++;
                if (i >= players.Count)
                {
                    i = 0;
                }
                if(players.Count!=0) CurrentPlayer = players[i];
            }
            if (players.Count != 0) SendAll("Win|" + players[0].SocketPlayer.GetRemoteEndpoint());
            reset();
        }

        //void StartGame1()
        //{
        //    serverbot = new Bot[4 - players.Count];
        //    Playing = true;
        //    int count = 0;
        //    int i = FindFistTurn();
        //    PreviousCard = "|";
        //    Mix();
        //    SendCard();
        //    for(int y=0;y<serverbot.Length;y++)
        //    {
        //        List<Card> temp = new List<Card>();
        //        for (int x = 51 - y * 13; x > 51 - y * 13 - 13; x--)
        //            temp.Add(Cards[x]);
        //     serverbot[y] = new Bot(temp,y.);
        //    }
        //    while(players.Count>0)
        //    {
        //        if(i>=players.Count)
        //        {
        //            if()
        //            serverbot[i-players.Count].
        //        }
        //    }
        //}
        public void SendOtherPlayer(SocketModel s, string str)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (s != players[i].SocketPlayer)
                    players[i].SocketPlayer.SendData("PlayerTurn" + str);
            }
        }
        public void TransData(SocketModel s, string str)
        {
            if (CurrentPlayer.SocketPlayer == s)
                data = str;
        }
        public int NumOfPlayer()
        {
            return players.Count;
        }
        public void SendAll(string str)
        {
            foreach (var item in players)
            {
                item.SocketPlayer.SendData(str);
            }
        }
        public void reset()
        {
            data = "";
            PreviousCard = "";
            Playing = false;
            for (int i = 0; i < players.Count; i++)
            {
                players[i].PlayerStatus = false;
            }
        }
        public void Remote(SocketModel s)
        {
            for(int i=0;i<players.Count;i++)
            {
                if (players[i].SocketPlayer == s)
                   players.Remove(players[i]);
            }
        }
        public bool cardleft(int temp, Player p)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].SocketPlayer == p.SocketPlayer)
                {
                    players[i].NumberOfCard = p.NumberOfCard;
                    return true;
                }
            }
            return false;
        }
        //BeginInvoke((Action)(() => CountDownTimer.Start()));
        public void OutRoom(SocketModel s)
        {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].SocketPlayer == s)
                        players.Remove(players[i]);
                }
            
        }

    }
}
