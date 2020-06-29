using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace Client
{
    public partial class Client : Form
    {
        private SimpleTcpClient _CLIENT;
        public Client()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;

            
            int port = Int32.Parse(portBox.Text);

            _CLIENT.Connect(ipBox.Text.ToString(), port);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            _CLIENT = new SimpleTcpClient();
            _CLIENT.StringEncoder = Encoding.UTF8;
            _CLIENT.DataReceived += _CLIENT_DataReceived;
        }

        private void _CLIENT_DataReceived(object sender, SimpleTCP.Message e)
        {
            sentMessagesBox.Invoke((MethodInvoker)delegate ()
            {
                sentMessagesBox.Text += e.MessageString;
            });
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            _CLIENT.WriteLineAndGetReply(messageBox.Text, TimeSpan.FromSeconds(1));
            messageBox.Text = "";
        }
    }
}
