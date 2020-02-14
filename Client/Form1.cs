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
namespace Client
{
    public partial class Form1 : Form
    {
        int port = 12000;
        List<Card> MyCard = new List<Card>();
        private TCPModel tcpForPlayer;
        bool yourturn = false;
        Thread t;
        const int Vertical = 170;
        const int Horizontal = 598;
        Button[] CardButton = new Button[13];
        Button[] CardHavePlay = new Button[13];
        Button[,] Enemy = new Button[3, 13];
        string[] EnemyName = new string[3];
        int[] EnemyCardLeft = new int[3];
        List<Card> CardChoose = new List<Card>();
        List<Card> OPlayerCard = new List<Card>();

        Rule rule = new Rule();
        private TCPModel tcpForChoosingServer;

        private void Form1_Load(object sender, EventArgs e)
        {
            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            this.FormClosed += new FormClosedEventHandler(CloseCommunicationThread);
            for (int i = 0; i < 13; i++)
            {
                //Enemy[0,i].Image = Image.FromFile(@"D:\TienLen - Copy - Copy - Copy - Copy(2)\TienLen - Copy - Copy - Copy - Copy(2)\Client\Cards\blue_back.png");
                Enemy[0, i] = new Button();
                Enemy[0, i].Location = new System.Drawing.Point(70, Horizontal - i * 50 + 70);
                Enemy[0, i].Margin = new System.Windows.Forms.Padding(4);
                Enemy[0, i].Name = Enemy[0, i].ToString();
                Enemy[0, i].Size = new System.Drawing.Size(100, 60);
                Enemy[0, i].BackgroundImageLayout = ImageLayout.Stretch;
                Enemy[0, i].FlatAppearance.BorderSize = 0;
                this.Controls.Add(Enemy[0, i]);
                //Enemy[2, i].Image = Image.FromFile(@"D:\TienLen - Copy - Copy - Copy - Copy(2)\TienLen - Copy - Copy - Copy - Copy(2)\Client\Cards\blue_back.png");
                Enemy[2, i] = new Button();
                Enemy[2, i].Location = new System.Drawing.Point(this.Width - 200, Horizontal - i * 50 + 70);
                Enemy[2, i].Margin = new System.Windows.Forms.Padding(4);
                Enemy[2, i].Name = Enemy[2, i].ToString();
                Enemy[2, i].Size = new System.Drawing.Size(100, 60);
                Enemy[2, i].BackgroundImageLayout = ImageLayout.Stretch;
                Enemy[2, i].FlatAppearance.BorderSize = 0;
                this.Controls.Add(Enemy[2, i]);
            }
            int f = (this.Width - (12 * 50 + 60)) / 2;
            for (int i = 0; i < 13; i++)
            {
                //Enemy[1, i].Image = Image.FromFile(@"D:\TienLen - Copy - Copy - Copy - Copy(2)\TienLen - Copy - Copy - Copy - Copy(2)\Client\Cards\blue_back.png");
                Enemy[1, i] = new Button();
                Enemy[1, i].Location = new System.Drawing.Point(f + i * 50, 50);
                Enemy[1, i].Margin = new System.Windows.Forms.Padding(4);
                Enemy[1, i].Name = Enemy[1, i].ToString();
                Enemy[1, i].Size = new System.Drawing.Size(60, 100);
                Enemy[1, i].BackgroundImageLayout = ImageLayout.Stretch;
                Enemy[1, i].FlatAppearance.BorderSize = 0;
                this.Controls.Add(Enemy[1, i]);
            }

            for (int i = 12; i >= 0; i--)
            {
                CardButton[i] = new Button();
                CardButton[i].Location = new System.Drawing.Point(Vertical + (i * 65), Horizontal);
                CardButton[i].Margin = new System.Windows.Forms.Padding(4);
                CardButton[i].Name = CardButton[i].ToString();
                CardButton[i].Size = new System.Drawing.Size(120, 176);
                CardButton[i].BackgroundImageLayout = ImageLayout.Stretch;
                CardButton[i].FlatAppearance.BorderSize = 0;
                CardButton[i].Click += new System.EventHandler(this.PBtChoose_click);
                this.Controls.Add(CardButton[i]);
            }
            for (int i = 12; i >= 0; i--)
            {
                CardHavePlay[i] = new Button();
                CardHavePlay[i].Location = new System.Drawing.Point(Vertical + (i * 65), Horizontal - 350);
                CardHavePlay[i].Margin = new System.Windows.Forms.Padding(4);
                CardHavePlay[i].Name = CardButton[i].ToString();
                CardHavePlay[i].Size = new System.Drawing.Size(120, 176);
                CardHavePlay[i].BackgroundImageLayout = ImageLayout.Stretch;
                CardHavePlay[i].FlatAppearance.BorderSize = 0;
                this.Controls.Add(CardHavePlay[i]);
            }
            CheckForIllegalCrossThreadCalls = false;
            CountDownTimer.Interval = 100;
            PBCountDown.Maximum = 10000;
            PBCountDown.Value = 0;
            PBCountDown.Step = 100;
            DetectServer();
            Connect();
            PBCountDown.Visible = false;
            HideButton(CardButton);
            HideButton(CardHavePlay);
            ChooseRoomForm();
            for (int i = 0; i < 3; i++)
                for (int y = 0; y < 13; y++)
                {
                    Enemy[i, y].Visible = false;
                    Enemy[i, y].Image = Image.FromFile(@"D:\TienLen - Copy - Copy - Copy - Copy (2)\TienLen - Copy - Copy - Copy - Copy (2)\Client\Cards\blue_back.png");
                }
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            if (PBCountDown.Maximum-100 == PBCountDown.Value)
            {
                    if (OPlayerCard.Count == 0)
                    {
                        CardChoose.Clear();
                        CardChoose.Add(MyCard.First());
                        tcpForPlayer.SendData("|" + MyCard.First().GetName());
                        MyCard.Remove(MyCard.First());
                        SeperateData(CardChoose, CardHavePlay);
                        SeperateData(MyCard, CardButton);
                    }
                    else
                    {
                        tcpForPlayer.SendData("Skip");
                    }
                    yourturn = false;
                    label2.Text = "";
                    CountDownTimer.Stop();
                PBCountDown.Value = 0;
                    PBCountDown.Visible = false;
            }
            else
            PBCountDown.PerformStep();
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
        public void HideButton(Button[] b)
        {
            for(int i=0; i<b.Length;i++)
            {
                b[i].Visible = false;
            }
        }
        private void PBtChoose_click(object sender, EventArgs e)
        {
            Button temp = ((Button)sender);
            if (temp.Location.Y == Horizontal)
                temp.Location = new System.Drawing.Point(temp.Location.X, temp.Location.Y - 50);
            else
                temp.Location = new System.Drawing.Point(temp.Location.X, temp.Location.Y + 50);
        }
        private void btnPlay_click(object sender, EventArgs e)
        {
            if (yourturn == true)
            {
                PlayACard();
                bool CheckYourCard;
                if (OPlayerCard.Count == 0)
                    CheckYourCard = rule.Check(CardChoose);
                else
                    CheckYourCard = rule.Check(CardChoose, OPlayerCard);
                if (CheckYourCard)
                {
                    string str = "";
                    foreach (Card item in CardChoose)
                    {
                        str += "|" + item.GetName();
                        MyCard.Remove(item);
                    }
                    tcpForPlayer.SendData(str);
                    HideChosenCard();
                    SeperateData(CardChoose, CardHavePlay);
                    SeperateData(MyCard, CardButton);
                    yourturn = false;
                    label2.Text = "";
                    PBCountDown.Visible = false;
                    CountDownTimer.Stop();
                }
                else
                {
                    MessageBox.Show("Cards Invalid");
                    CardDown(CardButton);
                }
            }
            else
            {
                MessageBox.Show("not your turn");
                CardDown(CardButton);
            }
        }
        private void HideChosenCard()
        {
            for(int i=0;i<13;i++)
            {
                if (CardButton[i].Location.Y != Horizontal)
                {
                    CardButton[i].Location = new System.Drawing.Point(CardButton[i].Location.X, Horizontal);
                    CardButton[i].Visible = false;
                }
            }
        }
        private void SeperateData(string[] s,Button[] b)
        {
            int m = (this.Size.Width - ((s.Length - 3) * 65 + 120)) / 2;

            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            //try
            //{
            //SortCard(s1);
            for (int i = 0; i < s.Length-2; i++)
                {
                b[i].Location = new System.Drawing.Point(m + (i * 65), b[i].Location.Y);
                //Card[i] = new Card(s1[i]);
                //MessageBox.Show(s1[i]);
                b[i].Image = Image.FromFile(s1 + s[i+2]+".png");
                    b[i].Visible = true;
                }
                for(int i=s.Length-1;i<13;i++)
                {
                    b[i].Visible = false;
                }
            //}
            //catch
            //{
            //    this.Close();
            //}
        }
        private void SeperateData(List<Card> c, Button[] b)
        {
            int n = c.Count;
            int m = (this.Size.Width - ((n - 1) * 65 + 120)) / 2;
            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            //try
            //{
            //SortCard(s1);
            for (int i = 0; i < n; i++)
            {
                //Card[i] = new Card(s1[i]);
                //MessageBox.Show(s1[i]);
                b[i].Location = new System.Drawing.Point(m + (i * 65), b[i].Location.Y);
                b[i].Image = Image.FromFile(s1 + c[i].GetName() + ".png");
                b[i].Visible = true;
            }
            for (int i = n; i < 13; i++)
            {
                b[i].Visible = false;
            }
            //}
            //catch
            //{
            //    this.Close();
            //}
        }
        private void SeperateData(Card[] c, Button[] b)
        {
            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            //try
            //{
            //SortCard(s1);
            int n = c.Length;
            for (int i = 0; i < n; i++)
            {
                //Card[i] = new Card(s1[i]);
                //MessageBox.Show(s1[i]);
                b[i].Image = Image.FromFile(s1 + c[i].GetName() + ".png");
                b[i].Visible = true;
            }
            for (int i = n; i < 13; i++)
            {
                b[i].Visible = false;
            }
            //}
            //catch
            //{
            //    this.Close();
            //}
        }
        private void GetCard(string[] str)
        {
            for(int i=1;i<str.Length;i++)
            {
                MyCard.Add(new Card(str[i]));
            }
        }
        Comparison<Card> comparison = new Comparison<Card>(compare);
        private void SortCard(Card[] c)
        {

            ////sort
            //string temp;
            //for (int i = 0; i < s.Length - 1; i++)
            //{
            //    for (int j = i; j < s.Length; j++)
            //    {
            //        if (s[i].Length==s[j].Length)
            //        {
            //            if (string.Compare(s[i], s[j]) == 1)
            //            {
            //                temp = s[i];
            //                s[i] = s[j];
            //                s[j] = temp;
            //            }
            //        }
            //        if(s[i].Length>s[j].Length)
            //        {
            //            temp = s[i];
            //            s[i] = s[j];
            //            s[j] = temp;
            //        }
            //    }
            //}
        }
        public void Connect()
        {
            string ip = "127.0.0.1";
            tcpForPlayer = new TCPModel(ip, port);
            if (tcpForPlayer.ConnectToServer() == -1)
                return;
            this.Text = tcpForPlayer.UpdateInformation();
            t = new Thread(Commmunication);
            t.IsBackground = true;
            t.Start();
            //tcpForOpponent = new TCPModel(ip, port);
            //tcpForOpponent.ConnectToServer();
        }
        public void Commmunication()
        {
            int rem=0;
            string[] s;
            string[] OrderAndCard;
            while (true)
            {
                string str = tcpForPlayer.ReadData();
                s = str.Split('#');
                //MessageBox.Show(str);
                //Console.WriteLine(s);
                for(int z=1;z<s.Length;z++)
                {
                    OrderAndCard = s[z].Split('|');
                    //for(int i=0;i<OrderAndCard.Length;i++)
                    //{
                    //    MessageBox.Show(OrderAndCard[i]);
                    //}
                    switch (OrderAndCard[0])
                    {
                        case "Init":
                            for (int i = 0; i < 4; i++)
                            {
                                if (OrderAndCard[i + 1] != tcpForPlayer.UpdateInformation())
                                {
                                    for (int y = 0; y < 13; y++)
                                        Enemy[rem, y].Visible = true;
                                    EnemyName[rem] = OrderAndCard[i + 1];
                                    EnemyCardLeft[rem] = 12;
                                    rem++;
                                }
                            }
                            label3.Text = EnemyName[0];
                            label4.Text = EnemyName[1];
                            label5.Text = EnemyName[2];
                            break;
                        case "NewTurn":
                                label7.Text = "";
                                label8.Text = "";
                                label9.Text = "";
                                label10.Text = "";
                            HideButton(CardHavePlay);
                            break;
                        case "Yourturn":
                            label6.Text = OrderAndCard[1] + "'s Turn";
                            if (OrderAndCard[1] == tcpForPlayer.UpdateInformation())
                            {
                                OPlayerCard.Clear();
                                for (int i = 2; i < OrderAndCard.Length; i++)
                                {
                                    OPlayerCard.Add(new Card(OrderAndCard[i]));
                                }
                                yourturn = true;
                                label2.Text = "yourturn";
                                PBCountDown.Visible = true;
                                PBCountDown.Value = 0;
                                BeginInvoke((Action)(() => CountDownTimer.Start()));
                            }
                            break;
                        case "Skip":
                            //MessageBox.Show("here");
                            //MessageBox.Show(s[z]);
                            if (OrderAndCard[1] == tcpForPlayer.UpdateInformation())
                            { label10.Text = "Bo qua";
                                break;
                            }
                            for (int i=0;i<3;i++)
                            {
                                if (EnemyName[i] == OrderAndCard[1])
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            label7.Text = "Bo qua";
                                            break;
                                        case 1:
                                            label8.Text = "Bo qua";
                                            break;
                                        case 2:
                                            label9.Text = "Bo qua";
                                            break;
                                    }
                                    break;
                                }
                            }
                            break;
                        case "YourCard":
                            GetCard(OrderAndCard);
                            MyCard.Sort(compare);
                            SeperateData(MyCard, CardButton);
                            MatchForm();
                            break;
                        case "PlayerTurn":
                            SeperateData(OrderAndCard, CardHavePlay);
                            for (int i=0;i<3;i++)
                                if (EnemyName[i]== OrderAndCard[1])
                                {
                                    //MessageBox.Show(s[z]);
                                    //MessageBox.Show(EnemyCardLeft[i].ToString());
                                    //MessageBox.Show((EnemyCardLeft[i] - OrderAndCard.Length ).ToString());
                                    for (int y = EnemyCardLeft[i]; y > EnemyCardLeft[i] - OrderAndCard.Length+2; y--)
                                        Enemy[i, y].Visible = false;
                                    EnemyCardLeft[i] = EnemyCardLeft[i] - OrderAndCard.Count() +2;
                                    break;
                                }
                            break;
                        case "CreateRoomSuccessful":
                            MessageBox.Show("CreateRoomSuccessful");
                            ReadyForm();
                            label1.Visible = true;
                            label1.Text = "Room: " + OrderAndCard[1];
                            break;
                        case "RoomFound":
                            MessageBox.Show("RoomFound");
                            ReadyForm();
                            label1.Visible = true;
                            label1.Text = "Room: " + OrderAndCard[1];
                            break;
                        case "Win":
                            Thread.Sleep(1000);
                            for (int i = 0; i < 3; i++)
                                for (int y = 0; y < 13; y++)
                                    Enemy[i, y].Visible = false;
                            WiningForm(tcpForPlayer.UpdateInformation() == OrderAndCard[1]);
                            label3.Text = "";
                            label4.Text = "";
                            label5.Text = "";
                            label6.Text = "";
                            label7.Text = "";
                            label8.Text = "";
                            label9.Text = "";
                            label10.Text = "";
                            HideButton(CardButton);
                            HideButton(CardHavePlay);
                            MyCard.Clear();
                            break;
                        //case "FlipDown":
                        //    FlipDown(Int32.Parse(OrderAndCard[1]), CardHavePlay);
                        //    break;
                        case "Exit":
                            HideButton(CardButton);
                            HideButton(CardHavePlay);
                            ChooseRoomForm();
                            CountDownTimer.Stop();
                            PBCountDown.Visible = false;
                            label2.Visible = false;
                            label1.Visible = false;
                            MyCard.Clear();
                            break;
                    }
                }

            }
            //while (true)
            //{
            //    Console.WriteLine("chokhang");
            //}
        }
        public void PlayACard()
        {
            CardChoose.Clear();
            for(int i=0;i<MyCard.Count;i++)
            {
                if (CardButton[i].Location.Y != Horizontal)
                {
                    CardChoose.Add(MyCard[i]);
                }
            }
        }
        
