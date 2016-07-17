using Lidgren.Network;
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
using System.Net;

namespace The_Coliseum
{
    public partial class Server : Form
    {
        //Statics
        public static Server MainServer;

        public enum LogType
        {
            Common,
            Error,
            Warning,
            Network
        }

        //Server
        public NetServer NetServer;
        public Thread NetThread;
        public Thread UpdateThread;
        public Game Game;

        //Timers
        DateTime prevTime = DateTime.Now;
        public int TurnTime = 30;
        public int TurnNumber = 0;

        //Ready
        public int TimeToStart = 5;
        public int ReadyTimer = 0;

        //Characters
        public int AttPoints = 0;
        public Character SelectedCharacter;

        //Actions
        public List<Character> ReadyCharacters = new List<Character>();
             
        public Server(int turnTime, int attPoints)
        {
            InitializeComponent();

            TurnTime = turnTime;
            AttPoints = attPoints;
            MainServer = this;
            turnProgress.Maximum = TurnTime;

            Init();
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
                    color = Color.Orange;
                    break;
            }

            string time = " [" + DateTime.Now.ToShortTimeString() + "] ";

            if (MainServer.logBox.InvokeRequired)
            {
                MainServer.logBox.Invoke(new Action(() =>
                MainServer.logBox.AppendText(time)));
                MainServer.logBox.Invoke(new Action(() =>
                MainServer.logBox.Select(MainServer.logBox.Text.Length - 1, time.Length)));
                MainServer.logBox.Invoke(new Action(() =>
                MainServer.logBox.SelectionColor = color));
                MainServer.logBox.Invoke(new Action(() =>
                MainServer.logBox.AppendText(message + Environment.NewLine)));

            }
            else
            {
                MainServer.logBox.AppendText(time);
                MainServer.logBox.Select(MainServer.logBox.Text.Length - 1, time.Length);
                MainServer.logBox.SelectionColor = color;
                MainServer.logBox.AppendText(message + Environment.NewLine);
            }
        }

        public void Init()
        {
            StartNetworking();
            StartThreading();

            Game = new Game();
        }

        public void StartNetworking()
        {
            try
            {
                NetPeerConfiguration config = new NetPeerConfiguration("The Coliseum");
                config.Port = 54545;
                config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
                config.MaximumConnections = 512;

                NetServer = new NetServer(config);
                NetServer.Start();

                //Look for IP
                string externalip = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                externalip = "Address: " + externalip + ":" + NetServer.Configuration.Port;
                ipLabel.Text = externalip;

                Log("Server successfully started", LogType.Network);
                Log("Port: " + NetServer.Configuration.Port, LogType.Network);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
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

        public void NetThreadMethod()
        {
            while (true)
            {
                Thread.Sleep(50);
                HandleIncome();
            }
        }

        public void UpdateThreadMethod()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Tick();
            }
        }

        public void Tick()
        {
            if (Game.Started)
            {
                int diff = (DateTime.Now - prevTime).Seconds;
                if (diff > TurnTime)
                    diff = TurnTime;

                ServerMessageSender.SendInt(ServerMessageSender.IntType.TurnPercent, (int)(((float)diff / (float)TurnTime) * 100), Server.MainServer.Game.Characters);

                if (turnProgress.GetCurrentParent().InvokeRequired)
                    turnProgress.GetCurrentParent().Invoke(new Action(() => turnProgress.Value = diff));
                else
                    turnProgress.Value = diff;

                if (diff == TurnTime)
                    MakeTurn();
            }
            else
            {
                if (Game.GetReadyPlayers() > Game.Characters.Count / 2)
                {
                    ReadyTimer++;

                    if (ReadyTimer < TimeToStart)
                        ServerMessageSender.SendInfo(LogType.Warning, "Game starts in " + (TimeToStart - ReadyTimer) + " seconds", Game.Characters);
                    else if (!Game.Started)
                    {
                        Game.Started = true;

                        //Set Players Locations
                        foreach (var character in Game.Characters)
                        {
                            character.SetLocation(The_Coliseum.Location.Find("Start"));
                        }
                        ServerMessageSender.SendReady(true, Game.Characters);
                    }
                } 
                else
                    ReadyTimer = 0;
            }
        }

