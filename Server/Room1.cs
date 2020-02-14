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
    class Room1
    {
        Card[] Cards = new Card[52];
        string data = "";
        string PreviousCard = "";
        List<Player> players = new List<Player>();
        bool Playing = false;
        string CurrentPlayer = "";
        string LastPlayer = "";
        Bot[] serverbot;
        List<string> Skip = new List<string>();

        public Room1(SocketModel sm)
        {
            Add(sm);
            InitCards();
            Mix();
        }
        public void Add(SocketModel s)
        {
            players.Add(new Player(s));
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
            foreach (Player item in players)
                if (!item.PlayerStatus)
                    return item.PlayerStatus;
            return true;
        }
        public void SendCard()
        {
            string str = "";
            for (int i = 0; i < players.Count; i++)
            {
                str = "#YourCard";
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
        void StartGame()
        {
            serverbot = null;
            int number = 0;
            if (players.Count < 4)
                serverbot = new Bot[4 - players.Count()];
            Playing = true;
            Mix();
            SendCard();
            //Thread.Sleep(10);
            if (serverbot != null)
            {
                for (int y = 0; y < serverbot.Count(); y++)
                {
                    List<Card> temp = new List<Card>();
                    for (int x = 51 - y * 13; x > 51 - y * 13 - 13; x--)
                        temp.Add(Cards[x]);
                    serverbot[y] = new Bot(temp, y.ToString());
                    }
                }
            Init();
            number = findfirstplayer();
            //MessageBox.Show(number.ToString());
            if (number < players.Count())
            {
                LastPlayer = players[number].SocketPlayer.GetRemoteEndpoint();
                CurrentPlayer = LastPlayer;
            }
            else
            {
                LastPlayer = serverbot[number - players.Count()].Name;
                //CurrentPlayer = LastPlayer;
                for (int index = number-players.Count(); index < serverbot.Length; index++)
                {
                    string str = serverbot[index].Name;
                    if (checkSkip(Skip, str))
                    {
                        SendAll("#Yourturn|" + str);
                        if (LastPlayer == str)
                        {
                            SendAll("#NewTurn");
                            Random rnd = new Random();
                            Thread.Sleep(rnd.Next(30, 50) * 100);
                            List<Card> temp = serverbot[index].BotFirstTurn();
                            PreviousCard = "";
                            foreach (Card item in temp)
                            {
                                PreviousCard += "|" + item.GetName();
                            }
                            SendAll("#PlayerTurn|" + str + PreviousCard);
                            //MessageBox.Show("botfirt turn" + PreviousCard);
                            //Thread.Sleep(10);
                            Skip.Clear();
                            LastPlayer = str;
                        }
                        else
                        {
                            Random rnd = new Random();
                            Thread.Sleep(rnd.Next(30, 50) * 100);
                            List<Card> temp = serverbot[index].Checkcards(PreviousCard);
                            if (temp != null)
                            {
                                PreviousCard = "";
                                foreach (Card item in temp)
                                {
                                    PreviousCard += "|" + item.GetName();
                                }
                                SendAll("#PlayerTurn|" + str + PreviousCard);
                                //Thread.Sleep(10);
                                LastPlayer = str;
                            }
                            else
                            {
                                SendAll("#Skip|" + str);
                                Skip.Add(serverbot[index].Name);
                            }
                        }
                        if (serverbot[index].returncard().Count == 0)
                        {
                            reset();
                            SendAll("#Win|");
                            return;
                        }
                    }
                }
            }
            while (true)
            {
                number = 0;
                List<Player> TempPlayer = players;
                for(int i=0;i< TempPlayer.Count();i++)
                {
                    string str= TempPlayer[i].SocketPlayer.GetRemoteEndpoint();
                    if (checkSkip(Skip, str))
                    {
                        if (LastPlayer == str)
                        {
                            SendAll("#NewTurn");
                            PreviousCard = "";
                            Skip.Clear();
                        }
                        SendAll("#Yourturn|" + str + PreviousCard);
                    }
                    while(number<70)
                    {
                        if (data != "")
                        {
                            if (data != "Skip")
                            {
                                SendAll("#PlayerTurn|" + str + data);
                                if (decrease(TempPlayer[i], data.Split('|').Length) == 0)
                                {
                                    reset();
                                    SendAll("#Win|" + TempPlayer[i].SocketPlayer.GetRemoteEndpoint());
                                    //Thread.Sleep(10);
                                    return;
                                }
                                PreviousCard = data;
                                LastPlayer = str;
                                data = "";
                                break;
                            }
                            else
                            {
                                SendAll("#Skip|" + str);
                                Skip.Add(str);
                                data = "";
                                break;
                            }
                        }
                        number++;
                        Thread.Sleep(200);
                    }
                    if(number==70)
                    {
                        SendAll("#Skip|" + str );
                        Skip.Add(str);
                    }
                    number = 0;
                }
                if (serverbot != null)
                {
                    for (int index = 0; index < serverbot.Length; index++)
                    {
                        string str = serverbot[index].Name;
                        if (checkSkip(Skip, str))
                        {
                            SendAll("#Yourturn|" + str);
                            if (LastPlayer == str)
                            {
                                SendAll("#NewTurn");
                                Random rnd = new Random();
                                Thread.Sleep(rnd.Next(30, 50) * 100);
                                List<Card> temp = serverbot[index].BotFirstTurn();
                                PreviousCard = "";
                                foreach (Card item in temp)
                                {
                                    PreviousCard += "|" + item.GetName();
                                }
                                SendAll("#PlayerTurn|"+str + PreviousCard);
                                //MessageBox.Show("botfirt turn" + PreviousCard);
                                //Thread.Sleep(10);
                                Skip.Clear();
                                LastPlayer = str;
                            }
                            else
                            {
                                Random rnd = new Random();
                                Thread.Sleep(rnd.Next(30, 50) * 100);
                                List<Card> temp = serverbot[index].Checkcards(PreviousCard);
                                if (temp != null)
                                {
                                    PreviousCard = "";
                                    foreach (Card item in temp)
                                    {
                                        PreviousCard += "|" + item.GetName();
                                    }
                                    SendAll("#PlayerTurn|" + str + PreviousCard);
                                    //Thread.Sleep(10);
                                    LastPlayer = str;
                                }
                                else
                                {
                                    SendAll("#Skip|" + str);
                                    Skip.Add(serverbot[index].Name);
                                }
                            }
                            if (serverbot[index].returncard().Count == 0)
                            {
                                reset();
                                SendAll("#Win|");
                                return;
                            }
                        }
                    }                    
                }
            }
        }
        public int NumOfPlayer()
        {
            return players.Count;
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
        public void Remote(SocketModel s)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].SocketPlayer == s)
                    players.Remove(players[i]);
            }
        }
        public void TransData(SocketModel s, string str)
        {
            if (CurrentPlayer == s.GetRemoteEndpoint())
                data = str;
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
        public void SendAll(string str)
        {
            foreach (var item in players)
            {
                item.SocketPlayer.SendData(str);
            }
        }
        public int decrease(Player player, int i)
        {
            for (int count = 0; count < players.Count; count++)
            {
                if (players[count] == player)
                {
                    players[count].NumberOfCard = players[count].NumberOfCard - i + 1;
                    return players[count].NumberOfCard;
                }
            }
            return -1;
        }
        public void SendOtherPlayer(SocketModel s, string str)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (s != players[i].SocketPlayer)
                    players[i].SocketPlayer.SendData("#PlayerTurn" + str);
            }
        }
        public bool checkSkip(List<string> ls, string str)
        {
            //foreach (string item in ls)
            foreach (string item in ls)
            {
                if (item == str)
                    return false;
            }
            return true;
        }
        public void Init()
        {
            string s = "#Init";
            for (int i = 0; i < players.Count; i++)
                s += "|" + players[i].SocketPlayer.GetRemoteEndpoint();
            for(int i=0;i<serverbot.Length;i++)
                s += "|" + serverbot[i].Name;
            SendAll(s);
        }

        int findfirstplayer()
        {
            for (int i = 0; i < players.Count(); i++)
                if (players[i].SocketPlayer.GetRemoteEndpoint() == LastPlayer)
                    return i;
            for (int i = 0; i < serverbot.Count(); i++)
                if (serverbot[i].Name == LastPlayer)
                    return i+players.Count();
            for(int i=0;i<52;i++)
            {
                if(Cards[i].GetName()=="3.1")
                {
                    return i / 13;
                }
            }
            return 0;        
        }

    }
}