        public void CloseCommunicationThread(object sender, FormClosedEventArgs e)
        {
            tcpForPlayer.SendData("Exit");
            if (t!=null &&t.IsAlive)
                {
                    tcpForPlayer.CloseConnection();
                    t.Abort();
                }
            CountDownTimer.Stop();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Card[] c;
            //Rule r = new Rule();
            //string[] str = textBox2.Text.Split('|');
            //c = new Card[str.Length];
            //for (int i = 0; i < str.Length; i++)
            //{
            //    c[i] = new Card(str[i]);
            //}
            //MessageBox.Show(r.Check(c));
            tcpForPlayer.SendData("Create");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tcpForPlayer.SendData("Search");
            //Card[] c1, c2;
            //Rule r = new Rule();
            //string[] str1 = textBox1.Text.Split('|');
            //string[] str2 = textBox2.Text.Split('|');
            //c1 = new Card[str1.Length];
            //c2 = new Card[str2.Length];
            //for (int i = 0; i < str1.Length; i++)
            //{
            //    c1[i] = new Card(str1[i]);
            //}
            //for (int i = 0; i < str2.Length; i++)
            //{
            //    c2[i] = new Card(str2[i]);
            //}
            //MessageBox.Show(r.Check(c1, c2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(yourturn==true)
            {
                if (OPlayerCard.Count == 0)
                {
                    CardChoose.Clear();
                    CardChoose.Add(MyCard.First());
                    tcpForPlayer.SendData("|" + MyCard.First().GetName());
                    MyCard.Remove(MyCard.First());
                    SeperateData(CardChoose, CardHavePlay);
                    SeperateData(MyCard, CardButton);
                }
                else
                {
                    tcpForPlayer.SendData("Skip");
                }
                yourturn = false;
                label2.Text = "";
                CountDownTimer.Stop();
                PBCountDown.Visible = false;
            }
            CardDown(CardButton);

        }
        private void ChooseRoomForm()
        {
            btnTryAgain.Visible = false;
            EndScreen.Visible = false;
            SearchRoom.Visible =true;
            CreateRoom.Visible =true;
            btnPlay.Visible = false;
            btnPass.Visible = false;
            btnCancel.Visible = false;
            btnReady.Visible = false;
            CircleProgress.Visible = false;
        }
        private void MatchForm()
        {
            btnTryAgain.Visible = false;
            EndScreen.Visible = false;
            SearchRoom.Visible = false;
            CreateRoom.Visible = false;
            btnPlay.Visible = true;
            btnPass.Visible = true;
            btnCancel.Visible = false;
            btnReady.Visible = false;
            CircleProgress.Visible = false;
        }
        private void ReadyForm()
        {
            btnTryAgain.Visible = false;
            EndScreen.Visible = false;
            SearchRoom.Visible = false;
            CreateRoom.Visible = false;
            btnPlay.Visible = false;
            btnPass.Visible = false;
            btnCancel.Visible = false;
            btnReady.Visible = true;
            CircleProgress.Visible = false;

        }
        private void WaitingForm()
        {
            btnTryAgain.Visible = false;
            EndScreen.Visible = false;
            SearchRoom.Visible = false;
            CreateRoom.Visible = false;
            btnPlay.Visible = false;
            btnPass.Visible = false;
            btnCancel.Visible = true;
            btnReady.Visible = false;
            CircleProgress.Visible = true;
        }

