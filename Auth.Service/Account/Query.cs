using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using Auth.Service.Domain.Model;
using System.Security.Claims;
using GraphQL.Authorization;
using Auth.Service.Domain;
using Auth.Service.Domain.Model.Request;
using Auth.Service.Domain.Model.Response;
using Auth.Service.Domain.DBModel;
using Auth.Service.Application;
using Newtonsoft.Json;
namespace Auth.Service
{
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {

        public ClaimsPrincipal User { get; set; }
        public HttpContext Context { get; set; }
        
    }



    public class Client
    {
        public static string ad = "localhost";
    }
    public class Query 
    {

        RefreshTokenManger rtm;
        AccountManager am;
        ClientManger cm;
        public Query()
        {
            rtm = new RefreshTokenManger();
            am = new AccountManager();
            cm = new ClientManger();
        }

        //[GraphQLMetadata("login")]

        public AuthResponse  Login( LoginInput login)
        {


            var _client = Singleton.Instance.Context.HttpContext.Request.Headers["CID"] + "";
            var res = new AuthResponse();
            var client = cm.GetCurrentClient(_client);
            if (client == null)
                throw new Exception("client not found");
            //Singleton.Instance.Context.HttpContext.Response.Headers.Add("header", "token");
            var ao = am.GetAccount(login);
            if (ao == null)
                throw new Exception("Login fail.");
   
            List<Claim> claims = new List<Claim>() { new Claim("user", JsonConvert.SerializeObject(ao)), new Claim("gateway", client.ClientKey) };
            res.expires = DateTime.UtcNow.AddMinutes(Singleton.Instance.AppSettings.JWTConfig.expireIn);
            res.accessToken = JwtHeader.GenerateAccessToken(claims, client);         
            res.refreshToken = JwtHeader.GenerateRefreshToken(ao, client);

            return res;

        }

        //[GraphQLMetadata("logout")]
        //[GraphQLAuthorize(Policy = "Activaccesspolicy")]
        [GraphQLAuthorize(Policy = "AuthorizedPolicy")]
        public string Logout()
        {
            var _client = Singleton.Instance.Context.HttpContext.User.Claims.Where(c => c.Type == "Client")
                  .Select(c => c.Value).SingleOrDefault();
            var client = cm.GetCurrentClient(_client);
            if (client == null)
                throw new Exception("client not found");
            var token = (Singleton.Instance.Context.HttpContext.Request.Headers["authorization"] + "").Split(" ")[1];
            rtm.RemoveRefreshTokenRd(client, token);
            return null;
        }

        [GraphQLAuthorize(Policy = "AuthorizedPolicy")]
        public AuthResponse RefreshToken(IResolveFieldContext context)
        {
            var res = new AuthResponse();
            var _client = Singleton.Instance.Context.HttpContext.User.Claims.Where(c => c.Type == "Client")
                   .Select(c => c.Value).SingleOrDefault();
            var client = cm.GetCurrentClient(_client);
            if (client == null)
                throw new Exception("client not found");
            var token = (Singleton.Instance.Context.HttpContext.Request.Headers["authorization"]+"").Split(" ")[1];
            var refreshTokenRrd = rtm.GetRefreshTokenRecord(token, client,true);
            if(refreshTokenRrd == null)
                throw new Exception("refresh token not found");
            rtm.UpdateTokenExpireTime(client, refreshTokenRrd);
            refreshTokenRrd.Account.RefreshToken = null;
            List<Claim> claims = new List<Claim>() { new Claim("user", JsonConvert.SerializeObject(refreshTokenRrd.Account)), new Claim("gateway", client.ClientKey) };
            res.expires = DateTime.UtcNow.AddMinutes(Singleton.Instance.AppSettings.JWTConfig.expireIn);
            res.accessToken = JwtHeader.GenerateAccessToken(claims, client);
            res.refreshToken = JwtHeader.GenerateRefreshToken(refreshTokenRrd.Account, client); ;
            return res;

        }

        //[GraphQLMetadata("hello")]

        [GraphQLAuthorize(Policy = "AuthorizedPolicy")]
        public string Hello()
        {
            //return "";
            //Singleton.Instance.Context.HttpContext.Response.Headers.Add("header", "token");
            return "ht";
        }

    }
    public class Login
    {
        public string name { set; get; }
    }

}
