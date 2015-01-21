using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Lib.Layer;
using Lib.ServiceContracts;
using System.ServiceModel;
using Lib.Layer.Client;

namespace BroiadCaster
{
    public partial class Form1 : Form, IDuplexChannelCallback
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            var clients = textBox2.Text.Split(new char[] { ',', ' ' });

            ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(this, net =>
            {
                net.Broadcast(clients, textBox1.Text);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(this, net =>
                {
                    net.GetClients();
                });
            });
        }

        public void Broadcast(string message)
        {
            throw new NotImplementedException();
        }

        public void ReturnHeartBeat(byte b)
        {
            throw new NotImplementedException();
        }

        public void ReturnClients(IEnumerable<string> clientMacs)
        {
            Invoke((Action)delegate
            {
                listBox1.DataSource = clientMacs;
            });
        }

        public void ReturnOnlineResult(string mac, OnlineState state)
        {
            throw new NotImplementedException();
        }

        public void ReturnOfflineResult(string mac, OnlineState state)
        {
            throw new NotImplementedException();
        }
    }
}
