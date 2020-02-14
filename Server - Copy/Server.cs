using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    public partial class Server : Form
    {
        private TCPModel tcp;
        private object thislock;
        private Room a = new Room();
        private List<Room1> Room = new List<Room1>();
        private Thread ListenderThread;
        public Server()
        {
            InitializeComponent();
            
        }
        void MainFormLoad(object sender, EventArgs e)
        {
            this.Text = "SERVER 2";
            CheckForIllegalCrossThreadCalls = false;
            thislock = new object();
        }
        void Button1Click(object sender, EventArgs e)
        {
            StartServer();
            ListenderThread = new Thread(ServeClients);
            ListenderThread.IsBackground = true;
            ListenderThread.Start();
        }
        public void StartServer()
        {
            string ip = textBox1.Text;
            int port = int.Parse(textBox2.Text);
            tcp = new TCPModel(ip, port);
            tcp.Listen();
            button1.Enabled = false;
        }
        public void ServeClients()
        {
            SocketModel sm;
            while (true)
            {
                sm = Accept();
                Thread t = new Thread(Commmunication);
                t.IsBackground = true;
                t.Start(sm);
            }
        }
        public SocketModel Accept()
        {
            int status = -1;
            Socket s = tcp.SetUpANewConnection(ref status);
            SocketModel player = new SocketModel(s);
            //Room.Add(player);
            //socketList[currentClient] = new SocketModel(s);
            //string str = socketList[currentClient].GetRemoteEndpoint();
            string str = player.GetRemoteEndpoint();
            string str1 = "New connection from: " + str + "\n";
            textBox3.AppendText(str1);
            return player;
        }
        public void Commmunication(object obj)
        {
            int RoomIndex = -1;
            SocketModel sm = (SocketModel)obj;
            try
            {
                while (sm.IsAlive())
                {
                    string str = sm.ReceiveData();
                    if (str.Length == 0)
                    {
                        sm.CloseSocket();
                        return;
                    }
                    switch (str)
                    {
                        case "Search":
                            RoomIndex = RoomSearching(sm);
                            if (RoomIndex == -1)
                                RoomIndex = RoomCreate(sm);
                            if (RoomIndex != -1)
                                sm.SendData("RoomFound|"+RoomIndex);
                            break;
                        case "Ready":
                            if (RoomIndex != -1) 
                                Room[RoomIndex].Ready(sm);
                            break;
                        case "Cancel":
                            if (RoomIndex != -1)
                                Room[RoomIndex].Cancel(sm);
                            break;
                        case "Create":
                            RoomIndex = SearchEmtyRoom(sm);
                            if (RoomIndex == -1)
                                RoomIndex = RoomCreate(sm);
                            if (RoomIndex != -1)
                            sm.SendData("CreateRoomSuccessful|" + RoomIndex);
                            break;
                        case "Exit":
                            if (RoomIndex != -1)
                            { Room[RoomIndex].Remote(sm);
                                RoomIndex = -1;
                                sm.SendData("Exit");
                            }
                            break;
                        default:
                            if (RoomIndex != -1)
                                Room[RoomIndex].TransData(sm, str);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace.ToString());
                    sm.CloseSocket();
                } 
        }
        public int RoomSearching(SocketModel sm)
        {
            for(int i=0;i<Room.Count;i++)
            {
                if (Room[i].CanJoin())
                {
                    Room[i].Add(sm);
                    Console.WriteLine(i);
                    return i;
                }
            }
            Console.WriteLine(-1);
            return -1;
        }

        public int SearchEmtyRoom(SocketModel sm)
        {
            for(int i=0;i<Room.Count;i++)
            {
                if (Room[i].NumOfPlayer() == 0)
                {
                    Room[i].Add(sm);
                    Console.WriteLine(i);
                    return i;
                }
            }
            Console.WriteLine(-1);
            return -1;
        }
        public int RoomCreate(SocketModel sm)
        {
            Room.Add(new Room1(sm));
            return Room.Count - 1;
        }
    }
}
