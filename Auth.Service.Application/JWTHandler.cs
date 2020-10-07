using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model;
using Auth.Service.Persistence;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Auth.Service.Application
{
    public class JwtHeader
    {

        public JwtHeader()
        {

        }

        public static string GenerateAccessToken(List<Claim> claims, ClientMaster client )
        {


            var tokenHandler = new JwtSecurityTokenHandler();
          
            //var claims = new List<Claim>{
            //       new Claim("graphqlaccess","false")
            //       };



            var _signInKey =
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.ClientId));
                //"SecretKeySecretKeySecretKeySecretKeySecretKeySecretKeySecretKeyS"


            var _encryptKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.ClientSecret));
       

            
            var creds = new SigningCredentials(_signInKey, SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(_encryptKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);


            var handler = new JwtSecurityTokenHandler();

            var _token = handler.CreateJwtSecurityToken(Singleton.Instance.AppSettings.JWTConfig.issuer, client.ClientName, new ClaimsIdentity(claims), null, DateTime.UtcNow.AddMinutes(
                
                client.RefreshTokenLifeTime
                ), null, creds, encryptingCredentials);
             
            return tokenHandler.WriteToken(_token);
        }

        public static string GenerateRefreshToken(Account ao,  ClientMaster RequestedClient)
        {


            var tokenHandler = new JwtSecurityTokenHandler();

            var issusTime = DateTime.UtcNow;
            var expTime = DateTime.UtcNow.AddMinutes(
                
                RequestedClient.RefreshTokenLifeTime+2
                );
                //.AddMinutes(Singleton.Instance.AppSettings.JWTConfig.expireIn);
         
            List<Claim> claims = new List<Claim>() { 
                new Claim("Client", RequestedClient.ClientKey) 
            };
            
            
            var _signInKey =
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Singleton.Instance.AppSettings.JWTConfig.signInKey));
            //"SecretKeySecretKeySecretKeySecretKeySecretKeySecretKeySecretKeyS"


            var _encryptKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Singleton.Instance.AppSettings.JWTConfig.encryptKey));



            var creds = new SigningCredentials(_signInKey, SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(_encryptKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);


            var handler = new JwtSecurityTokenHandler();

            var _token = handler.CreateJwtSecurityToken(Singleton.Instance.AppSettings.JWTConfig.issuer, Singleton.Instance.AppSettings.JWTConfig.aud, new ClaimsIdentity(claims), null  , expTime, null, creds, encryptingCredentials);
            var token = tokenHandler.WriteToken(_token);

             DBContextFunction.ExcuteCreateRecords<RefreshToken>(new List<RefreshToken>() { new RefreshToken() { Id = token, ExpiredTime = expTime, AccountId = ao.Id, IssuedTime = issusTime, ClientKeyId = RequestedClient.ClientKeyId } });
            return token;
        }

        //public static string RefreshAccessToken(string _token )
        //{
        //    var token = RefreshTokenPersistence.GetRefreshTokenRecord(_token, null, false);
        //    var ao = AccountManager.GetAccountByID(token.AccountId);
        //    var client = ClientPersistence.GetClients(new List<string>(), new List<int>() { token.ClientKeyId }).FirstOrDefault();
        //    var newAccessToken= GenerateAccessToken(AccountManager.FormingAccountClaim(ao), client);
        //    return newAccessToken;
        //}

        public static void UpdateContextUserAndToken( string newAccessToken)
        {
             
            var handler = new JwtSecurityTokenHandler();
         
            var tokenS = handler.ReadToken(newAccessToken) as JwtSecurityToken;
            var claims = tokenS.Claims;
            //context.User.Claims = claims;
        }

        public static bool CheckTokenExpiryExpired(IEnumerable<Claim> claims)
        {
            var exp = new DateTime();
            var _exp = claims.Where(c => c.Type == "exp")
          .Select(c => c.Value).SingleOrDefault();
            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(_exp));


                if (dateTimeOffset.ToUniversalTime() > DateTime.UtcNow)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

    }
}
