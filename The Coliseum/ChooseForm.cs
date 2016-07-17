using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lidgren.Network;
using System.Threading;

namespace The_Coliseum
{
    public partial class ChooseForm : Form
    {
        public Client Client;
        public bool Opened = false;

        public ChooseForm()
        {
            InitializeComponent();
        }

        private void startBut_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.IndexOf(tabControl1.SelectedTab) == 0) //Play
            {
                if (Client != null)
                {
                    Client.Show();
                    Opened = true;
                    Close();
                    Client.Init();
                }
                else
                {
                    MessageBox.Show("You have to connect first");
                }
            }
            else //Host
            {
                Server server = new Server(Convert.ToInt32(turnBox.Text), Convert.ToInt32(pointsBox.Text));
                server.Show();
                Opened = true;
                Close();
            }
        }

        private void connectBut_Click(object sender, EventArgs e)
        {
            Client = new Client(nameBox.Text, characterBox.Text, addressBox.Text);

            try
            {
                NetPeerConfiguration config = new NetPeerConfiguration("The Coliseum");

                Client.NetClient = new NetClient(config);
                Client.NetClient.Start();
                Client.NetClient.Connect(Client.IP, Client.Port);

                Thread.Sleep(1000);

                connectBut.Text = "Connected";
                connectBut.Enabled = false;
                nameBox.ReadOnly = true;
                addressBox.ReadOnly = true;
                characterBox.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void ChooseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Opened)
                Application.Exit();
        }
    }
}