        private void WiningForm(bool Win)
        {
            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            if (Win)
                EndScreen.BackgroundImage = Image.FromFile(s1 + "Win.png");
            else
                EndScreen.BackgroundImage = Image.FromFile(s1 + "Lose.png");

            EndScreen.FlatAppearance.BorderSize = 0;
            EndScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTryAgain.Visible = true;
            EndScreen.Visible = true;
            SearchRoom.Visible = false;
            CreateRoom.Visible = false;
            btnPlay.Visible = false;
            btnPass.Visible = false;
            btnCancel.Visible = false;
            btnReady.Visible = false;
            CircleProgress.Visible = false;
        }
        private void Ready_Click(object sender, EventArgs e)
        {
            tcpForPlayer.SendData("Ready");
            WaitingForm();
        }

        private void Ready_MouseEnter(object sender, EventArgs e)
        {
            btnReady.Size = new Size(120,115);
            
        }

        private void Ready_MouseLeave(object sender, EventArgs e)
        {
            btnReady.Size = new Size(114, 108);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            btnPlay.Size = new Size(175, 50);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btnPlay.Size = new Size(170, 42);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            btnPass.Size = new Size(175, 50);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btnPass.Size = new Size(170, 42);
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            ReadyForm();

        }

        private void btnTryAgain_MouseEnter(object sender, EventArgs e)
        {
            btnTryAgain.Size = new Size(250, 65);

        }

