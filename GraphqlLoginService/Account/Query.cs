
using System;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Server.Authorization.AspNetCore;
namespace GraphqlLoginService.Account
{

 
    public class Query 
    {
  

        [GraphQLMetadata("login")]

        public string Login()
        {
            
            return "success";

        }

        [GraphQLMetadata("logout")]
        [GraphQLAuthorize(Policy = "graphqlaccesspolicy")]
        public string Logout()
        {
          
            return null;

        }

        [GraphQLMetadata("hello")]
        [GraphQLAuthorize(Policy = "graphqlaccesspolicy")]
        public string GetHello()
        {

  
            return "World";
        }

    }


}
