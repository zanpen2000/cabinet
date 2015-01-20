using Lib.Librarys;
using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lib.Layer
{

    /// <summary>
    /// 订阅者容器
    /// </summary>
    public class SubscriberContainer
    {
        #region 单例实现
        private static readonly object _syncLock = new object();
        private static SubscriberContainer _instance;

        public static SubscriberContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SubscriberContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        private SubscriberContainer()
        {

        }



        #endregion

        public List<ISubscriber> Subscribers { get { return _subscribers; } }

        public event EventHandler<SubscriberMessageEventArgs> SubscriberAdded;
        public event EventHandler<SubscriberMessageEventArgs> SubscriberRemoved;
        public event EventHandler<MessageNotifyErrorEventArgs> NotifyError;

        private List<ISubscriber> _subscribers = new List<ISubscriber>(0);

        /// <summary>
        /// 根据运行机制决定是否允许同一个客户端订阅多次
        /// </summary>
        public bool AllowClientMultipleRegistration
        {
            get
            {
                bool allow = true;
                try
                {
                    string allowstr = AppSettings.Get("AllowClientMultipleRegistration");
                    Boolean.TryParse(allowstr.ToString(), out allow);
                }
                catch { }
                return allow;
            }
        }


        public void AddSubscriber(ISubscriber listener)
        {
            lock (_syncLock)
            {
                if (!Exists(listener))
                {
                    _subscribers.Add(listener);
                    if (SubscriberAdded != null)
                    {
                        this.SubscriberAdded(this, new SubscriberMessageEventArgs(listener));
                    }
                }
                else if (MacExists(listener.Mac))
                {
                    //update container
                    var sub = _subscribers.Find(ps => ps.Mac == listener.Mac);
                    _subscribers.Remove(sub);
                    _subscribers.Add(listener);
                }
                else
                {
                    //重复注册订阅者
                }
            }
        }

        private bool MacExists(string Mac)
        {
            return _subscribers.Exists(s => s.Mac == Mac);
        }

        public bool Exists(ISubscriber sub)
        {
            return _subscribers.Exists(s => s.Mac == sub.Mac && s.IP == sub.IP && s.Port == sub.Port);
        }

        public void RemoveSubscriber(ISubscriber listener)
        {
            lock (_syncLock)
            {
                if (_subscribers.Count(x => x.Mac == listener.Mac) > 0)
                {
                    this._subscribers.RemoveAll(x => x.Mac == listener.Mac);
                }
            }
            if (SubscriberRemoved != null)
            {
                this.SubscriberRemoved(this, new SubscriberMessageEventArgs(listener));
            }
        }

        public void NotifyMessage(string message)
        {
            ISubscriber[] listeners = _subscribers.ToArray();
            foreach (ISubscriber lstn in listeners)
            {
                try
                {
                    lstn.Notify(message);
                }
                catch (Exception ex)
                {
                    OnNotifyError(lstn, ex);
                }
            }
        }


        public void NotifyMessage(string clientMac, string message)
        {
            ISubscriber[] listeners = _subscribers.ToArray();
            foreach (ISubscriber lstn in listeners.Where(l => l.Mac == clientMac))
            {
                try
                {
                    lstn.Notify(message);
                }
                catch (Exception ex)
                {
                    OnNotifyError(lstn, ex);
                }
            }
        }

        public void NotifyMessage(IEnumerable<string> clientMacs, string message)
        {
            ISubscriber[] listeners = _subscribers.ToArray();

            foreach (ISubscriber lstn in listeners)
            {
                if (clientMacs.Contains(lstn.Mac))
                {
                    try
                    {
                        lstn.Notify(message);
                    }
                    catch (Exception ex)
                    {
                        OnNotifyError(lstn, ex);
                    }
                }
            }
        }


        private void OnNotifyError(ISubscriber subscriber, Exception ex)
        {
            if (this.NotifyError == null)
            {
                return;
            }

            MessageNotifyErrorEventArgs args = new MessageNotifyErrorEventArgs(subscriber, ex);
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object state)
            {
                this.NotifyError(this, state as MessageNotifyErrorEventArgs);
            }, args);
        }
    }
}
