using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model;
using Auth.Service.Persistence;
using Newtonsoft.Json;
namespace Auth.Service.Application
{
    public class RefreshTokenManger
    {
      
        public RefreshToken GetRefreshTokenRecord(string token, ClientMaster client, bool getAccount)
        {
            if (string.IsNullOrEmpty(token))
                throw new Exception("token unknown ");
            if (client == null)
                throw new Exception("client unknown ");
            var res = RefreshTokenPersistence.GetRefreshTokenRecord(token, client.ClientKeyId, getAccount);
            if(res != null && getAccount)
                res.Account.AccountAdditionalType = AccountPersistence.AccountAdditionalType(res.Account.Id); 
            return res;
        }
        public async void UpdateTokenExpireTime(ClientMaster client, RefreshToken token)
        {
            token.ExpiredTime = DateTime.UtcNow.AddMinutes(client.RefreshTokenLifeTime);
            DBContextFunction.ExcuteUpdateRecords(new List<RefreshToken>() { token },"Id" +
                "");
        }

        public async void RemoveRefreshTokenRd(ClientMaster client, string token)
        {
            DBContextFunction.ExcuteDelRecords(new List<RefreshToken>() { new RefreshToken() { Id = token ,ClientKeyId = client.ClientKeyId} });
        }

    }
}
