using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Coliseum
{
    public partial class Client : Form
    {
        //Statics
        public static Client MainClient;
        public enum LogType
        {
            Common,
            Error,
            Warning,
            Network
        }

        //Server
        public NetClient NetClient;
        public Thread NetThread;
        public Thread UpdateThread;

        public string IP;
        public int Port = 54545;

        //Character
        public string PlayerName = "";
        public string CharacterName = "";

        public Client(string playerName, string characterName, string ip)
        {
            InitializeComponent();
            MainClient = this;
            PlayerName = playerName;
            CharacterName = characterName;
            IP = ip;

            Init();
        }

        public void Init()
        {
            StartNetworking();
            StartThreading();
        }

        public static void Log(string message, LogType type)
        {
            Color color = Color.Black;

            switch (type)
            {
                case LogType.Error:
                    color = Color.Red;
                    break;
                case LogType.Network:
                    color = Color.Blue;
                    break;
                case LogType.Warning:
                    color = Color.YellowGreen;
                    break;
            }

            string time = " [" + DateTime.Now.ToShortTimeString() + "] ";

            if (MainClient.logBox.InvokeRequired)
            {
                MainClient.logBox.Invoke(new Action(() =>
                MainClient.logBox.AppendText(time)));
                MainClient.logBox.Invoke(new Action(() =>
                MainClient.logBox.Select(MainClient.logBox.Text.Length - 1, time.Length)));
                MainClient.logBox.Invoke(new Action(() =>
                MainClient.logBox.SelectionColor = color));
                MainClient.logBox.Invoke(new Action(() =>
                MainClient.logBox.AppendText(message + Environment.NewLine)));

            }
            else
            {
                MainClient.logBox.AppendText(time);
                MainClient.logBox.Select(MainClient.logBox.Text.Length - 1, time.Length);
                MainClient.logBox.SelectionColor = color;
                MainClient.logBox.AppendText(message + Environment.NewLine);
            }
        }

        public void StartThreading()
        {
            
        }

        public void StartNetworking()
        {
            try
            {
                NetPeerConfiguration config = new NetPeerConfiguration("The Coliseum");

                NetClient = new NetClient(config);
                NetClient.Start();
                NetClient.Connect(IP, Port);

                Log("Server successfully started", LogType.Network);
                Log("Port: " + NetServer.Configuration.Port, LogType.Network);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
