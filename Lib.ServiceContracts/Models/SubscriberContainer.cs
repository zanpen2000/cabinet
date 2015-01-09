using Lib.Librarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceContracts
{
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

        public List<Subscriber> Subscribers { get { return _subscribers; } }

        public event EventHandler<SubscriberMessageEventArgs> SubscriberAdded;
        public event EventHandler<SubscriberMessageEventArgs> SubscriberRemoved;
        public event EventHandler<MessageNotifyErrorEventArgs> NotifyError;

        private List<Subscriber> _subscribers = new List<Subscriber>(0);

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


        public void AddSubscriber(Subscriber listener)
        {
            lock (_syncLock)
            {
                if (_subscribers.Count(x => x.ClientMacAddress == listener.ClientMacAddress) > 0 && !AllowClientMultipleRegistration)
                {
                    Console.WriteLine("重复注册订阅者{0}", listener.ClientMacAddress);
                }
                else
                {
                    _subscribers.Add(listener);
                    if (SubscriberAdded != null)
                    {
                        this.SubscriberAdded(this, new SubscriberMessageEventArgs(listener));
                    }
                }
            }
        }

        public void RemoveSubscriber(Subscriber listener)
        {
            lock (_syncLock)
            {
                if (_subscribers.Contains(listener))
                {
                    this._subscribers.Remove(listener);
                }
                else
                {
                    throw new InvalidOperationException("要移除的监听器不存在");
                }
            }
            if (SubscriberRemoved != null)
            {
                this.SubscriberRemoved(this, new SubscriberMessageEventArgs(listener));
            }
        }

        public void NotifyMessage(string message)
        {
            Subscriber[] listeners = _subscribers.ToArray();
            foreach (Subscriber lstn in listeners)
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
        private void OnNotifyError(Subscriber subscriber, Exception ex)
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
