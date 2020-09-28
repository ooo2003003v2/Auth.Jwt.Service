using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;
namespace Auth.Service.Domain.Model
{
    public class GraphQlUserContextDictionary : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }
        public HttpContext Context { get; set; }
        public GraphQlUserContextDictionary(ClaimsPrincipal User, HttpContext context)
        {
            this.Context = context;
            this.User = User;
        }
    }
}
