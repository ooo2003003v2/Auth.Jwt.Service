using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Auth.Service.Domain.Model;
using Newtonsoft.Json;
namespace Auth.Service.Application
{
    public class LogApplication
    {

        private static readonly log4net.ILog log
= log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static async void WriteStateLog(object msg)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                log.Info(Singleton.Instance.Context.HttpContext.TraceIdentifier+ " (" + Singleton.Instance.Context.HttpContext.Connection.RemoteIpAddress.ToString() + ":" + Singleton.Instance.Context.HttpContext.Connection.RemotePort + "):       " + stackTrace.GetFrame(1).GetFileName() + " (" + stackTrace.GetFrame(1).GetFileLineNumber() + ") " + stackTrace.GetFrame(1).GetMethod().Name + " - " + JsonConvert.SerializeObject(msg));
            }
            catch (Exception ex)
            {

            }
        }

        public static async void WriteStateError(object msg)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                log.Error(Singleton.Instance.Context.HttpContext.TraceIdentifier + " (" + Singleton.Instance.Context.HttpContext.Connection.RemoteIpAddress.ToString() + ":"+Singleton.Instance.Context.HttpContext.Connection.RemotePort+"):       " + stackTrace.GetFrame(1).GetFileName() + " (" + stackTrace.GetFrame(1).GetFileLineNumber() + ") " + stackTrace.GetFrame(1).GetMethod().Name + " - " + JsonConvert.SerializeObject(msg));
            }
            catch (Exception ex)
            {

            }

        }

    }
}
