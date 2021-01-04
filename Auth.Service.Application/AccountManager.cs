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
using General.Auth.Account.Domain;
using General.EF.Core.Function;
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
                if(ao.AccountTypeId != -1)
                ao.AccountType = AccountPersistence.AccountType(ao.AccountTypeId);
                ao.AccountAdditionalType = AccountPersistence.AccountAdditionalType(ao.Id);
                ao.Password = null;
                if (!ao.IsRequiredChangePass)
                    ao.IsRequiredChangePass = DateTime.UtcNow >= ao.ChangePassDeadline;
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
       

        public AccountModel FormingAuthAccount(Account ao)
        {
            List<General.Auth.Account.Domain.AccountType> accountTypes = new List<General.Auth.Account.Domain.AccountType>();
            if (ao.AccountAdditionalType != null)
                if (ao.AccountAdditionalType.Count > 0)
                    ao.AccountAdditionalType.Select(p => {
                        accountTypes.Add(new General.Auth.Account.Domain.AccountType()
                        {
                            Id = p.AccountAdditionalTypeNavigation.Id,
                            Level = p.AccountAdditionalTypeNavigation.Level,
                            GroupId = p.AccountAdditionalTypeNavigation.GroupId,
                            AccountTypeCode = p.AccountAdditionalTypeNavigation.AccountTypeCode,
                            AccountTypeName = (p.AccountAdditionalTypeNavigation.AccountTypeAttr.FirstOrDefault() == null ? "" : p.AccountAdditionalTypeNavigation.AccountTypeAttr.FirstOrDefault().Name)

                        }); return p; }).ToList();
                AccountModel am = new AccountModel()
            {
                Id = ao.Id,
                Name = ao.Name,
                AccountTypeId = ao.AccountTypeId,
                ChangePassDeadline = ao.ChangePassDeadline,
                IsRequiredChangePass = ao.IsRequiredChangePass,
                CreatedDate = ao.CreatedDate,
                Email = ao.Email,
                LastLoginDate = ao.LastLoginDate,
                UpdatedDate = ao.UpdatedDate,
                AccountType = ao.AccountType == null?
                new General.Auth.Account.Domain.AccountType()
                {
                    Id = -1,
                    Level = 0,
                    AccountTypeName="Admin",
                    AccountTypeCode="ADMIN"
                }
                : new General.Auth.Account.Domain.AccountType()
                {
                    Id = ao.AccountType.Id,
                    GroupId = ao.AccountType.GroupId,
                    AccountTypeName = ao.AccountType.AccountTypeAttr.FirstOrDefault().Name,
                    Level = ao.AccountType.Level,
                    AccountTypeCode = ao.AccountType.AccountTypeCode
                },
                AccountAdditionalType = accountTypes
                };
            return am;
            
        }

        public Claim FormatUserToClaim(AccountModel am)
        {
            Claim claims = new Claim("user", JsonConvert.SerializeObject(am));

            return claims;
        }

    }
}
