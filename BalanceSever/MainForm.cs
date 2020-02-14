/*
 * Created by SharpDevelop.
 * User: chepchip
 * Date: 11/11/2016
 * Time: 11:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Game_Xuc_xac
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			this.Text = "ROOT SERVER";
			CheckForIllegalCrossThreadCalls = false;			
		}
		
		private TCPModel tcp;
		private SocketModel[] socketList;
		private int numberOfPlayers = 200;
		private int currentClient;
		private Object thisLock = new Object();
		
		public void StartServer(){
			string ip = textBox1.Text;
			int port = int.Parse(textBox2.Text);
			tcp = new TCPModel(ip,port);
			tcp.Listen();
			button1.Enabled = false;
		}
		public void ServeClients(){
			socketList = new SocketModel[numberOfPlayers];
			for (int i = 0;i < numberOfPlayers;i ++){
				ServeAClient();
			}
		}
		public void Accept(){
			int status = -1;
			Socket s = tcp.SetUpANewConnection(ref status);	
			socketList[currentClient] = new SocketModel(s);
			string str = socketList[currentClient].GetRemoteEndpoint();
			string str1 = "New connection from: "+ str + "\n";
			textBox3.AppendText(str1);		
		}
		public void ServeAClient(){
			int num = -1;
			lock (thisLock){
				Accept();
				currentClient ++;	
				num = currentClient-1;				
			}
			Commmunication(num);
		}
		private int balance = 1;
		public void Commmunication(int num){
			string str = "";
			if ((balance == 1) || (balance == 2))
				str = "13000";				
			if ((balance == 3) || (balance == 4))
				str = "14000";
			balance ++;
			Console.WriteLine("Balance la: " + balance);
			if (balance == 5)
				balance = 1;
			socketList[num].SendData(str);			
		}

		void Button1Click(object sender, EventArgs e)
		{
			StartServer();
			Thread t = new Thread(ServeClients);
			t.Start();
		}
	}
}
