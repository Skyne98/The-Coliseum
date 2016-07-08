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

        //Client
        public NetClient NetClient;
        public Thread NetThread;
        public Thread UpdateThread;

        public string IP;
        public int Port = 54545;

        //Character
        public string PlayerName = "";
        public string CharacterName = "";
        public string CharacterLocation = "";

        //Status
        public int CurrentPlayers = 0;
        public int ReadyPlayers = 0;
        public bool Ready = false;
        public bool GameReady = false;
        public int TurnNumber = 0;

        public Client(string playerName, string characterName, string ip)
        {
            InitializeComponent();
            MainClient = this;
            PlayerName = playerName;
            playerLab.Text = playerName;
            CharacterName = characterName;
            characterLab.Text = characterName;
            IP = ip;
        }

        public void Init()
        {
            ClientMessageSender.SendLogin();
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
            NetThread = new Thread(NetThreadMethod);
            NetThread.Start();
            Log("Network thread started", LogType.Warning);

            UpdateThread = new Thread(UpdateThreadMethod);
            UpdateThread.Start();
            Log("Update thread started", LogType.Warning);
        }

        public void UpdateThreadMethod()
        {
            while (true)
            {
                Thread.Sleep(100);

            }
        }

        public void NetThreadMethod()
        {
            while (true)
            {
                Thread.Sleep(50);
                HandleIncome();
            }
        }

        public void HandleIncome()
        {
            NetIncomingMessage msg;
            while ((msg = NetClient.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        HandleData(msg);
                        break;
                }
                NetClient.Recycle(msg);
            }
        }

        //Data
        public void HandleData(NetIncomingMessage msg)
        {
            ServerMessageSender.MessageType type = (ServerMessageSender.MessageType)msg.ReadByte();
            switch (type)
            {
                case ServerMessageSender.MessageType.Ready:
                    HandleReady(msg);
                    break;
                case ServerMessageSender.MessageType.Info:
                    HandleInfo(msg);
                    break;
                case ServerMessageSender.MessageType.Login:
                    HandleLogin(msg);
                    break;
                case ServerMessageSender.MessageType.Int:
                    HandleInt(msg);
                    break;
                case ServerMessageSender.MessageType.String:
                    HandleString(msg);
                    break;
            }
        }

        public void HandleReady(NetIncomingMessage msg)
        {
            bool ready = msg.ReadBoolean();
            GameReady = ready;
            readyBut.Enabled = !GameReady;
        }

        public void HandleInfo(NetIncomingMessage msg)
        {
            LogType type = (LogType)msg.ReadByte();
            string value = msg.ReadString();

            Log(value, type);
        }

        public void HandleLogin(NetIncomingMessage msg)
        {
            bool succ = msg.ReadBoolean();

            if (succ)
            {

            }
            else
            {
                NetClient.Disconnect("bye");
            }
        }

        public void HandleInt(NetIncomingMessage msg)
        {
            ServerMessageSender.IntType type = (ServerMessageSender.IntType)msg.ReadByte();
            int value = msg.ReadInt32();

            switch (type)
            {
                case ServerMessageSender.IntType.CurrentPlayers:
                    CurrentPlayers = value;
                    ChangeStatus(ReadyPlayers + "/" + CurrentPlayers + " are ready");
                    break;
                case ServerMessageSender.IntType.ReadyPlayers:
                    ReadyPlayers = value;
                    ChangeStatus(ReadyPlayers + "/" + CurrentPlayers + " are ready");
                    break;
                case ServerMessageSender.IntType.TurnPercent:
                    ChangeProgress(value);
                    break;
                case ServerMessageSender.IntType.TurnNumber:
                    if (GameReady)
                    {
                        ChangeStatus("Turn: " + value);
                        TurnNumber = value;
                    }
                    break;
            }
        }

        public void HandleString(NetIncomingMessage msg)
        {
            ServerMessageSender.StringType type = (ServerMessageSender.StringType)msg.ReadByte();
            string value = msg.ReadString();

            switch (type)
            {
                case ServerMessageSender.StringType.NewPlayer:
                    playersBox.Items.Add(value);
                    playersBox.Refresh();
                    break;
                case ServerMessageSender.StringType.NewLocation:
                    CharacterLocation = value;
                    locationLab.Text = "Location: " + value;
                    break;
            }
        }

        public void ChangeStatus(string value)
        {
            if (turnProgress.GetCurrentParent().InvokeRequired)
                turnProgress.GetCurrentParent().Invoke(new Action(() => statusLabel.Text = value));
            else
                statusLabel.Text = value;
        }

        public void ChangeProgress(int value)
        {
            if (turnProgress.GetCurrentParent().InvokeRequired)
                turnProgress.GetCurrentParent().Invoke(new Action(() => turnProgress.Value = value));
            else
                turnProgress.Value = value;
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetThread.Abort();
            UpdateThread.Abort();
            NetClient.Shutdown("bye");
            Application.Exit();
        }

        private void readyBut_Click(object sender, EventArgs e)
        {
            Ready = !Ready;

            if (Ready)
                readyBut.Text = "Ready";
            else
                readyBut.Text = "Not Ready";

            ClientMessageSender.SendReady(Ready);
        }
    }

    public static class ClientMessageSender
    {
        public static void SendLogin()
        {
            NetOutgoingMessage msg = Client.MainClient.NetClient.CreateMessage();
            msg.Write((byte)ServerMessageSender.MessageType.Login);
            msg.Write(Client.MainClient.PlayerName);
            msg.Write(Client.MainClient.CharacterName);
            Send(msg, 0);
        }

        public static void SendReady(bool ready)
        {
            NetOutgoingMessage msg = Client.MainClient.NetClient.CreateMessage();
            msg.Write((byte)ServerMessageSender.MessageType.Ready);
            msg.Write(ready);
            Send(msg, 0);
        }

        public static void Send(NetOutgoingMessage msg, int channel)
        {
            Client.MainClient.NetClient.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, channel);
        }
    }
}
