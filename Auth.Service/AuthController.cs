using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.NewtonsoftJson;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth.Service.Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Auth.Service
{
    [Route("graphql")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            //configuration.GetSection("AppSettings").Bind(AppSettings);

        }
        //[AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GraphQLQuery query)
        {
            //Request.Headers.Add("Request-Id", HttpContext.TraceIdentifier);
            var a = Request.Host.Value;

            //Log.LogApplication.WriteStateLog(Singleton.Instance.Context.HttpContext.Request.Headers);
            
            //BaseApplication.GetGlobalValue();
            //System.Diagnostics.Debug.WriteLine("wait taSK ");
            


            //setting.Context.HttpContext.Request.Headers.Add("Request-Id", HttpContext.TraceIdentifier);
            //System.Diagnostics.Debug.WriteLine("s",JsonConvert.SerializeObject(setting.Context.HttpContext.Request.Headers));
            //System.Diagnostics.Debug.WriteLine("ASYNC ");
            var schema = new AccountSchema();
            var inputs = query.Variables.ToInputs();
            var graphQlUserContext = new GraphQlUserContextDictionary(User,HttpContext); 
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema.GraphQLSchema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.EnableMetrics = false;
              
                _.UserContext = graphQlUserContext;
            });

            //Log.LogApplication.WriteStateLog(Response.Headers);

            return Ok(result);
        }

    }
}
