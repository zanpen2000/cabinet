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

        public LogAttribute(LogType type = Layer.LogType.ApplicationInfo)
        {
            this.LogType = type;

        }

        public override object Execute(params object[] objs)
        {
            if (objs.Count() < 0) return null;

            var obj = objs[0];
            switch (LogType)
            {
                case LogType.UserMessage:
                    Lib.Layer.Logger.AppendUserMessage(obj.ToString());
                    break;
                case LogType.ApplicationInfo:
                    Lib.Layer.Logger.AppendInfo(obj.ToString());
                    break;
                case LogType.Error:
                    Lib.Layer.Logger.AppendErrorInfo(obj.ToString());
                    break;
                case LogType.All:
                    Lib.Layer.Logger.AppendUserMessage(obj.ToString());
                    Lib.Layer.Logger.AppendInfo(obj.ToString());
                    Lib.Layer.Logger.AppendErrorInfo(obj.ToString());
                    break;
                default:

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
