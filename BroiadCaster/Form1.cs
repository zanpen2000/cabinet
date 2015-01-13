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



    }
}
