using System;
using System.Collections.Generic;
using System.Text;
using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model.Response;
using Auth.Service.Domain.Model;
using System.Security.Claims;
using Newtonsoft.Json;
namespace Auth.Service.Application
{
    public class AuthManager
    {
        AccountManager am;
        public AuthManager( AccountManager _am)
        {
            am = _am;
        } 

        public  AuthResponse FormatAuthResponse(ClientMaster client, Account ao)
        {
            var account = am.FormingAuthAccount(ao);
            var res = new AuthResponse();
            var claims = new List<Claim>() { am.FormatUserToClaim( account), new Claim("gateway", client.ClientKey) };
            res.account = account;
            res.expires = DateTime.UtcNow.AddMinutes(Singleton.Instance.AppSettings.JWTConfig.expireIn);
            res.accessToken = JwtHeader.GenerateAccessToken(claims, client);
            res.refreshToken = JwtHeader.GenerateRefreshToken(ao, client);
            return res;
        }
    }
}
