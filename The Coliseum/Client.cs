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
            readyBut.Invoke(new Action(() => { readyBut.Enabled = !GameReady; }));
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

                        //Reset Actions
                        actionsView.Invoke(new Action(() =>
                        {
                            actionsView.BeginUpdate();
                            actionsView.Nodes.Clear();
                            actionsView.Nodes.Add("Actions");
                            actionsView.EndUpdate();
                        }));
                        
                    }
                    break;
                case ServerMessageSender.IntType.BlockAction:
                    if (value == 1)
                        actionsView.Invoke(new Action(() => { actionsView.Enabled = true; }));
                    else
                        actionsView.Invoke(new Action(() => { actionsView.Enabled = false; }));
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
                    playersBox.Invoke(new Action(() => {
                        playersBox.Items.Add(value);
                        playersBox.Refresh();
                    }));
                    break;
                case ServerMessageSender.StringType.NewLocation:
                    CharacterLocation = value;
                    locationLab.Invoke(new Action(() => { locationLab.Text = "Location: " + value; }));
                    break;
                case ServerMessageSender.StringType.NewAction:
                    PopulateActionsView(value);
                    actionsView.Invoke(new Action(() => { actionsView.Nodes[0].Expand(); }));
                    actionsView.Invoke(new Action(() => { actionsView.Enabled = true; }));
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

        public void PopulateActionsView(string action)
        {
            string path = action.Split(@"|".ToCharArray())[0];
            string toolTip = action.Split(@"|".ToCharArray())[1];
            string code = action.Split(@"|".ToCharArray())[2];

            actionsView.Invoke(new Action(() => { actionsView.BeginUpdate(); }));
            TreeNode root = actionsView.Nodes[0];
            TreeNode node = root;

            foreach (string pathBits in path.Split(@"\".ToCharArray()))
            {
                if (path.Split(@"\".ToCharArray()).ToList().IndexOf(pathBits) != path.Split(@"\".ToCharArray()).Length - 1)
                    node = AddNode(node, pathBits, "", "");
                else
                    node = AddNode(node, pathBits, toolTip, code);
            }
            actionsView.Invoke(new Action(() => { actionsView.EndUpdate(); }));
        }

        private TreeNode AddNode(TreeNode node, string key, string toolTip, string code)
        {
            if (node.Nodes.ContainsKey(key))
            {
                return node.Nodes[key];
            }
            else
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = key;
                newNode.Name = key;
                if (!string.IsNullOrEmpty(toolTip))
                    newNode.Tag = new ActionInfo(toolTip, code);
                actionsView.Invoke(new Action(() => { node.Nodes.Add(newNode); }));
                return newNode;
            }
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

        private void actionsView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                ActionInfo info = (ActionInfo)e.Node.Tag;

                ClientMessageSender.SendString(ServerMessageSender.StringType.NewAction, info.code);
                actionsView.Invoke(new Action(() => { actionsView.Enabled = false; }));
            }
        }

        private void actionsView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (!string.IsNullOrEmpty(((ActionInfo)e.Node.Tag).toolTip))
                {
                    toolTip1.ToolTipTitle = "Tip";
                    toolTip1.Show(((ActionInfo)e.Node.Tag).toolTip, this, e.Node.Bounds.X + e.Node.Bounds.Height / 2, e.Node.Bounds.Y, 500);
                }
            }
        }
    }

    public struct ActionInfo
    {
        public string toolTip;
        public string code;

        public ActionInfo(string a, string b)
        {
            toolTip = a;
            code = b;
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

        public static void SendString(ServerMessageSender.StringType type, string value)
        {
            NetOutgoingMessage msg = Client.MainClient.NetClient.CreateMessage();
            msg.Write((byte)ServerMessageSender.MessageType.String);
            msg.Write((byte)type);
            msg.Write(value);
            Send(msg, 0);
        }

        public static void Send(NetOutgoingMessage msg, int channel)
        {
            Client.MainClient.NetClient.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, channel);
        }
    }
}
