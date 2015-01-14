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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            var clients = textBox2.Text.Split(new char[] { ',', ' ' });

            ServiceCaller.Execute<IPublishService>(callback_OnPublish, net =>
            {
                foreach (var item in clients.ToArray())
                {
                    net.Broadcast(item, textBox1.Text);
                }
            });
        }

        void callback_OnPublish(object sender, SubscriberCallbackEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                SubscriberCallback cb = new SubscriberCallback();
                cb.OnReturnSubscribers += cb_OnReturnSubscribers;

                InstanceContext ins = new InstanceContext(cb);
                ServiceCaller.Execute<IPublishService>(ins, net =>
                {
                    net.GetSubscribers();
                });
            });
        }

        void cb_OnReturnSubscribers(object sender, SubscribersCallbackEventArgs e)
        {
            Invoke((Action)delegate
            {
                this.listBox1.DataSource = e.Subscribers;
            });
        }
        public void Publish(string mac, string message)
        {

        }

        public void ReturnRegis(string mac, string msg)
        {
            //throw new NotImplementedException();
        }

        public void ReturnUnregis(string mac, string msg)
        {
            //throw new NotImplementedException();
        }

        public void ReturnSubscribers(IEnumerable<ISubscriber> subscriberMacs)
        {

        }
    }
}