        public void MakeTurn()
        {
            prevTime = DateTime.Now;
            TurnNumber++;
            ServerMessageSender.SendInt(ServerMessageSender.IntType.TurnNumber, TurnNumber, Game.Characters);

            if (turnProgress.GetCurrentParent().InvokeRequired)
                turnProgress.GetCurrentParent().Invoke(new Action(() => turnLabel.Text = "Turn: " + TurnNumber));
            else
                turnLabel.Text = "Turn: " + TurnNumber;

            //Process Ready Characters
            foreach (var character in ReadyCharacters)
            {
                character.ActionsSystem.ProcessCode(character.ActionsSystem.CurrentActionCode);
            }

            foreach (var character in Game.Characters)
            {
                //Send New Actions
                character.ActionsSystem.GetActions();
                character.ActionsSystem.SendActions();
            }

            ReadyCharacters = new List<Character>();
        }

        public void HandleIncome()
        {
            NetIncomingMessage msg;
            while ((msg = NetServer.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        HandleApproval(msg);
                        break;
                    case NetIncomingMessageType.Data:
                        HandleData(msg);
                        break;
                }
                NetServer.Recycle(msg);
            }
        }

        public void HandleApproval(NetIncomingMessage msg)
        {
            msg.SenderConnection.Approve();
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
                case ServerMessageSender.MessageType.Login:
                    HandleLogin(msg);
                    break;
                case ServerMessageSender.MessageType.String:
                    HandleString(msg);
                    break;
            }
        }

        public void HandleReady(NetIncomingMessage msg)
        {
            bool ready = msg.ReadBoolean();
            Character character = ServerMessageSender.GetCharacterFromMessage(msg);

            if (ready)
            {
                character.Ready = true;
                Log(character.PlayerName + " is ready", LogType.Common);
                ServerMessageSender.SendInfo(LogType.Common, character.PlayerName + " is ready", Server.MainServer.Game.Characters);
                ServerMessageSender.SendInt(ServerMessageSender.IntType.ReadyPlayers, Server.MainServer.Game.GetReadyPlayers(), Server.MainServer.Game.Characters);
            }
            else
            {
                character.Ready = false;
                Log(character.PlayerName + " is no longer ready", LogType.Common);
                ServerMessageSender.SendInfo(LogType.Common, character.PlayerName + " is no longer ready", Server.MainServer.Game.Characters);
                ServerMessageSender.SendInt(ServerMessageSender.IntType.ReadyPlayers, Server.MainServer.Game.GetReadyPlayers(), Server.MainServer.Game.Characters);
            }
        }

        public void HandleLogin(NetIncomingMessage msg)
        {
            string playerName = msg.ReadString();
            string charName = msg.ReadString();

            if (Game.Characters.Find(a => a.PlayerName == playerName) != null)
            {
                if (Game.Characters.Find(a => a.PlayerName == playerName).Name == charName)
                {
                    //Log in New Player to Exitsting Character Slot
                    Character character = Game.Characters.Find(a => a.PlayerName == playerName);
                    character.Connection = msg.SenderConnection;
                    character.Connection.Approve();
                    ServerMessageSender.SendInfo(LogType.Common, "Joined the game", character);
                    Log(character.PlayerName + " joined the game", LogType.Common);
                    ServerMessageSender.SendLogin(true, character);

                    //Send Info About Users
                    ServerMessageSender.SendInt(ServerMessageSender.IntType.CurrentPlayers, Game.Characters.Count, Game.Characters);
                    ServerMessageSender.SendInt(ServerMessageSender.IntType.ReadyPlayers, Game.GetReadyPlayers(), Game.Characters);

                    foreach (Character ichar in Game.Characters)
                    {
                        if (ichar != character)
                            ServerMessageSender.SendString(ServerMessageSender.StringType.NewPlayer, ichar.PlayerName, character);
                    }

                    //Send About Game
                    ServerMessageSender.SendReady(Game.Started, character);
                    ServerMessageSender.SendInt(ServerMessageSender.IntType.TurnNumber, TurnNumber, character);
                    character.ActionsSystem.SendActions();
                    if (ReadyCharacters.Contains(character))
                        ServerMessageSender.SendInt(ServerMessageSender.IntType.BlockAction, 1, character);
                    ServerMessageSender.SendString(ServerMessageSender.StringType.NewLocation, character.Location.Name, character);
                }
                else
                {
                    msg.SenderConnection.Approve();
                    ServerMessageSender.SendInfo(LogType.Common, "Wrong character name", msg.SenderConnection);
                    Log(playerName + " tried to log in with wrong character name", LogType.Common);
                    ServerMessageSender.SendLogin(false, msg.SenderConnection);
                }
            }
            else if (!Game.Started)
            {
                //Create New Character / Register New Player
                Character character = new Character();
                character.Name = charName;
                character.PlayerName = playerName;
                character.Connection = msg.SenderConnection;
                character.Connection.Approve();
                Game.Characters.Add(character);

                UpdateCharacterList();
                ServerMessageSender.SendInfo(LogType.Common, character.PlayerName + " joined the game", Game.Characters);
                ServerMessageSender.SendString(ServerMessageSender.StringType.NewPlayer, character.PlayerName, Game.Characters);
                Log(character.PlayerName + " joined the game", LogType.Common);
                ServerMessageSender.SendLogin(true, character);

                //Send Info About Users
                ServerMessageSender.SendInt(ServerMessageSender.IntType.CurrentPlayers, Game.Characters.Count, Game.Characters);
                ServerMessageSender.SendInt(ServerMessageSender.IntType.ReadyPlayers, Game.GetReadyPlayers(), Game.Characters);

                foreach (Character ichar in Game.Characters)
                {
                    if (ichar != character)
                        ServerMessageSender.SendString(ServerMessageSender.StringType.NewPlayer, ichar.PlayerName, character);
                }
            }
            else if (Game.Started)
            {
                msg.SenderConnection.Approve();
                ServerMessageSender.SendInfo(LogType.Common, "Sorry, but the game has already started", msg.SenderConnection);
                Log(playerName + " tried to join the game, but it has already started", LogType.Common);
                ServerMessageSender.SendLogin(false, msg.SenderConnection);
            }
        }

        public void HandleString(NetIncomingMessage msg)
        {
            ServerMessageSender.StringType type = (ServerMessageSender.StringType)msg.ReadByte();
            string value = msg.ReadString();

            switch (type)
            {
                case ServerMessageSender.StringType.NewAction:
                    Character character = ServerMessageSender.GetCharacterFromMessage(msg);

                    character.ActionsSystem.CurrentActionCode = value;
                    ReadyCharacters.Add(character);
                    ReadyCharacters.OrderBy(a => a.ActionsSystem.CurrentActionPriority);
                    break;
            }
        }

        public void UpdateCharacterList()
        {
            ListBox box = charactersList;

            box.Invoke(new Action(() => { box.Items.Clear(); }));

            foreach (Character character in Game.Characters)
            {
                box.Invoke(new Action(() => { box.Items.Add(character.Name); }));
            }

            box.Invoke(new Action(() => { box.Refresh(); }));
        }

        public void UpdateCharacter(Character character)
        {
            playerNameBox.Invoke(new Action(() => {
                playerNameBox.Text = character.PlayerName;
                characterNameBox.Text = character.Name;

                if (character.Location != null)
                    characterLocationBox.Text = character.Location.Name;
                else
                    characterLocationBox.Text = "No location";

                attributesBox.BeginUpdate();
                attributesBox.Items.Clear();
                attributesBox.Items.Add($"Luck: {character.AttLuck}");
                attributesBox.EndUpdate();
            }));
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetThread.Abort();
            UpdateThread.Abort();
            NetServer.Shutdown("bye");
            Application.Exit();
        }

        private void charactersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Character character = Game.Characters[charactersList.SelectedIndex];
            SelectedCharacter = character;
            UpdateCharacter(SelectedCharacter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedCharacter != null)
            {
                UpdateCharacter(SelectedCharacter);
            }
        }
    }

    public static class ServerMessageSender
    {
        public enum MessageType
        {
            Info,
            Login,
            Ready,
            Chat,
            Int,
            String
        }

        public enum IntType
        {
            CurrentPlayers,
            ReadyPlayers,
            TurnPercent,
            TurnNumber,
            BlockAction
        }

        public enum StringType
        {
            NewPlayer,
            NewLocation,
            NewAction
        }

        public enum ChatType
        {
            Global,
            Local
        }

        public static void SendString(StringType type, string value, Character character)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.String);
            msg.Write((byte)type);
            msg.Write(value);
            SendToCharacter(msg, character, 0);
        }

        public static void SendString(StringType type, string value, NetConnection connection)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.String);
            msg.Write((byte)type);
            msg.Write(value);
            SendToConnection(msg, connection, 0);
        }

        public static void SendString(StringType type, string value, List<Character> characters)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.String);
            msg.Write((byte)type);
            msg.Write(value);
            SendToCharacters(msg, characters, 0);
        }

        public static void SendInt(IntType type, int value, Character character)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Int);
            msg.Write((byte)type);
            msg.Write(value);
            SendToCharacter(msg, character, 0);
        }

        public static void SendInt(IntType type, int value, NetConnection connection)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Int);
            msg.Write((byte)type);
            msg.Write(value);
            SendToConnection(msg, connection, 0);
        }

        public static void SendInt(IntType type, int value, List<Character> characters)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Int);
            msg.Write((byte)type);
            msg.Write(value);
            SendToCharacters(msg, characters, 0);
        }

        public static void SendLogin(bool succ, Character character)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Login);
            msg.Write(succ);
            SendToCharacter(msg, character, 0);
        }

        public static void SendLogin(bool succ, NetConnection connection)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Login);
            msg.Write(succ);
            SendToConnection(msg, connection, 0);
        }

        public static void SendLogin(bool succ, List<Character> characters)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Login);
            msg.Write(succ);
            SendToCharacters(msg, characters, 0);
        }

        public static void SendReady(bool ready, Character character)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Ready);
            msg.Write(ready);
            SendToCharacter(msg, character, 0);
        }

        public static void SendReady(bool ready, NetConnection connection)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Ready);
            msg.Write(ready);
            SendToConnection(msg, connection, 0);
        }

        public static void SendReady(bool ready, List<Character> characters)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Ready);
            msg.Write(ready);
            SendToCharacters(msg, characters, 0);
        }

        public static void SendInfo(Server.LogType type, string message, Character character)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Info);
            msg.Write((byte)type);
            msg.Write(message);
            SendToCharacter(msg, character, 0);
        }

        public static void SendInfo(Server.LogType type, string message, NetConnection connection)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Info);
            msg.Write((byte)type);
            msg.Write(message);
            SendToConnection(msg, connection, 0);
        }

        public static void SendInfo(Server.LogType type, string message, List<Character> characters)
        {
            NetOutgoingMessage msg = Server.MainServer.NetServer.CreateMessage();
            msg.Write((byte)MessageType.Info);
            msg.Write((byte)type);
            msg.Write(message);
            SendToCharacters(msg, characters, 0);
        }

        public static void SendToCharacter(NetOutgoingMessage msg, Character character, int channel)
        {
            SendToConnection(msg, character.Connection, channel);
        }

        public static void SendToCharacters(NetOutgoingMessage msg, List<Character> characters, int channel)
        {
            List<NetConnection> list = new List<NetConnection>();

            foreach (Character character in characters)
            {
                list.Add(character.Connection);
            }

            SendToConnections(msg, list, channel);
        }

        public static void SendToConnection(NetOutgoingMessage msg, NetConnection connection, int channel)
        {
            Server.MainServer.NetServer.SendMessage(msg, connection, NetDeliveryMethod.ReliableOrdered, channel);
        }

        public static void SendToConnections(NetOutgoingMessage msg, List<NetConnection> connections, int channel)
        {
            if (connections.Count > 0)
                Server.MainServer.NetServer.SendMessage(msg, connections, NetDeliveryMethod.ReliableOrdered, channel);
        }

        public static Character GetCharacterFromMessage(NetIncomingMessage msg)
        {
            return Server.MainServer.Game.Characters.Find(a => a.Connection == msg.SenderConnection);
        }
    }
}
