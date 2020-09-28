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

       public  IGetAccountStrategy getAccountStrategy;

        public AccountManager()
        {

        }

        

        public Account GetAccount<T>(T input)
        {
            Account ao = null;
            try
            {
                ao = getAccountStrategy.RequestAccount( input );
                if (ao == null)
                    return null;
             
                ao.AccountAdditionalType = AccountPersistence.AccountAdditionalType(ao.Id);
                ao.Password = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");
             
            }
            return ao;
        }

        public async void LoggingAccountLogin(Account ao)
        {
            AccountLoginLog log = new AccountLoginLog() {
                AccessIp = Singleton.Instance.Context.HttpContext.Connection.RemoteIpAddress.ToString(),
                AccountId = ao.Id,
                UserAgent = Singleton.Instance.Context.HttpContext.Request.Headers["User-Agent"].ToString(),
                CreatedDate = DateTime.UtcNow
            };
            DBContextFunction.ExcuteCreateRecords<AccountLoginLog>(new List<AccountLoginLog>() { log });
        }
       

        public Claim FormingAccountClaim(Account ao)
        {
            Claim claims = new Claim("user", JsonConvert.SerializeObject(ao));

            return claims;
        }

    }
}
