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

namespace The_Coliseum
{
    public partial class ChooseForm : Form
    {
        public ChooseForm()
        {
            InitializeComponent();
        }

        private void startBut_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.IndexOf(tabControl1.SelectedTab) == 0) //Play
            {
                Client client = new Client();
                client.Show();
                Close();
            }
            else //Host
            {
                Server server = new Server(Convert.ToInt32(turnBox.Text), Convert.ToInt32(pointsBox.Text));
                server.Show();
                Close();
            }
        }
    }
}
