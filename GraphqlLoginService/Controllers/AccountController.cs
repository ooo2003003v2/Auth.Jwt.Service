using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GraphqlLoginService.Model;
using GraphQL;
using GraphqlLoginService.Account;

namespace GraphqlLoginService.Controllers
{
    [Route("account")]
    
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            //configuration.GetSection("AppSettings").Bind(AppSettings);

     
        }


        

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GraphQLQuery query)
        {          
            var schema = new AccountSchema();
            var inputs = query.Variables.ToInputs();

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema.GraphQLSchema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.ExposeExceptions = true;
            });


            return Ok(result);
        }

    }
}
