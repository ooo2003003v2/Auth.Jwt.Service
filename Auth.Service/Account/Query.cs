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
        AuthManager authm;
        public Query()
        {
            rtm = new RefreshTokenManger();
            am = new AccountManager();
            cm = new ClientManger();
            authm = new AuthManager(am);
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
            am.getAccountStrategy = new LoginByRequestInput();
            var ao = am.GetAccount(login);
            if (ao == null)
                throw new Exception("Login fail.");
            am.LoggingAccountLogin(ao);
            return authm.FormatAuthResponse(client, ao);

        }


        [GraphQLAuthorize(Policy = "AuthorizedPolicy")]
        public AuthResponse RefreshToken(IResolveFieldContext context)
        {
           
            var _client = Singleton.Instance.Context.HttpContext.User.Claims.Where(c => c.Type == "Client")
                   .Select(c => c.Value).SingleOrDefault();
            var client = cm.GetCurrentClient(_client);
            if (client == null)
                throw new Exception("client not found");
            var token = (Singleton.Instance.Context.HttpContext.Request.Headers["authorization"]+"").Split(" ")[1];
            var refreshTokenRrd = rtm.GetRefreshTokenRecord(token, client, false);
            if(refreshTokenRrd == null)
                throw new Exception("refresh token not found");
            rtm.UpdateTokenExpireTime(client, refreshTokenRrd);

            am.getAccountStrategy = new LoginById();
            var ao = am.GetAccount(refreshTokenRrd.AccountId);
            return authm.FormatAuthResponse(client, ao);

        }

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

    }
    public class Login
    {
        public string name { set; get; }
    }

}
