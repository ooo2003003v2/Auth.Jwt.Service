using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Auth.Service.Persistence;
using Auth.Service.Domain.Model.Request;
using Auth.Service.Domain.Model;
using Auth.Service.Domain.DBModel;
using System.Security.Claims;
using Newtonsoft.Json;
namespace Auth.Service.Application
{
    public class AccountManager
    {
        public AccountManager()
        {

        }

        public Account GetAccount(LoginInput input)
        {
            Account ao = null;
            try
            {
                ao = AccountPersistence.GetAccounts(new List<int>(), new List<string>() { input.login }).FirstOrDefault();
                if (ao == null)
                    return null;
                if (!PasswordVerification(input.password, ao.Pass))
                    return null;
                ao.AccountAdditionalType = AccountPersistence.AccountAdditionalType(ao.Id);
                ao.Pass = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");
             
            }
            return ao;
        }

        public static Account GetAccountByID(int id)
        {
            Account ao = null;
            try
            {
                ao = AccountPersistence.GetAccounts(new List<int>() { id}, new List<string>() ).FirstOrDefault();
                if (ao == null)
                    return null;
                
                ao.AccountAdditionalType = AccountPersistence.AccountAdditionalType(ao.Id);
                ao.Pass = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            
            return ao;
        }

        public string DecryptFrontEndPass(string _str_pass)
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

        public bool PasswordVerification(string str_pass, string hash)
        {
            bool flag = false;
            try
            {
                if (BCrypt.Net.BCrypt.Verify(DecryptFrontEndPass(str_pass), hash))
                    flag = true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex + "");
            }
            return flag;
        }

        public static List<Claim> FormingAccountClaim(Account ao)
        {
            List<Claim> claims = new List<Claim>() { new Claim("user", JsonConvert.SerializeObject(ao)) };

            return claims;
        }

    }
}
