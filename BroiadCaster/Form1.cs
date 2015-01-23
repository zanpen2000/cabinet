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

        public void ReturnClients(IEnumerable<ISubscriber> clientMacs)
        {
            Invoke((Action)delegate
            {
                listBox1.DataSource = clientMacs;
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



        public void ReturnOnlineResult(string mac, OnlineState state)
        {
            throw new NotImplementedException();
        }

        public void ReturnOfflineResult(string mac, OnlineState state)
        {
            throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServiceProxy.Call<IMainService>(n =>
            {
                textBox3.Text = n.Test(textBox3.Text);
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var clients = textBox2.Text.Split(new char[] { ',', ' ' });

            Random r = new Random();
            int times = int.Parse(textBox4.Text);
            string msg = textBox1.Text;
            ServiceProxy.Call<IDuplexChannelService, IDuplexChannelCallback>(this, net =>
            {
                List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

                int c = 0;

                for (int i = 0; i < times; i++)
                {
                    int j = r.Next(900);
                    int t = i;

                    System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(() =>
                    {
                        System.Threading.Thread.Sleep(j);
                        net.Broadcast(clients, j.ToString() + "\t" + t.ToString());


                    });
                    tasks.Add(task);
                }

                tasks.AsParallel().ForAll(t =>
                {
                    t.Start();
                });
            });
        }




    }
}
