using System;
using System.Collections.Generic;
using System.Text;
using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model.Response;
using Auth.Service.Domain.Model;
using System.Security.Claims;
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
            var claims = new List<Claim>() { am.FormingAccountClaim(ao), new Claim("gateway", client.ClientKey) };
            var res = new AuthResponse();
            res.expires = DateTime.UtcNow.AddMinutes(Singleton.Instance.AppSettings.JWTConfig.expireIn);
            res.accessToken = JwtHeader.GenerateAccessToken(claims, client);
            res.refreshToken = JwtHeader.GenerateRefreshToken(ao, client);
            return res;
        }
    }
}
