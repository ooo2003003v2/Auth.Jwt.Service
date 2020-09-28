using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using Auth.Service.Domain.Model;
namespace Auth.Service.Persistence
{
    public class LogPersistence
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        public Singleton setting;




        public static async void WriteStateLog(object msg)
        {
            StackTrace stackTrace = new StackTrace();
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(msg));

            //System.Diagnostics.Debug.WriteLine("requestID: " + Singleton.Instance.Context.HttpContext.Request.Headers["Request-Id"].ToString());
            //System.Web.HttpContext.Current.Trace.Write(JsonConvert.SerializeObject(msg));
            log.Info(Singleton.Instance.Context.HttpContext.Request.Headers["Request-Id"].ToString() + " (" + Singleton.Instance.Context.HttpContext.Connection.RemoteIpAddress.ToString() + ":" + Singleton.Instance.Context.HttpContext.Connection.RemotePort + "):       " + stackTrace.GetFrame(2).GetFileName() + " (" + stackTrace.GetFrame(2).GetFileLineNumber() + ") " + stackTrace.GetFrame(2).GetMethod().Name + " - " + JsonConvert.SerializeObject(msg));
        }

      
        public static async void WriteStateError(object msg)
        {
            StackTrace stackTrace = new StackTrace();
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(msg));
            //System.Web.HttpContext.Current.Trace.Warn(JsonConvert.SerializeObject(msg));

            log.Error(Singleton.Instance.Context.HttpContext.Request.Headers["Request-Id"].ToString() + " (" + Singleton.Instance.Context.HttpContext.Connection.RemoteIpAddress.ToString() + ":" + Singleton.Instance.Context.HttpContext.Connection.RemotePort + "):       " + stackTrace.GetFrame(2).GetFileName() + " (" + stackTrace.GetFrame(2).GetFileLineNumber() + ") " + stackTrace.GetFrame(2).GetMethod().Name + " - " + JsonConvert.SerializeObject(msg));
        }
    }
}
