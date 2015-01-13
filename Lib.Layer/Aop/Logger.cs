using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class Logger
    {
        static object obj = new object();

        static ILog infologger;
        static ILog usermsglogger;
        static ILog errlogger;


        static Logger()
        {
            var cfgname = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(cfgname));


            infologger = LogManager.GetLogger("loginfo");
            infologger.Logger.IsEnabledFor(log4net.Core.Level.All);

            usermsglogger = LogManager.GetLogger("logusermsg");
            usermsglogger.Logger.IsEnabledFor(log4net.Core.Level.All);


            errlogger = LogManager.GetLogger("logerror");
            errlogger.Logger.IsEnabledFor(log4net.Core.Level.All);

        }

        public static void AppendErrorInfo(string line)
        {
            errlogger.Error(line);
        }

        public static void AppendErrorInfo(string line, Exception ex)
        {
            errlogger.Error(line, ex);
        }

        public static void AppendUserMessage(string line)
        {
            usermsglogger.Info(line);
        }


        public static void AppendUserMessage(string line, Exception ex)
        {
            usermsglogger.Info(line, ex);
        }


        public static void AppendInfo(string line)
        {
            infologger.Info(line);
        }

        public static void AppendInfo(string line, Exception ex)
        {
            infologger.Info(line, ex);
        }
    }
}
