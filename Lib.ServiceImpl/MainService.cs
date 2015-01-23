using Lib.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.ServiceImpl
{
    public class MainService : IMainService
    {
         #region 单例实现
        private static readonly object _syncLock = new object();
        private static MainService _instance;

        public static MainService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MainService();
                        }
                    }
                }
                return _instance;
            }
        }

        private MainService()
        {

        }

        #endregion


        public string Test(string msg)
        {
            return DateTime.Now.ToString() + "\t" + msg;
        }
    }
}
