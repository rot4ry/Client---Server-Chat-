using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private SimpleTcpServer _SERVER;
        
        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            _SERVER = new SimpleTcpServer();
            _SERVER.Delimiter = 0x13;
            _SERVER.StringEncoder = Encoding.UTF8;
            _SERVER.DataReceived += _SERVER_DataReceived;
        }

        private void _SERVER_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {

                txtStatus.Text += new StringBuilder().Append("\n").Append(e.MessageString).ToString();
                string formattedMessage = new StringBuilder().Append("\n").Append(e.MessageString).ToString();
                _SERVER.BroadcastLine(formattedMessage);
            });
        }
                

        private void startServerButton_Click(object sender, EventArgs e)
        {
            int ip;
            Int32.TryParse(ipBox.Text, out ip);
            System.Net.IPAddress ip_address = new System.Net.IPAddress(ip);

            int port;
            Int32.TryParse(portBox.Text, out port);

            _SERVER.Start(ip_address, port);
        }

        private void stopServerButton_Click(object sender, EventArgs e)
        {
            if (_SERVER.IsStarted)
            {
                _SERVER.Stop();
            }
        }
    }
}
