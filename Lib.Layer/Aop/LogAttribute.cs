using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Layer
{
    public class LogAttribute : AopAttribute
    {
        private LogType LogType;
        private string Message;

        public LogAttribute(LogType type, string msg = "")
        {
            this.LogType = type;
            this.Message = msg;
        }

        public override object Execute(params object[] objs)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(this.Message)) msg = this.Message;
            else
            {
                if (objs.Count() < 0) return null;
                msg = objs[0].ToString();
            }

            switch (LogType)
            {
                case LogType.UserMessage:
                    Lib.Layer.Logger.AppendUserMessage(msg);
                    break;
                case LogType.ApplicationInfo:
                    Lib.Layer.Logger.AppendInfo(msg);
                    break;
                case LogType.Error:
                    Lib.Layer.Logger.AppendErrorInfo(msg);
                    break;
                case LogType.All:
                    Lib.Layer.Logger.AppendUserMessage(msg);
                    Lib.Layer.Logger.AppendInfo(msg);
                    Lib.Layer.Logger.AppendErrorInfo(msg);
                    break;
                default:
                    Lib.Layer.Logger.AppendInfo(msg);
                    break;
            }
            return base.Execute(objs);
        }
    }

    public enum LogType
    {
        UserMessage,
        ApplicationInfo,
        Error,
        All,
    }
}