        private void btnTryAgain_MouseLeave(object sender, EventArgs e)
        {
            btnTryAgain.Size = new Size(230, 53);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tcpForPlayer.SendData("Cancel");
            ReadyForm();
        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (PBCountDown.Maximum == e.ProgressPercentage)
            {
                if (yourturn == true)
                {
                    if (OPlayerCard.Count == 0)
                    {
                        CardChoose.Clear();
                        CardChoose.Add(MyCard.First());
                        tcpForPlayer.SendData("|" + MyCard.First().GetName());
                        MyCard.Remove(MyCard.First());
                        SeperateData(CardChoose, CardHavePlay);
                        SeperateData(MyCard, CardButton);
                    }
                    else
                    {
                        tcpForPlayer.SendData("Skip");
                    }
                    yourturn = false;
                    label2.Text = "";
                    CountDownTimer.Stop();
                    PBCountDown.Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcpForPlayer.SendData("Exit");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CardDown(Button[] b)
        {
            for(int i=0;i<b.Count();i++)
            {
                b[i].Location = new System.Drawing.Point(b[i].Location.X, Horizontal);
            }
        }
        private void FlipDown(int index,Button[] b)
        {
            string currenDirectory = Environment.CurrentDirectory;
            string s1 = currenDirectory.Replace("\\bin\\Debug", "\\Cards\\");
            //try
            //{
            //SortCard(s1);
            for (int i = 0; i < index; i++)
            {
                //Card[i] = new Card(s1[i]);
                //MessageBox.Show(s1[i]);
                b[i].Image = Image.FromFile(s1+"blue_back.png");
            }
        }
        public void DetectServer()
        {
            string ip = "127.0.0.1";
            tcpForChoosingServer = new TCPModel(ip, port);
            tcpForChoosingServer.ConnectToServer();
            string str = tcpForChoosingServer.ReadData();
            port = int.Parse(str);
            Console.WriteLine("Port la: " + port);
        }
    }
}
