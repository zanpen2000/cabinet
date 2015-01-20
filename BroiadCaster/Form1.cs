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

namespace BroiadCaster
{
    public partial class Form1 : Form, IDuplexChannelCallback
    {
        public Form1()
        {
            InitializeComponent();
    
        }

        void cb_OnClientsReturn(object sender, ClientsReturnEventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var clients = textBox2.Text.Split(new char[] { ',', ' ' });

            //ServiceCaller.Execute<IPublishService>(callback_OnPublish, net =>
            //{
            //    foreach (var item in clients.ToArray())
            //    {
            //        net.Broadcast(item, textBox1.Text);
            //    }
            //});
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    SubscriberCallback cb = new SubscriberCallback();
            //    cb.OnReturnSubscribers += cb_OnReturnSubscribers;

            //    InstanceContext ins = new InstanceContext(cb);
            //    ServiceCaller.Execute<IPublishService>(ins, net =>
            //    {
            //        net.GetSubscribers();
            //    });
            //});
        }

     


        public void ReturnSubscribers(IEnumerable<string> subscriberMacs)
        {
            Invoke((Action)delegate
            {
                this.listBox1.DataSource = subscriberMacs;
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
            throw new NotImplementedException();
        }

        public void ReturnOnlineResult(OnlineState state)
        {
            throw new NotImplementedException();
        }

        public void ReturnOfflineResult(OnlineState state)
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
    }
}
