using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Service.Application
{
    public class PasswordManager
    {
        public static string DecryptFrontEndPass(string _str_pass)
        {
            //Log.LogApplication.WriteStateLog(Singleton.Instance.AppSettings.secretKey);
            //Log.LogApplication.WriteStateLog(System.Text.ASCIIEncoding.ASCII.GetBytes(Singleton.Instance.AppSettings.secretKey));
            //Log.LogApplication.WriteStateLog(System.Convert.ToBase64String(
            //              System.Text.ASCIIEncoding.ASCII.GetBytes(Singleton.Instance.AppSettings.FrontendSecretKey)
            //              ));

            //Log.LogApplication.WriteStateLog(System.Convert.FromBase64String(_str_pass));
            string str_pass =

            Encoding.UTF8.GetString(
                        System.Convert.FromBase64String(_str_pass)
            //)
            //.Replace(
            //              System.Convert.ToBase64String(
            //                  System.Text.ASCIIEncoding.ASCII.GetBytes(Singleton.Instance.AppSettings.FrontendSecretKey)
            //                  ), ""
                          );
            //Log.LogApplication.WriteStateError("_pass");
            //Log.LogApplication.WriteStateError(str_pass);

            return str_pass;

        }
    }
}
